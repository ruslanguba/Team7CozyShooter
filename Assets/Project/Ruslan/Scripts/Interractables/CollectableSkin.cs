using UnityEngine;

public class CollectableSkin : Collectable
{
    [SerializeField] private int _skinIndex;

    public override void Collect(Transform target)
    {
        base.Collect(target);
        target.GetComponentInParent<SkinHandler>().SetSkin(_skinIndex);
    }
}
