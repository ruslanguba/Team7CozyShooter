using UnityEngine;

public interface IInteractablePlace
{
    void OpenDoor();
    void CloseDoor();
    void SetEnemyVisibility(bool visible);
}
