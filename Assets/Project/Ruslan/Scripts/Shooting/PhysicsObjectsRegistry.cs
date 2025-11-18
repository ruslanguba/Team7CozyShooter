using System.Collections.Generic;
using UnityEngine;

public class PhysicsObjectsRegistry : MonoBehaviour
{
    public class BodyData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 velocity;
        public Vector3 angularVelocity;
    }

    //public Dictionary<Rigidbody, BodyData> Bodies => _bodiesData;
    private Dictionary<Rigidbody, BodyData> _bodiesData = new Dictionary<Rigidbody, BodyData>();
    private void Awake()
    {
        RegisterRigitbodiesOnScine();
    }

    private void RegisterRigitbodiesOnScine()
    {
        foreach (var rb in FindObjectsByType<Rigidbody>(0))
        {
            _bodiesData.Add(rb, new BodyData());
        }
    }

    public void RegisterNewRigitbody(Rigidbody rb)
    {
        _bodiesData.Add(rb, new BodyData());
    }

    public void DeleateRigitbody(Rigidbody rb)
    {
        _bodiesData.Remove(rb);
    }

    public void SaveRigitbodiesData()
    {
        foreach (var rb in _bodiesData)
        {
            rb.Value.position = rb.Key.transform.position;
            rb.Value.rotation = rb.Key.transform.rotation;
            rb.Value.velocity = rb.Key.linearVelocity;
            rb.Value.angularVelocity = rb.Key.angularVelocity;
        }
    }

    public void LoadRigitbodiesData()
    {
        foreach (var rb in _bodiesData)
        {
            rb.Key.transform.position = rb.Value.position;
            rb.Key.transform.rotation = rb.Value.rotation;
            rb.Key.linearVelocity = rb.Value.velocity;
            rb.Key.angularVelocity = rb.Value.angularVelocity;
        }
    }
}
