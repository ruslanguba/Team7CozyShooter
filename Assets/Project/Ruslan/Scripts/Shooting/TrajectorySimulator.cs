using System.Collections.Generic;
using UnityEngine;

public class TrajectorySimulator : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _timeStep = 0.2f;
    [SerializeField] private int _maxSteps = 300;

    private Vector3[] _points;
    private LineRenderer _lineRenderer;

    private GameObject simBullet;
    private Rigidbody simRb;

    private Dictionary <Rigidbody, BodyData> _bodies = new Dictionary<Rigidbody, BodyData>();

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _points = new Vector3[_maxSteps];

        // создаЄм симул€ционную пулю
        simBullet = Instantiate(_bulletPrefab);
        simRb = simBullet.GetComponent<Rigidbody>();

        simBullet.SetActive(false);

        foreach (var rb in FindObjectsByType<Rigidbody>(0))
        {
            _bodies.Add(rb, new BodyData());
        }
    }

    public void ShowTrajectory(Vector3 origin, Vector3 initialVelocity)
    {
        foreach (var rb in _bodies) 
        {
            rb.Value.position = rb.Key.transform.position;
            rb.Value.rotation = rb.Key.transform.rotation;
            rb.Value.velocity = rb.Key.linearVelocity;
            rb.Value.angularVelocity = rb.Key.angularVelocity;
        }
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

        foreach (var rb in _bodies)
        {
            rb.Key.transform.position = rb.Value.position;
            rb.Key.transform.rotation = rb.Value.rotation;
            rb.Key.linearVelocity = rb.Value.velocity;
            rb.Key.angularVelocity = rb.Value.angularVelocity;
        }
        simBullet.SetActive(false);
    }

    public void HideTrajectory()
    {
        _lineRenderer.positionCount = 0;
    }
}

public class BodyData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
    public Vector3 angularVelocity;
}