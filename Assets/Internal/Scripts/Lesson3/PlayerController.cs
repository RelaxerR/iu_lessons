using System;
using UnityEngine;

namespace Internal.Scripts.Lesson3
{
  public class PlayerController : MonoBehaviour
  {
    [SerializeField]
    private float Speed= 6.0f;
    [SerializeField]
    private float Gravity = 20.0f;

    private void Start()
    {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }
  }
}
