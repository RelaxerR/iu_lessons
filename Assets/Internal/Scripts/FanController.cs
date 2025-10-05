using System;
using UnityEngine;

namespace Internal.Scripts
{
  public class FanController : MonoBehaviour
  {
    [SerializeField]
    private Vector3 RotationVector;
    private void Update()
    {
      transform.Rotate(RotationVector * Time.deltaTime);
    }
  }
}
