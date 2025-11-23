using System;
using UnityEngine;

public class HintActivatorTrigger : MonoBehaviour
{
    public event Action<bool, Vector3> OnTriggerActivated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out InputReader inputReader))
        {
            OnTriggerActivated?.Invoke(true, other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerActivated?.Invoke(false, other.transform.position);
    }
}
