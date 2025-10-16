using UnityEngine;

namespace Internal.Scripts
{
  public class JumpPlatformController : MonoBehaviour
  {
    [SerializeField] private float jumpBoost = 15f;

    private void OnTriggerEnter(Collider other)
    {
      var player = other.GetComponent<PlayerController>();
      if (!player)
        return;
      var rb = other.GetComponent<Rigidbody>();
      if (rb)
      {
        rb.AddForce(Vector3.up * jumpBoost, ForceMode.Impulse);
      }
    }
  }
}