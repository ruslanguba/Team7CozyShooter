using System.Collections;
using UnityEngine;

public class NPCMover : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;

    public float jumpForce = 5f;
    public float forwardForce = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void MoveTovards(Vector3 target, float time)
    {
        StartCoroutine(SmoothMove(target, time));
    }
    private IEnumerator SmoothMove(Vector3 target, float time)
    {
        Vector3 start = transform.position;
        animator.SetBool("isMoving", true);
        float t = 0;

        while (t < time)
        {
            t += Time.deltaTime / time;
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }
        animator.SetBool("isMoving", false);
    }

    public void JumpForward()
    {
        animator.SetTrigger("jump");

        rb.isKinematic = false;
        rb.AddForce(transform.up * jumpForce + transform.forward * forwardForce, ForceMode.VelocityChange);
    }
}
