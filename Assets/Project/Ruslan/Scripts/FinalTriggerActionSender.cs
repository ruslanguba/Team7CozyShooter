using System;
using UnityEngine;

public class FinalTriggerActionSender : MonoBehaviour
{
    public event Action<bool> OnTriggerEnterAction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerSafePosition component))
        {
            OnTriggerEnterAction?.Invoke(true);
        }
    }

}
