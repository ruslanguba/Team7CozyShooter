using UnityEngine;

public class CanvasTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _text;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out InputReader inputReader))
        {
            inputReader.enabled = false;
            _text.SetActive(true);
            //Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
        }       
    }
}
