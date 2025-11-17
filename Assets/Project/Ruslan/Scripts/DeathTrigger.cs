using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerSafePosition playerSafePosition))
        {
            playerSafePosition.ReturnPlayerToPlatform();
            Debug.Log(other);
        }
    }
}
