using UnityEngine;

public class StartRocket : MonoBehaviour
{

    [SerializeField] private float thrustPower = 1000f;
    [SerializeField] private ParticleSystem _particle;
    public float continuousThrust = 500f;
    private Rigidbody rocketRigidBody;
    private bool hasLaunched = false;

    void Start()
    {
        rocketRigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            LaunchRocket();
        }
    }
    private void Update()
    {
        if (hasLaunched)
        {
            transform.Rotate(0, 0, 1000);
        }
    }

    void FixedUpdate()
    {
        if (hasLaunched)
        {
            rocketRigidBody.AddForce(transform.forward * continuousThrust * Time.fixedDeltaTime, ForceMode.Acceleration);
            transform.Rotate(0, 0, 1000);
        }
    }

    void LaunchRocket()
    {
        rocketRigidBody.AddForce(transform.forward * thrustPower, ForceMode.Force);        
        _particle.Play();
        Destroy(gameObject, 20f);
    }
}
