using UnityEngine;

public class PlayerSafePosition : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float saveInterval = 0.3f;
    [SerializeField] private float moveToPlatformSpeed = 50;
    [SerializeField] private PlayerContext playerContext;
    private CharacterController characterController;

    [SerializeField] private Vector3 lastSafePos;
    private float timer;
    private bool isMovingToPlatform = false;

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

        //if (isMovingToPlatform)
        //{
        //    Vector3 delta = lastSafePos - player.position;
        //    MoveToPlatform(delta);
        //}
    }

    public void ReturnPlayerToPlatform()
    {
        playerContext.PlayerGravity.SetYVelocity(0);
        playerContext.PlayerMovement.ResetHorizontalVelocity();
        characterController.enabled = false;

        player.transform.position = new Vector3(lastSafePos.x, lastSafePos.y + 1, lastSafePos.z);
        characterController.enabled = true;
    }

    //private void MoveToPlatform(Vector3 delta)
    //{
    //    if (isMovingToPlatform)
    //    {
    //        if (player.transform.position.y <= lastSafePos.y)
    //        {
    //            playerContext.PlayerMovement.ApplyMovement(player.up * moveToPlatformSpeed);
    //            playerContext.PlayerGravity.SetYVelocity(0);
    //        }
    //        else
    //        {
    //            playerContext.PlayerMovement.ApplyMovement(delta.normalized * moveToPlatformSpeed);
    //            float dist = Vector3.Distance(lastSafePos, player.position);
    //            if (dist <= 1f)
    //            {
    //                player.transform.position = lastSafePos;
    //                playerContext.PlayerGravity.SetMoveUpwords(false);
    //                playerContext.PlayerMovement.ResetHorizontalVelocity();
    //                isMovingToPlatform = false;
                    
    //            }
    //        }
    //    }
    //}
}
