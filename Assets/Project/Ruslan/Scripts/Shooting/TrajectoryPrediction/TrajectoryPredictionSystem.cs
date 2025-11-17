using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPredictionSystem
{
    private readonly PredictionSceneProvider _sceneProvider;
    private readonly WorldGeometryReplicator _replicator;
    private readonly TrajectorySimulator _simulator;
    private readonly TrajectoryRenderer _renderer;
    private readonly PredictionSceneCleaner _cleaner;
    private bool _geometrySynced = false;

    public TrajectoryPredictionSystem(
        PredictionSceneProvider sceneProvider,
        WorldGeometryReplicator replicator,
        TrajectorySimulator simulator,
        TrajectoryRenderer renderer,
        PredictionSceneCleaner cleaner)
    {
        _sceneProvider = sceneProvider;
        _replicator = replicator;
        _simulator = simulator;
        _renderer = renderer;
        _cleaner = cleaner;
    }

    public void Predict(Vector3 startPosition, Vector3 startVelocity)
    {
        _sceneProvider.EnsureSceneCreated();
            _cleaner.ClearScene();
            _replicator.SyncGeometry();

        List<Vector3> points = _simulator.Simulate(startPosition, startVelocity);
        _renderer.Render(points);
    }

    public void ClearPrediction()
    {
        _cleaner.ClearScene();
        _renderer.Clear();
    }
}
