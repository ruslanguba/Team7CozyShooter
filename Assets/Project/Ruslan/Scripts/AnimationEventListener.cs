using System;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    public event Action OnAnimationEnded;

    public void AnimationEndedAction()
    {
        OnAnimationEnded?.Invoke();
    }
}
