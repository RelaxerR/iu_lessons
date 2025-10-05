using UnityEngine;

namespace Internal.Scripts.Lesson2
{
  /// <summary>
  /// Контроллер огненного шара.
  /// Летит в направлении цели (игрока) и уничтожается при столкновении.
  /// </summary>
  public class BallController : MonoBehaviour
  {
    #region Inspector Fields

    [Header("=== Movement ===")]
    [Tooltip("Скорость полёта огненного шара")]
    [SerializeField] private float speed = 15f;

    [Header("=== Targeting ===")]
    [Tooltip("Если true — шар будет искать игрока при старте и лететь к нему. Иначе — летит прямо.")]
    [SerializeField] private bool seekPlayerOnStart = true;

    #endregion

    #region Private Fields

    private Vector3 moveDirection;
    private Transform playerTransform;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
      // Пытаемся найти игрока, если включено
      if (seekPlayerOnStart)
      {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
          playerTransform = player.transform;
          // Направление к игроку на момент создания
          moveDirection = (playerTransform.position - transform.position).normalized;
        }
        else
        {
          // Если игрока нет — летим вперёд по локальной оси Z
          moveDirection = transform.forward;
        }
      }
      else
      {
        // Летим вперёд по направлению спавна
        moveDirection = transform.forward;
      }
    }

    private void Update()
    {
      // Двигаем шар
      transform.Translate(moveDirection * (speed * Time.deltaTime), Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
      // Уничтожаем шар при столкновении с чем угодно
      Destroy(gameObject);

      // Опционально: нанести урон игроку, если попали в него
      if (collision.gameObject.CompareTag("Player"))
      {
        // Пример: вызвать метод получения урона
        // collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(25f);
        Debug.Log("Fireball hit the Player!");
      }
    }

    #endregion
  }
}