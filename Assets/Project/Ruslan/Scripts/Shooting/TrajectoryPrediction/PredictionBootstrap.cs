using UnityEngine;

public class PredictionBootstrap : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private StaticObjectsRegistry _staticObjectsRegistry;
    [SerializeField] private DynamicObjectsRegistry _dynamicObjectsRegistry;
    private TrajectoryPredictionSystem _system;

    private void Awake()
    {
        _staticObjectsRegistry = GetComponent<StaticObjectsRegistry>();
        _dynamicObjectsRegistry = GetComponent<DynamicObjectsRegistry>();
        var sceneProvider = new PredictionSceneProvider();
        var cleaner = new PredictionSceneCleaner(sceneProvider);
        var replicator = new WorldGeometryReplicator(sceneProvider, _staticObjectsRegistry, _dynamicObjectsRegistry);
        var simulator = new TrajectorySimulator(sceneProvider, _projectilePrefab);
        var renderer = new TrajectoryRenderer(_lineRenderer);

        _system = new TrajectoryPredictionSystem(sceneProvider, replicator, simulator, renderer, cleaner);
        _staticObjectsRegistry.SetPredictionSceneProvider(sceneProvider);
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
