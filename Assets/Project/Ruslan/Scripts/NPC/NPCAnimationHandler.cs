using UnityEngine;

public class NPCAnimationHandler : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] bool _isTalk;
    [SerializeField] bool _isGreeting;
    [SerializeField] bool _isApplause;
    [SerializeField] bool _isSwim;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        SetAnim();
    }

    public void SetIdleAnim()
    {
        _animator.SetBool("isTalk", false);
        _animator.SetBool("isGreeting", false);
        _animator.SetBool("isApplause", false);
    }
    public void SetAnim()
    {
        _animator.SetBool("isTalk", _isTalk);
        _animator.SetBool("isGreeting", _isGreeting);
        _animator.SetBool("isApplause", _isApplause);
        _animator.SetBool("isSwim", _isSwim);
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
