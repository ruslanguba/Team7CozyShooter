using UnityEngine;

public class NPC_Canvas : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject Canvas;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerContext>() != null)
        { 
            Canvas.SetActive(true); 
        }
    }

    private void Start()
    {
        Canvas.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.GetComponent<PlayerContext>() != null)
        {
            Canvas.SetActive(false);
        }
    }
}
