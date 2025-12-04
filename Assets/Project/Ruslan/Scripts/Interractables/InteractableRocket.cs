using UnityEngine;

public class InteractableRocket : InteractableRigitbodyActivator
{
    [Header("Thrust Settings")]
    [SerializeField] private float _thrustPower = 200f;
    [SerializeField] private float _continuousThrust = 500f;

    [Header("Wobble Settings")]
    [SerializeField] private float _wobbleAmount = 10f;
    [SerializeField] private float _wobbleSpeed = 2f;

    [Header("Other")]
    [SerializeField] private ParticleSystem _particle;

    private bool _hasLaunched = false;
    private float _randomOffsetX;
    private float _randomOffsetY;

    void Start()
    {
        _randomOffsetX = Random.Range(0f, 100f);
        _randomOffsetY = Random.Range(0f, 100f);
    }

    public override void OnInteract()
    {
        base.OnInteract();
        LaunchRocket();
    }

    void FixedUpdate()
    {
        if (_hasLaunched)
        {
            // Синусоидное смещение
            float sineX = Mathf.Sin((Time.time + _randomOffsetX) * _wobbleSpeed);
            float sineY = Mathf.Sin((Time.time + _randomOffsetY) * _wobbleSpeed);

            // Формируем направление
            Vector3 wobbleDir =
                transform.up +
                transform.right * sineX * _wobbleAmount * 0.01f +
                transform.forward * sineY * _wobbleAmount * 0.01f;

            wobbleDir.Normalize();

            // Движение
            _rb.AddForce(wobbleDir * _continuousThrust * Time.fixedDeltaTime, ForceMode.Acceleration);

            // Поворот ракеты в сторону движения
            Quaternion targetRot = Quaternion.LookRotation(wobbleDir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.fixedDeltaTime * 5f);
        }
    }

    private void LaunchRocket()
    {
        _rb.AddForce(transform.up * _thrustPower, ForceMode.Impulse);
        _particle.Play();
        _hasLaunched = true;

        Destroy(gameObject, 20f);
    }
}
