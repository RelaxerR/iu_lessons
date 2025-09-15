using System;
using UnityEngine;

namespace Internal.Scripts.Lesson3
{
  public class MouseLookX : MonoBehaviour
  {
    [SerializeField]
    private float SensitivityHorizontal = 3.0f;

    private void Update()
    {
      var horizontalRotation = Input.GetAxis("Mouse X") * SensitivityHorizontal;
      transform.Rotate(0, horizontalRotation, 0);
    }
  }
}
