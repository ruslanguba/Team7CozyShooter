using UnityEngine;
using UnityEngine.SceneManagement;


public class WorldGeometryReplicator
{
    private readonly PredictionSceneProvider _sceneProvider;

    public WorldGeometryReplicator(PredictionSceneProvider provider)
    {
        _sceneProvider = provider;
    }

    public void SyncGeometry()
    {
        Scene predictionScene = _sceneProvider.Scene;

        // Клонируем динамические объекты с Rigidbody
        foreach (Rigidbody rbOriginal in Object.FindObjectsByType<Rigidbody>(0))
        {
            GameObject clone = Object.Instantiate(rbOriginal.gameObject);
            SceneManager.MoveGameObjectToScene(clone, predictionScene);

            // Отключаем визуал и скрипты
            foreach (var renderer in clone.GetComponentsInChildren<Renderer>())
                renderer.enabled = false;

            foreach (var mb in clone.GetComponents<MonoBehaviour>())
                mb.enabled = false;

            if (clone.transform.childCount > 0 || clone.GetComponents<MonoBehaviour>().Length > 0)
            {
                foreach (var mb in clone.GetComponentsInChildren<MonoBehaviour>())
                {
                    mb.enabled = false; // отключаем все скрипты на клоне
                }
            }
        }

        // Клонируем статичные Collider без Rigidbody
        foreach (Collider col in Object.FindObjectsByType<Collider>(0))
        {
            if (col.attachedRigidbody == null)
            {
                GameObject clone = Object.Instantiate(col.gameObject);
                SceneManager.MoveGameObjectToScene(clone, predictionScene);

                foreach (var renderer in clone.GetComponentsInChildren<Renderer>())
                    renderer.enabled = false;
            }
        }
    }
}
