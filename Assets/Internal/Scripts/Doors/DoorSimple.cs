using System.Collections;
using Internal.Scripts.Interface;
using UnityEngine;

namespace Internal.Scripts.Doors
{
  [RequireComponent(typeof(BoxCollider))]
  public class DoorSimple : MonoBehaviour, IDoor, IInteractable
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
      Debug.Log("DoorSimple initialized.");
    }

    public void Open()
    {
      Debug.Log("Open() called.");
      StartCoroutine(OpenDoor());
    }

    public void Close()
    {
      Debug.Log("Close() called.");
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
    public void Interact()
    {
      StopAllCoroutines();
      StartCoroutine(!isOpen ? OpenDoor() : CloseDoor());
    }
  }
}