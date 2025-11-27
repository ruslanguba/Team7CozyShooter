using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Transition(SceneTransitionRequest request)
    {
        StartCoroutine(LoadSceneRoutine(request));
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator LoadSceneRoutine(SceneTransitionRequest request)
    {
        // TODO: fade-out
        yield return SceneManager.LoadSceneAsync(request.SceneName);
        Debug.Log("Loadeded");
        // TODO: fade-in
    }
}
