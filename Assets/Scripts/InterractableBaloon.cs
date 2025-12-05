using UnityEngine;

public class InterractableBaloon : InteractableBase
{
    [SerializeField] private AudioClip _clip;
    private PropsMover _propsMover;

    private void Awake()
    {
        _propsMover= GetComponentInParent<PropsMover>();
    }
    public override void OnInteract()
    {
        AudioManager.Instance.PlaySFX( _clip );
        _propsMover.GetHit();
    }
}
