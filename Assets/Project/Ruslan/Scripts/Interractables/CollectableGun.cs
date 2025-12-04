using UnityEngine;

public class CollectableGun : Collectable
{
    [SerializeField] private PillowGun pillowGun;
    override protected void Awake()
    {
        base.Awake();
        pillowGun.Deactivate();
        pillowGun.enabled = false;
    }

    public override void Collect(Transform target)
    {
        base.Collect(target);
        pillowGun.Activate();
        pillowGun.enabled = true;
    }
}
