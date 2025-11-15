using UnityEngine;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    public abstract void OnInteract();
}
