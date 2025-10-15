using System;
using Internal.Scripts.Interface;
using UnityEngine;

namespace Internal.Scripts
{
  public class FanControllerInteractable : MonoBehaviour, IInteractable
  {
    [SerializeField]
    private Vector3 RotationVector;

    [SerializeField]
    private float targetSpeed = 100f; // Максимальная скорость вращения

    [SerializeField]
    private float acceleration = 10f; // Скорость разгона/торможения

    private float currentSpeed = 0f;
    private bool isOn = false;

    private void Update()
    {
      // Плавное изменение скорости
      currentSpeed = Mathf.MoveTowards(currentSpeed, isOn ? targetSpeed : 0f, acceleration * Time.deltaTime);

      // Вращение вентилятора
      transform.Rotate(RotationVector * currentSpeed * Time.deltaTime);
    }

    public void Interact()
    {
      isOn = !isOn;
    }
  }
}
