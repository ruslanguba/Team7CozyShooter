using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrajectorySimulator
{
    private readonly PredictionSceneProvider _sceneProvider;
    private readonly GameObject _projectilePrefab;

    public TrajectorySimulator(PredictionSceneProvider provider, GameObject projectilePrefab)
    {
        _sceneProvider = provider;
        _projectilePrefab = projectilePrefab;
    }

    public List<Vector3> Simulate(Vector3 startPos, Vector3 startVel)
    {
        var physicsScene = _sceneProvider.PhysicsScene;
        var predictionScene = _sceneProvider.Scene;

        // Создаём ghost-снаряд
        GameObject ghost = Object.Instantiate(_projectilePrefab, startPos, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghost, predictionScene);

        // Делаем ghost невидимым
        foreach (var renderer in ghost.GetComponentsInChildren<Renderer>())
            renderer.enabled = false;

        Rigidbody rb = ghost.GetComponent<Rigidbody>();
        rb.linearVelocity = startVel;

        List<Vector3> points = new List<Vector3>();
        float step = Time.fixedDeltaTime;

        for (int i = 0; i < 300; i++)
        {
            physicsScene.Simulate(step);
            points.Add(ghost.transform.position);

            if (rb.linearVelocity.sqrMagnitude < 0.01f)
                break;
        }

        return points;
    }
}