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

    void FixedUpdate()
    {
        if (hasLaunched)
        {
            rocketRigidBody.AddForce(transform.forward * continuousThrust * Time.fixedDeltaTime, ForceMode.Force);
            transform.Rotate(0, 0, 600);
        }
    }

    void LaunchRocket()
    {
        rocketRigidBody.AddForce(transform.forward * thrustPower, ForceMode.Acceleration);        
        _particle.Play();
    }
}
