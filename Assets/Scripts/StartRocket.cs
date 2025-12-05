using UnityEngine;

public class StartRocket : MonoBehaviour
{

    [SerializeField] private float _thrustPower = 200f;
    [SerializeField] private float _continuousThrust = 500f;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ParticleSystem _particle;
    

    private Rigidbody rocketRigidBody;
    private bool hasLaunched = false;

    void Start()
    {
        rocketRigidBody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
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
            transform.Rotate(0, 0, 10);
        }
    }

    void FixedUpdate()
    {
        if (hasLaunched)
        {
            rocketRigidBody.AddForce(transform.forward * _continuousThrust * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    void LaunchRocket()
    {
        rocketRigidBody.AddForce(transform.forward * _thrustPower, ForceMode.Force);        
        _particle.Play();
        hasLaunched = true;
        _audioSource.Play();
        Destroy(gameObject, 20f);
    }
}