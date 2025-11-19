using UnityEngine;

public class AnimatorHandler
{
    private readonly Animator _animator;

    private static readonly int SpeedHash = Animator.StringToHash("speed");
    private static readonly int IsMovingHash = Animator.StringToHash("isMoving");
    private static readonly int IsJumpHash = Animator.StringToHash("isJump");
    private static readonly int AttackTriggerHash = Animator.StringToHash("attack");
    private static readonly int IsBackwardHash = Animator.StringToHash("isBackward");
    private static readonly int MoveDirHash = Animator.StringToHash("moveDirection");
    private static readonly int AnimSpeedMultiplierHash = Animator.StringToHash("animSpeedMultiplier");

    public AnimatorHandler(Animator animator)
    {
        _animator = animator;
    }
    public void SetSpeed(float speed) => _animator.SetFloat(SpeedHash, Mathf.Abs(speed));
    public void SetMoving(bool moving) => _animator.SetBool(IsMovingHash, moving);
    public void SetBackward(bool backward) => _animator.SetBool(IsBackwardHash, backward);
    public void SetJump() => _animator.SetTrigger(IsJumpHash);
    public void PlayAttack() => _animator.SetTrigger(AttackTriggerHash);
    public void SetMoveDirection(int dir) => _animator.SetInteger(MoveDirHash, dir);
    public void SetAnimSpeedMultiplier(float multiplier) => _animator.SetFloat(AnimSpeedMultiplierHash, multiplier);

}
