using UnityEngine;

public class PoolBullet : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolCount;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform poolParent;
    //[SerializeField] private float _shootingRate;

    private GameObject[] poolEffect;
    private RaycastHit hit;
    private int currentPoolIndex = 0;
    //private float _shootingTimer;

    void Start()
    {
        poolParent = transform;
        for (int i = 0; i < _poolCount; i++)
        {
            Instantiate(_prefab, transform.position, transform.rotation, poolParent);
        }

        poolEffect = new GameObject[_poolCount];
        for(int i = 0; i < _poolCount; i++)
        {
            poolEffect[i] = Instantiate(_prefab, transform.position, transform.rotation, poolParent);
            poolEffect[i].SetActive(false);
        }
    }
   
    void Update()
    {
        //if (_shootingTimer >= 0)
        //{
        //    _shootingTimer -= Time.deltaTime;
        //}

        if (Input.GetMouseButtonDown(0))
        {
            //if (IsCanShoot())
            //{
                if (Physics.Raycast(_camera.position, _camera.TransformDirection(Vector3.forward), out hit, 200))
                {
                    GameObject obj = poolParent.GetChild(currentPoolIndex).gameObject;
                    obj.SetActive(true);
                    obj.transform.position = hit.point + hit.normal * 0.01f;
                    obj.transform.rotation = Quaternion.Euler(0, 0, 0);
                    obj.transform.rotation = Quaternion.FromToRotation(obj.transform.up, hit.normal);
                    //obj.GetComponent<ParticleSystem>().Emit(200);
                    currentPoolIndex++;
                    if (currentPoolIndex > poolParent.childCount - 1) currentPoolIndex = 0;
                    //_shootingTimer = _shootingRate;
                }
            //}
        }
    }

    //private bool IsCanShoot()
    //{
    //    return _shootingTimer <= 0;
    //}
}
