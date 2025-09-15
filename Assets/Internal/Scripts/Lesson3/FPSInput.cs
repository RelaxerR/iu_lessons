using System;
using UnityEngine;

namespace Internal.Scripts.Lesson3
{
  [RequireComponent(typeof(CharacterController))]
  [AddComponentMenu("Control Script/FPS Input")]
  public class FPSInput : MonoBehaviour
  {
    [SerializeField]
    private float Speed= 6.0f;
    [SerializeField]
    private float Gravity = 20.0f;
    
    [SerializeField]
    private float RollSpeed = 100f;
    [SerializeField]
    private float RollMax = 30f;
    
    private CharacterController _characterController { get; set; }
    private float currentRoll { get; set; }

    private void Start()
    {
      _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
      var deltaX = Input.GetAxis("Horizontal") * Speed;
      var deltaZ = Input.GetAxis("Vertical") * Speed;
      var deltaY = Input.GetAxis("UpDown") * Speed;
      var deltaRoll = Input.GetAxis("Roll") * Speed;
      
      Debug.Log($"deltaX: {deltaX}, deltaY: {deltaY}, deltaZ: {deltaZ}, deltaRoll: {deltaRoll}");
      
      currentRoll += deltaRoll * RollSpeed * Time.deltaTime;
      currentRoll = Mathf.Clamp(currentRoll, -RollMax, RollMax);
      
      var movement = new Vector3(deltaX, deltaY, deltaZ);
      
      movement = Vector3.ClampMagnitude(movement, Speed);
      // movement.y = Gravity;
      
      movement *= Time.deltaTime;
      movement = transform.TransformDirection(movement);
      
      _characterController.Move(movement);
      
      var euler = transform.localEulerAngles;
      euler.z = -currentRoll;
      transform.localEulerAngles = euler;
    }
  }
}
