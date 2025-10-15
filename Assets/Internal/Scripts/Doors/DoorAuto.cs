using System.Collections;
using Internal.Scripts.Interface;
using UnityEngine;

namespace Internal.Scripts.Doors
{
  [RequireComponent(typeof(BoxCollider))]
  public class DoorAuto : MonoBehaviour, IDoor
  {
    [SerializeField]
    private GameObject RotationPoint;
    public float openAngle = 90f;
    public float openSpeed = 2f;
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openedRotation;

    private void Start()
    {
      closedRotation = transform.rotation;
      openedRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Player") && !isOpen)
        StartCoroutine(OpenDoor());
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.CompareTag("Player") && isOpen)
        StartCoroutine(CloseDoor());
    }

    public void Open()
    {
      if (!isOpen)
        StartCoroutine(OpenDoor());
    }

    public void Close()
    {
      if (isOpen)
        StartCoroutine(CloseDoor());
    }

    private IEnumerator OpenDoor()
    {
      isOpen = true;
      float t = 0;
      Debug.Log("OpenDoor coroutine started.");
      var prevRotation = RotationPoint.transform.rotation;
      while (t < 1)
      {
        t += Time.deltaTime * openSpeed;
        RotationPoint.transform.rotation = Quaternion.Slerp(prevRotation, openedRotation, t);
        yield return null;
      }
      Debug.Log("Door opened.");
    }

    private IEnumerator CloseDoor()
    {
      isOpen = false;
      float t = 0;
      Debug.Log("CloseDoor coroutine started.");
      var prevRotation = RotationPoint.transform.rotation;
      while (t < 1)
      {
        t += Time.deltaTime * openSpeed;
        RotationPoint.transform.rotation = Quaternion.Slerp(prevRotation, closedRotation, t);
        yield return null;
      }
      Debug.Log("Door closed.");
    }
  }
}