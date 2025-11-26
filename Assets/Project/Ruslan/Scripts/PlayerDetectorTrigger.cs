using System;
using UnityEngine;

public class PlayerDetectorTrigger : MonoBehaviour
{
    public event Action OnTriggerEnterAction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerContext player))
        {
            OnTriggerEnterAction?.Invoke();
        }
    }

}
