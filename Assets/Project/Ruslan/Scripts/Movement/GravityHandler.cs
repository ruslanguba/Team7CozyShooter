using UnityEngine;

public class GravityHandler : MonoBehaviour
{
    [SerializeField] private float _gravity;

    private CharacterController _controller;
    private Vector3 _velocity;

    public Vector3 VerticalVelocity => _velocity;
    public float GetGravity => _gravity;


    private void Awake()
    {
        _controller = GetComponent<CharacterController>(); 
    }

    private void Update()
    {
        UpdateGravity();
    }

    public void UpdateGravity()
    {
        if (_controller.isGrounded && _velocity.y < 0f)
            _velocity.y = -2f;

        _velocity.y += _gravity * Time.fixedDeltaTime;
    }

    public void SetYVelocity(float value)
    {
        _velocity.y = value;
    }

    public float GetYVelocity()
    {
        return _velocity.y;
    }
}
