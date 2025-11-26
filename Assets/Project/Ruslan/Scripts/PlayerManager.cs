using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    private InputReader _input;
    public Transform GetPlayerTransform => _playerTransform;

    [SerializeField] private Transform _playerTransform;
    [SerializeField] private CinemachineInputAxisController _axisController;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            _playerTransform = transform;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _input = GetComponent<InputReader>();
        _axisController = GetComponentInChildren<CinemachineInputAxisController>();
    }

    public void EnableInput()
    {
        _input.enabled = true;
        _axisController.enabled = true;
    }

    public void DisableInput()
    {
        _input.enabled = false;
        _axisController.enabled = false;
    }
}
