using System.Collections.Generic;
using UnityEngine;

public class PhysicsObjectsRegistry : MonoBehaviour
{
    public static PhysicsObjectsRegistry Instance { get; private set; }

    public TrajectorySimulator TrajectorySimulator;
    public List<Collider> AllColliders = new List<Collider>();
    public List<Rigidbody> AllRigidbodies = new List<Rigidbody>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        RegisterAllObjects();
    }

    private void RegisterAllObjects()
    {
        // Найти ВСЕ коллайдеры сцены
        AllColliders.AddRange(FindObjectsByType<Collider>(0));

        // Найти ВСЕ Rigidbody
        AllRigidbodies.AddRange(FindObjectsByType<Rigidbody>(0));
        Debug.Log($"[Registry] Found {AllColliders.Count} colliders, {AllRigidbodies.Count} rigidbodies");
    }
}
