using UnityEngine;

public class InterractableBaloon : InteractableBase
{
    private PropsMover _propsMover;

    private void Awake()
    {
        _propsMover= GetComponentInParent<PropsMover>();
    }
    public override void OnInteract()
    {
        _propsMover.GetHit();
    }
}
