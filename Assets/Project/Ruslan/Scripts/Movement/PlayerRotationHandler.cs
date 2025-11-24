using UnityEngine;

public class PlayerRotationHandler : MonoBehaviour
{
    public void UpdateRotation(Vector3 direction, float rotationSpeed)
    {
        if (direction.sqrMagnitude < 0.01f)
            return;

        Quaternion targetRot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }
}
