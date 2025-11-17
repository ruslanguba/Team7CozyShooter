using System.Collections.Generic;
using UnityEngine;

public class TrajectorySimulator : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _timeStep = 0.2f;
    [SerializeField] private int _maxSteps = 50;
    [SerializeField] private PhysicsObjectsRegistry _physicsObjectsRegistry;
    private Vector3[] _points;
    private LineRenderer _lineRenderer;

    private GameObject simBullet;
    private Rigidbody simRb;
    public bool IsSimulating =>_isSimulating;
    private bool _isSimulating;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        if (_physicsObjectsRegistry != null )
            _physicsObjectsRegistry = GetComponent<PhysicsObjectsRegistry>();
        _points = new Vector3[_maxSteps];

        // создаЄм симул€ционную пулю
        simBullet = Instantiate(_bulletPrefab);
        simRb = simBullet.GetComponent<Rigidbody>();

        simBullet.SetActive(false);
    }

    public void ShowTrajectory(Vector3 origin, Vector3 initialVelocity)
    {
        _isSimulating = true;
        _physicsObjectsRegistry.SaveRigitbodiesData();
        // активируем симул€ционную пулю
        simBullet.transform.position = origin;
        simBullet.transform.rotation = Quaternion.identity;
        simBullet.SetActive(true);

        simRb.linearVelocity = Vector3.zero;
        simRb.angularVelocity = Vector3.zero;

        simRb.AddForce(initialVelocity, ForceMode.VelocityChange);

        Physics.simulationMode = SimulationMode.Script;

        for (int i = 0; i < _maxSteps; i++)
        {
            Physics.Simulate(_timeStep);
            _points[i] = simBullet.transform.position;
        }

        _lineRenderer.positionCount = _maxSteps;
        _lineRenderer.SetPositions(_points);

        Physics.simulationMode = SimulationMode.FixedUpdate;
        _physicsObjectsRegistry.LoadRigitbodiesData();

        simBullet.SetActive(false);
    }

    public void HideTrajectory()
    {
        _isSimulating = false;
        _lineRenderer.positionCount = 0;
    }
}