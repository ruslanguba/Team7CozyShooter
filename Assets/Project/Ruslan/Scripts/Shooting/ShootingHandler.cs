using UnityEngine;

public class ShootingHandler : MonoBehaviour
{
    [SerializeField] private Transform _shootingPivot;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _shootingForce;
    [SerializeField] private float _shootingRate;
    [SerializeField] private Guns _currentGun;
    [SerializeField] private PlayerArmoury _armoury;

    private float _shootingTimer;
    private InputReader input;


    private void Awake()
    {
        input = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        input.OnFire += OnFireAction;
    }

    private void OnDisable()
    {
        input.OnFire -= OnFireAction;
    }

    private void OnFireAction()
    {
        if(IsCanShoot())
        {
            GameObject newBullet = Instantiate(_bulletPrefab, _shootingPivot);
            //Rigidbody bulletRigitBody = newBullet.GetComponent<Rigidbody>();
            //bulletRigitBody.AddForce(transform.forward * _shootingForce, ForceMode.Impulse);
            _shootingTimer = _shootingRate;
        }
    }

    private bool IsCanShoot()
    {
        return _shootingTimer <= 0;
    }

    private void Update()
    {
        if (_shootingTimer >= 0)
        {
            _shootingTimer -= Time.deltaTime;
        }
    }
}
