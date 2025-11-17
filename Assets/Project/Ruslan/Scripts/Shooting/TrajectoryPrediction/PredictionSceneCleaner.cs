using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PredictionSceneCleaner
{
    private readonly PredictionSceneProvider _provider;

    public PredictionSceneCleaner(PredictionSceneProvider provider)
    {
        _provider = provider;
    }

    public void ClearScene()
    {
        if (!_provider.Scene.IsValid()) return;

        List<GameObject> toDelete = new List<GameObject>();
        foreach (GameObject obj in _provider.Scene.GetRootGameObjects())
            toDelete.Add(obj);

        foreach (GameObject obj in toDelete)
            Object.DestroyImmediate(obj);
    }
}
