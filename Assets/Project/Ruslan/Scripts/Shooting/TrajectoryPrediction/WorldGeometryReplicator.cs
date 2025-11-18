using UnityEngine;
using UnityEngine.SceneManagement;


public class WorldGeometryReplicator
{
    private readonly PredictionSceneProvider _sceneProvider;
    private readonly StaticObjectsRegistry _staticObjectsRegistry;
    private readonly DynamicObjectsRegistry _dynamicObjectsRegistry;
    public WorldGeometryReplicator(PredictionSceneProvider provider, StaticObjectsRegistry staticObjectsRegistry, DynamicObjectsRegistry dynamicObjectsRegistry)
    {
        _sceneProvider = provider;
        _staticObjectsRegistry = staticObjectsRegistry;
        _dynamicObjectsRegistry = dynamicObjectsRegistry;
    }

    public void SyncGeometry()
    {
        Scene predictionScene = _sceneProvider.Scene;

        // Клонируем динамические объекты с Rigidbody
        foreach (Rigidbody rbOriginal in _dynamicObjectsRegistry.DynamicBodies)
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
        CloneStatic();
    }

    private void CloneStatic()
    {
        foreach (Collider col in _staticObjectsRegistry.StaticColliders)
        {
            if (col.attachedRigidbody == null)
            {
                GameObject clone = Object.Instantiate(col.gameObject);
                SceneManager.MoveGameObjectToScene(clone, _sceneProvider.Scene);

                foreach (var renderer in clone.GetComponentsInChildren<Renderer>())
                    renderer.enabled = false;
            }
        }
    }
}
