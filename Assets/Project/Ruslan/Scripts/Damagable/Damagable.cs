using UnityEngine;

public abstract class Damagable : MonoBehaviour
{
    [SerializeField] protected float _maxHitPoints;
    private float _hitPoints;

    private void Awake()
    {
        _hitPoints = _maxHitPoints;
    }

    public abstract void TakeHit();
}
