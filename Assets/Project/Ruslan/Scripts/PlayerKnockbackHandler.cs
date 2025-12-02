using UnityEngine;

public class PlayerKnockbackHandler : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 knockVelocity;
    private float knockDecay = 10f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void AddKnockback(Vector3 direction, float force)
    {
        direction.y = 0; // чтобы не подбрасывало вверх
        knockVelocity = direction.normalized * force;
    }

    private void Update()
    {
        if (knockVelocity.magnitude > 0.1f)
        {
            controller.Move(knockVelocity * Time.deltaTime);
            knockVelocity = Vector3.Lerp(knockVelocity, Vector3.zero, Time.deltaTime * knockDecay);
        }
    }
}
