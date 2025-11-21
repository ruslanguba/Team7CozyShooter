using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager: MonoBehaviour
{
    public static PlayerManager Instance;

    public Transform GetPlayerTransform => _playerTransform;
    private Transform _playerTransform;

    private void Awake()
    {
        // Singleton, но не переносим между сценами
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        FindPlayerInScene();
    }

    private void OnEnable()
    {
        // Подписываемся на смену сцены, чтобы искать игрока заново
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindPlayerInScene();
    }

    private void FindPlayerInScene()
    {
        GameObject player = FindFirstObjectByType<PlayerContext>().gameObject;

        if (player != null)
        {
            _playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("PlayerManager: игрок не найден в сцене. Убедись, что у игрока установлен тег 'Player'.");
        }
    }
}
