using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Internal.Scripts
{
  [RequireComponent(typeof(Rigidbody))]
  [RequireComponent(typeof(PlayerInput))]
  public class PlayerController : MonoBehaviour
  {
    [SerializeField]
    private float MoveSpeed = 10f;
    [SerializeField]
    private float SensitivityVertical = 1f;

    [SerializeField]
    private GameObject Head;
    
    private Vector2 moveInput;
    [SerializeField]
    private float DeadZoneMin = -45f;
    [SerializeField]
    private float DeadZoneMax = 45f;

    private float VerticalRotation;
    
    [SerializeField]
    private float RunSpeed = 20;

    private float CurrentSpeed;

    private void Start()
    {
      CurrentSpeed = MoveSpeed;
    }
    public void OnRun(InputAction.CallbackContext context)
    {
      var input = context.ReadValue<float>();
      Debug.Log(input);
      CurrentSpeed = input > 0 ? RunSpeed : MoveSpeed;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
      moveInput = context.ReadValue<Vector2>();
    }
    public void OnRotate(InputAction.CallbackContext context)
    {
      var rotateInput = context.ReadValue<Vector2>();
      transform.Rotate(0, rotateInput.x, 0, Space.World);
      
      VerticalRotation -= rotateInput.y * SensitivityVertical;
      VerticalRotation = Mathf.Clamp(VerticalRotation, DeadZoneMin, DeadZoneMax);
      
      Head.transform.localEulerAngles = new Vector3(VerticalRotation, 0, 0);
    }

    private void Update()
    {
      var moveVector = new Vector3(moveInput.x, 0, moveInput.y);
      transform.Translate(moveVector * (Time.deltaTime * CurrentSpeed), Space.Self);
    }
    
    
  }
}
