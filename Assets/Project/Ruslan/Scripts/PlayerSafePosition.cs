using UnityEngine;

public class PlayerSafePosition : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float saveInterval = 0.3f;

    [SerializeField] private Transform _checkSurfacePivot;
    [SerializeField] private float radius = 0.3f;

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
        if (characterController.isGrounded && !IsStandingOnPillow())
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

    bool IsStandingOnPillow(float distance = 1f)
    {
        Collider[] hits = Physics.OverlapSphere(_checkSurfacePivot.position, radius);

        foreach (var hit in hits)
        {
            if (hit.GetComponent<Pillow>() != null)
                return true;   // Нашли Pillow
        }

        return false; // Ничего подходящего нет
    }
}
