using UnityEngine;

public class ColorPainter : MonoBehaviour
{
    [SerializeField] private float _speed;
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * _speed, ForceMode.Impulse);
        transform.parent = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Damagable damagable))
        {
            damagable.TakeHit();
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        Destroy(gameObject, 2);
    }
}
