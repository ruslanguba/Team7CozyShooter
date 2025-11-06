using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Scriptable Objects/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float acceleration = 20f;
    public float airSpeedMultiplier;

    [Header("Jump")]
    public float jumpHeight = 2f;

    [Header("Gravity")]
    public float gravity = -9.81f;

    [Header("Look")]
    public float lookSensitivity = 1f;
    public float minPitch = -80f;
    public float maxPitch = 80f;
    public float lookSmoothTime = 0.05f;
}
