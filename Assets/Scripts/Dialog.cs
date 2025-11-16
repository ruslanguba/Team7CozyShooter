using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField] private GameObject _text;    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerContext playerContext))
        {
            _text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        if (other.gameObject.TryGetComponent(out PlayerContext playerContext))
        {
            _text.SetActive(false);
        }
    }
}
