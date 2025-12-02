using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    ScoreManager scoreManager;

    private void Awake()
    {
        if(scoreManager == null)
            scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Collectable collectable))
        {
            collectable.Collect(transform);
            scoreManager.AddScore(collectable.ScoreToAdd);    
        }
    }
}
