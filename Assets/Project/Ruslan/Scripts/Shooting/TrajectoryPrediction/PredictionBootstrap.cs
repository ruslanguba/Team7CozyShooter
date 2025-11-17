using UnityEngine;

public class PredictionBootstrap : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private GameObject _projectilePrefab;

    private TrajectoryPredictionSystem _system;

    private void Awake()
    {
        var sceneProvider = new PredictionSceneProvider();
        var cleaner = new PredictionSceneCleaner(sceneProvider);
        var replicator = new WorldGeometryReplicator(sceneProvider);
        var simulator = new TrajectorySimulator(sceneProvider, _projectilePrefab);
        var renderer = new TrajectoryRenderer(_lineRenderer);

        _system = new TrajectoryPredictionSystem(sceneProvider, replicator, simulator, renderer, cleaner);
    }

    public void Predict(Vector3 pos, Vector3 vel)
    {
        _system.Predict(pos, vel);
    }

    public void Clear()
    {
        _system.ClearPrediction();
        Physics.simulationMode = SimulationMode.FixedUpdate;
    }
}
