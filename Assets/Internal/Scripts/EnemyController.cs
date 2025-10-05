using System;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Internal.Scripts.Lesson2
{
  /// <summary>
  /// Контроллер врага. Двигается в случайном направлении, при столкновении с игроком — бежит к нему.
  /// Автоматически поворачивается в сторону движения. При входе в триггер — преследует игрока и стреляет огненными шарами.
  /// </summary>
  public class EnemyController : MonoBehaviour
  {
    #region Inspector Fields

    [Header("=== Movement Settings ===")]
    [Tooltip("Скорость передвижения врага")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("=== Direction Settings ===")]
    [Tooltip("Минимальное значение для случайного направления по X/Z")]
    [SerializeField] private float directionChangeSpeedMin = -1f;

    [Tooltip("Максимальное значение для случайного направления по X/Z")]
    [SerializeField] private float directionChangeSpeedMax = 1f;

    [Header("=== Combat Settings ===")]
    [Tooltip("Префаб огненного шара")]
    [SerializeField] private GameObject fireballPrefab;

    [Tooltip("Точка спавна огненного шара (например, рот или рука)")]
    [SerializeField] private Transform fireballSpawnPoint;

    [Tooltip("Периодичность стрельбы огненными шарами (в секундах)")]
    [SerializeField] private float fireballCooldown = 2f;

    [Tooltip("Длина рейкаста к цели для проверки видимости")]
    [SerializeField] private float raycastDistance = 10f;

    [Header("=== References ===")]
    [Tooltip("Ссылка на голову врага (если нужна отдельная логика взгляда)")]
    [SerializeField] private GameObject head;

    #endregion

    #region Private Fields

    /// <summary>Текущее направление движения врага (в XZ-плоскости)</summary>
    private Vector3 currentDirection;
    [CanBeNull] private GameObject Target;

    /// <summary>Таймер до следующего выстрела</summary>
    private float nextFireballTime;

    #endregion

    #region Unity Lifecycle

    /// <summary>
    /// Инициализация: задаём случайное начальное направление
    /// </summary>
    private void Start()
    {
      CalculateRandomDirection();
      nextFireballTime = Time.time;
    }

    /// <summary>
    /// Обновление позиции и поворота каждый кадр
    /// </summary>
    private void Update()
    {
      // Если есть цель — двигаемся к ней
      if (Target)
      {
        var directionToTarget = (Target.transform.position - transform.position);
        directionToTarget.y = 0f; // игнорируем высоту
        currentDirection = directionToTarget.normalized;
      }

      // Перемещаем врага
      var moveVector = currentDirection * (Time.deltaTime * moveSpeed);
      transform.Translate(moveVector, Space.World);

      // Поворачиваем врага в сторону движения (игнорируя Y)
      if (currentDirection != Vector3.zero)
      {
        var flatDirection = new Vector3(currentDirection.x, 0, currentDirection.z).normalized;
        if (flatDirection != Vector3.zero)
        {
          transform.rotation = Quaternion.LookRotation(flatDirection);
        }
      }

      LookToPlayer();
      TryShootFireball();
    }

    #endregion

    #region Private Methods

    private void LookToPlayer()
    {
      if (!Target || !head) return;
      var lookPos = Target.transform.position - head.transform.position;
      lookPos.y = 0;
      head.transform.LookAt(head.transform.position + lookPos);
    }

    private void TryShootFireball()
    {
      if (!Target || !fireballPrefab || !fireballSpawnPoint) return;

      // Проверяем, прошло ли время до следующего выстрела
      if (Time.time < nextFireballTime) return;

      // Проверяем видимость цели через рейкаст
      var directionToTarget = (Target.transform.position - fireballSpawnPoint.position).normalized;
      if (Physics.Raycast(fireballSpawnPoint.position, directionToTarget, out var hit, raycastDistance))
      {
        if (hit.transform.gameObject == Target)
        {
          // Стреляем!
          Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);
          nextFireballTime = Time.time + fireballCooldown;
        }
      }
    }

    #endregion

    #region Collision & Trigger Handling

    /// <summary>
    /// При столкновении: если это игрок — бежим к нему, иначе — меняем направление
    /// </summary>
    private void OnCollisionEnter(Collision other)
    {
      if (other.gameObject.CompareTag("Player"))
      {
        SetTarget(other.gameObject);
      }
      else
      {
        CalculateRandomDirection();
      }
    }

    /// <summary>
    /// При входе в триггер: если это игрок — устанавливаем его как цель
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.CompareTag("Player"))
      {
        SetTarget(other.gameObject);
      }
    }

    /// <summary>
    /// Устанавливает цель для преследования и атаки
    /// </summary>
    private void SetTarget(GameObject target)
    {
      Target = target;
      // Направляемся к игроку
      currentDirection = (target.transform.position - transform.position);
      currentDirection.y = 0f;
      currentDirection.Normalize();
    }

    #endregion

    #region Direction Logic

    /// <summary>
    /// Генерирует случайное направление движения в XZ-плоскости
    /// </summary>
    private void CalculateRandomDirection()
    {
      currentDirection = new Vector3(
        Random.Range(directionChangeSpeedMin, directionChangeSpeedMax),
        0f,
        Random.Range(directionChangeSpeedMin, directionChangeSpeedMax)
      ).normalized;
    }

    #endregion
  }
}