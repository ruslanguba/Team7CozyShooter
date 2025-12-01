using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event Action OnOpenMenu;
    public event Action OnCloseMenu;
    public event Action OnSpace;

    public event Action OnOpenComics;

    public static GameManager Instance { get; private set; }

    [SerializeField] private GameInput gameInput;
    private bool _isPaused;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnEnable()
    {
        gameInput.OnPause += ShowMenu;
        gameInput.OnTub += ShowInstruction;
    }

    private void OnDisable()
    {
        gameInput.OnPause -= ShowMenu;
        gameInput.OnTub -= ShowInstruction;
    }

    private void ShowMenu()
    {
        OnOpenMenu?.Invoke();
        HandlePause();
    }

    private void ShowInstruction()
    {
        OnOpenComics?.Invoke();
        HandlePause();
    }

    private void HandlePause()
    {
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            PauseGame();
        }
        else
        {
            ContinueGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PlayerManager.Instance.DisableInput();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        PlayerManager.Instance.EnableInput();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _isPaused = false;

        OnCloseMenu?.Invoke();
    }

    public void ExitToMenu()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 1;
        var request = new SceneTransitionRequest("Main menu");
        SceneTransitionManager.Instance.Transition(request);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ContinueGame();
    }
}
