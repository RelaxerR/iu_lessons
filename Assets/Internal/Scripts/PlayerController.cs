using UnityEngine;

namespace Internal.Scripts
{
  public class PlayerController : MonoBehaviour
  {
    #region Serializable

    [SerializeField]
    private string HelloMessage = "Hello from PlayerController!";

    #endregion

    #region Unity Events

    private void Start()
    {
      #if DEBUG
      Debug.Log(HelloMessage);
      #endif
    }

    #endregion
  }   
}
