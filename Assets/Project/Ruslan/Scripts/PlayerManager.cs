using UnityEngine;

public class PlayerManager: MonoBehaviour
{
    public static PlayerManager Instance;
    public Transform GetPlayerTransform => _playerTransform;
    private Transform _playerTransform;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void SetPlayerTransform(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }
}
