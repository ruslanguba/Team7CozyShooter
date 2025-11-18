using UnityEngine;

public class CanvasTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _text;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerContext playerContext))
        {
            _text.SetActive(true);
            Time.timeScale = 0;
        }
        
    }
}
