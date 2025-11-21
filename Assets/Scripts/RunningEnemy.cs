//using System.Collections.Generic;
//using UnityEngine;

//public class RunningEnemy : MonoBehaviour
//{
//    [SerializeField] private float _speed = 2;
//    [SerializeField] private float _rotationSpeed = 10;

//    private InterectObject _targetCabinet;

//    void Update()
//    {
//        if (_targetCabinet != null)
//        {
//            MoveTowardsTarget();
//        }
//    }

//    private void MoveTowardsTarget()
//    {
//        Vector3 direction = _targetCabinet.transform.position - transform.position;

//        if (direction.magnitude <= 0.5f)
//        {
//            _targetCabinet.AcceptEnemy();
//            _targetCabinet = null;
//        }

//        else
//        {
//            Quaternion targetRotation = Quaternion.LookRotation(direction);
//            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
//            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
//        }
//    }

//    public InterectObject TargetCabinet
//    {
//        get => _targetCabinet;
//        set => _targetCabinet = value;
//    }
//}
