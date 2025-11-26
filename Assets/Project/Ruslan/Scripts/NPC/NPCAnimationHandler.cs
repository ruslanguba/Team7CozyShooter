using UnityEngine;

public class NPCAnimationHandler : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void SetIdleAnim()
    {
        _animator.SetBool("isTalk", false);
        _animator.SetBool("isGreeting", false);
        _animator.SetBool("isApplause", false);
    }

    public void SetisApplauseAnim()
    {
        _animator.SetBool("isApplause", true);
    }

    public void SetisGreetingAnim()
    {
        _animator.SetBool("isGreeting", true);
    }

    public void SetTalkAnim()
    {
        _animator.SetBool("isTalk", true);
    }
}
