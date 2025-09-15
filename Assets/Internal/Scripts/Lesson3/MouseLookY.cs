using System;
using UnityEngine;

namespace Internal.Scripts.Lesson3
{
  public class MouseLookY : MonoBehaviour
  {
    [SerializeField]
    private float SensitivityVertical = 3.0f;
    [SerializeField]
    private float MinimumVerticalRotation = -60.0f;
    [SerializeField]
    private float MaximumVerticalRotation = 60.0f;
    
    private float VerticalRotation { get; set; }

    private void Update()
    {
      VerticalRotation -= Input.GetAxis("Mouse Y") * SensitivityVertical;
      VerticalRotation = Mathf.Clamp(VerticalRotation, MinimumVerticalRotation, MaximumVerticalRotation);
      
      var horizontalRotation = transform.localEulerAngles.y;
      transform.localEulerAngles = new Vector3(VerticalRotation, horizontalRotation, 0);
    }
  }
}
