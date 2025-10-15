using System;
using Internal.Scripts.Interface;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Internal.Scripts
{
  [RequireComponent(typeof(Rigidbody))]
  [RequireComponent(typeof(PlayerInput))]
  public class PlayerController : MonoBehaviour
  {
    #region Inspector Fields

    [Header("=== Movement ===")]
    [SerializeField] private float MoveSpeed = 10f;
    [SerializeField] private float RunSpeed = 20f;

    [Header("=== Rotation ===")]
    [SerializeField] private float SensitivityVertical = 1f;
    [SerializeField] private float DeadZoneMin = -45f;
    [SerializeField] private float DeadZoneMax = 45f;
    [SerializeField] private GameObject Head;

    [Header("=== Combat ===")]
    [Tooltip("Префаб огненного шара игрока")]
    [SerializeField] private GameObject FireballPrefab;

    [Tooltip("Точка спавна снаряда (например, рука или рот)")]
    [SerializeField] private Transform FireballSpawnPoint;

    [Tooltip("Дальность рейкаста для поиска врага")]
    [SerializeField] private float FireRaycastDistance = 50f;

    [Tooltip("Кулдаун между выстрелами (в секундах)")]
    [SerializeField] private float FireCooldown = 0.5f;

    #endregion

    #region Private Fields

    private Vector2 moveInput;
    private float verticalRotation;
    private float currentSpeed;
    private float nextFireTime;
    [CanBeNull]
    private IInteractable currentInteractable;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
      currentSpeed = MoveSpeed;
      nextFireTime = Time.time;
    }

    private void Update()
    {
      HandleMovement();
      HandleShooting();
    }

    private void OnTriggerEnter(Collider other)
    {
      Debug.Log($"Some object entered in player trigger: {other.gameObject.name}");
      if (other.TryGetComponent<IInteractable>(out var component)) currentInteractable = component;
    }
    private void OnTriggerExit(Collider other)
    {
      Debug.Log($"Some object exit in player trigger: {other.gameObject.name}");
      if (other.TryGetComponent<IInteractable>(out var component) && currentInteractable == component) currentInteractable = null;
    }

    #endregion

    #region Input Handlers

    public void OnRun(InputAction.CallbackContext context)
    {
      var input = context.ReadValue<float>();
      currentSpeed = input > 0 ? RunSpeed : MoveSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
      moveInput = context.ReadValue<Vector2>();
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
      var rotateInput = context.ReadValue<Vector2>();
      transform.Rotate(0, rotateInput.x, 0, Space.World);

      verticalRotation -= rotateInput.y * SensitivityVertical;
      verticalRotation = Mathf.Clamp(verticalRotation, DeadZoneMin, DeadZoneMax);

      Head.transform.localEulerAngles = new Vector3(verticalRotation, 0, 0);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
      if (context.performed)
      {
        TryShoot();
      }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
      Debug.Log($"Some object interacted with player interact: {context.started}");
      if (!context.started) return;
      currentInteractable?.Interact();
    }

    #endregion

    #region Private Methods

    private void HandleMovement()
    {
      var moveVector = new Vector3(moveInput.x, 0, moveInput.y);
      transform.Translate(moveVector * (Time.deltaTime * currentSpeed), Space.Self);
    }

    private void HandleShooting()
    {
      // Можно также вызывать TryShoot() по кнопке, а не каждый кадр — но оставим так для гибкости
    }

    private void TryShoot()
    {
      if (!FireballPrefab || !FireballSpawnPoint) return;

      if (Time.time < nextFireTime) return;

      // Направление выстрела — вперёд от точки спавна
      var fireDirection = FireballSpawnPoint.forward;

      // Рейкаст в поисках врага
      // if (!Physics.Raycast(FireballSpawnPoint.position, fireDirection, out var hit, FireRaycastDistance))
      //   return;
      // // Проверяем, попали ли во врага (по тегу или по компоненту)
      // if (!hit.transform.CompareTag("Enemy"))
      //   return;
      // Создаём огненный шар
      Instantiate(FireballPrefab, FireballSpawnPoint.position, FireballSpawnPoint.rotation);
      nextFireTime = Time.time + FireCooldown;

      // Debug.Log($"Hit enemy: {hit.transform.name}");
    }

    #endregion
  }
}