using System;
using UnityEngine;

namespace Internal.Scripts
{
  public class BallController : MonoBehaviour
  {
    [SerializeField]
    private Vector3 MoveVector;
    [SerializeField]
    private Vector3 RotateVector;
    
    private void Update()
    {
      transform.Translate(MoveVector * Time.deltaTime);
      transform.Rotate(RotateVector * Time.deltaTime);
    }
  }
}
