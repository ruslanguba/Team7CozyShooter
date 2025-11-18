using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StaticObjectsRegistry : MonoBehaviour
{
    [SerializeField] private List<Collider> _staticColliders = new();
    public IReadOnlyList<Collider> StaticColliders => _staticColliders;

    [SerializeField] private PredictionSceneProvider _sceneProvider; // ссылка на сцену для клонирования
    [SerializeField] private int _batchSize = 50; // сколько объектов обрабатывать за кадр

    private void Start()
    {
        StartCoroutine(CollectAllCoroutine());
    }

    public void SetPredictionSceneProvider(PredictionSceneProvider sceneProvider)
    {
        _sceneProvider = sceneProvider;
    }

    public IEnumerator CollectAllCoroutine()
    {
        _staticColliders.Clear();

        var colliders = Object.FindObjectsByType<Collider>(FindObjectsSortMode.None);
        int counter = 0;

        foreach (var col in colliders)
        {
            if (col.attachedRigidbody == null)
                _staticColliders.Add(col);

            counter++;
            if (counter >= _batchSize)
            {
                counter = 0;
                yield return null; // пропускаем кадр
            }
        }
    }
}
