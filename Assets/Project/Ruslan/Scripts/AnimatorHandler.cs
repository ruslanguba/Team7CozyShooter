using UnityEngine;

public class AnimatorHandler
{
    private readonly Animator _animator;
    private readonly PlayerContext _playerContext;

    private static readonly int SpeedHash = Animator.StringToHash("speed");
    private static readonly int IsMovingHash = Animator.StringToHash("isMoving");
    private static readonly int IsJumpHash = Animator.StringToHash("isJump");
    private static readonly int AttackTriggerHash = Animator.StringToHash("attack");
    private static readonly int IsBackwardHash = Animator.StringToHash("isBackward");
    private static readonly int MoveDirHash = Animator.StringToHash("moveDirection");
    private static readonly int AnimSpeedMultiplierHash = Animator.StringToHash("animSpeedMultiplier");

    //public AnimatorHandler(Animator animator)
    //{
    //    _animator = animator;
    //}
    public AnimatorHandler(PlayerContext playerContext)
    {
        _playerContext = playerContext;
    }

    public void SetSpeed(float speed) => _playerContext.Animator.SetFloat(SpeedHash, Mathf.Abs(speed));
    public void SetMoving(bool moving) => _playerContext.Animator.SetBool(IsMovingHash, moving);
    public void SetBackward(bool backward) => _playerContext.Animator.SetBool(IsBackwardHash, backward);
    public void SetJump() => _playerContext.Animator.SetTrigger(IsJumpHash);
    public void PlayAttack() => _playerContext.Animator.SetTrigger(AttackTriggerHash);
    public void SetMoveDirection(int dir) => _playerContext.Animator.SetInteger(MoveDirHash, dir);
    public void SetAnimSpeedMultiplier(float multiplier) => _playerContext.Animator.SetFloat(AnimSpeedMultiplierHash, multiplier);

    //public void SetSpeed(float speed) => _animator.SetFloat(SpeedHash, Mathf.Abs(speed));
    //public void SetMoving(bool moving) => _animator.SetBool(IsMovingHash, moving);
    //public void SetBackward(bool backward) => _animator.SetBool(IsBackwardHash, backward);
    //public void SetJump() => _animator.SetTrigger(IsJumpHash);
    //public void PlayAttack() => _animator.SetTrigger(AttackTriggerHash);
    //public void SetMoveDirection(int dir) => _animator.SetInteger(MoveDirHash, dir);
    //public void SetAnimSpeedMultiplier(float multiplier) => _animator.SetFloat(AnimSpeedMultiplierHash, multiplier);

}
