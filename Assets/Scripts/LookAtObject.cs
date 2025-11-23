using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    [SerializeField] private GameObject _object;

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _object.transform.position);
    }
}
