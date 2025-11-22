using UnityEngine;

public class CanvasTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _text;

    private void Start()
    {
        _text.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out InputReader inputReader))
        {
            //inputReader.enabled = false;
            _text.SetActive(true);
            _text.transform.LookAt(other.transform.position);
            //Time.timeScale = 0;
            //Cursor.lockState = CursorLockMode.Confined;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _text.SetActive(false);
    }
}
