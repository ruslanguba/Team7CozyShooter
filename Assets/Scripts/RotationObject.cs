using UnityEngine;

public class RotationObject : MonoBehaviour
{
    [SerializeField] private float _speedRotationX;
    [SerializeField] private float _speedRotationY;
    [SerializeField] private float _speedRotationZ;

    void Update()
    {

        transform.Rotate(_speedRotationX * Time.deltaTime, _speedRotationY * Time.deltaTime, _speedRotationZ * Time.deltaTime);
    }
}
