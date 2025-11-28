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
        SetStartAnim();
    }

    public void SetIdleAnim()
    {
        _animator.SetBool("isTalk", false);
        _animator.SetBool("isGreeting", false);
        _animator.SetBool("isApplause", false);
    }
    public void SetStartAnim()
    {
        _animator.SetBool("isTalk", _isTalk);
        _animator.SetBool("isGreeting", _isGreeting);
        _animator.SetBool("isApplause", _isApplause);
        _animator.SetBool("isSwim", _isSwim);
    }
    public void SetisApplauseAnim()
    {
        _animator.SetBool("isApplause", true);
        _animator.SetBool("isTalk", false);
        _animator.SetBool("isGreeting", false);
    }

    public void SetGreetingAnim()
    {
        _animator.SetBool("isGreeting", true);
        _animator.SetBool("isTalk", false);
        _animator.SetBool("isApplause", false);
    }

    public void SetTalkAnim()
    {
        _animator.SetBool("isTalk", true);
        _animator.SetBool("isGreeting", false);
        _animator.SetBool("isApplause", false);
    }
}
