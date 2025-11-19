using UnityEngine;

public class PlayerSafePosition : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float saveInterval = 0.3f;
    [SerializeField] private PlayerContext playerContext;
    private CharacterController characterController;

    [SerializeField] private Vector3 lastSafePos;
    private float timer;

    void Start()
    {
        characterController = GetComponentInParent<CharacterController>();
        lastSafePos = player.position;
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                lastSafePos = player.position;
                timer = saveInterval;
            }
        }
    }

    public void ReturnPlayerToPlatform()
    {
        characterController.enabled = false;

        player.transform.position = new Vector3(lastSafePos.x, lastSafePos.y + 1, lastSafePos.z);
        characterController.enabled = true;
    }
}
