using UnityEngine;

public class SceneTransitionRequest
{
    public string SceneName { get; }

    public SceneTransitionRequest(string sceneName)
    {
        SceneName = sceneName;
    }
}
