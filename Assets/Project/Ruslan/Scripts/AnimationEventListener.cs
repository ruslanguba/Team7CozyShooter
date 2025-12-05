using System;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    public event Action OnAnimationJump;

    public void AnimationAction()
    {
        OnAnimationJump?.Invoke();
    }
}
