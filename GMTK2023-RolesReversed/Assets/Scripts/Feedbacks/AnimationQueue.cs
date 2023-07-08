using System;
using ScriptableEvents;
using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationQueue : MonoBehaviour
{
    [SerializeField] private List<MMF_Player> _animationStack = new List<MMF_Player>();
    public bool IsPlaying = false;
    [SerializeField] private GameEvent _onEmptyAnimationQueue;

    private void OnDestroy()
    {
        foreach (var anim in _animationStack)
            anim.Events.OnComplete.RemoveListener(AnimationEnded);
    }

    public void Clear()
    {
        foreach (var anim in _animationStack)
            anim.Events.OnComplete.RemoveListener(AnimationEnded);

        _animationStack.Clear();
    }

    public void Enqueue(MMF_Player newAnim)
    {
        _animationStack.Add(newAnim);
        PlayNext();
    }

    public void PlayNext()
    {

        if (_animationStack.Count == 0)
        {
            _onEmptyAnimationQueue?.Raise();
            IsPlaying = false;
            return;
        }

        if (IsPlaying)
        {
            return;
        }

        _animationStack[0].Events.OnComplete.RemoveListener(AnimationEnded);
        _animationStack[0].Events.OnComplete.AddListener(AnimationEnded);
        _animationStack[0].PlayFeedbacks();
        IsPlaying = true;
    }

    public void AnimationEnded()
    {
        IsPlaying = false;
        _animationStack[0].Events.OnComplete.RemoveListener(AnimationEnded);
        _animationStack.RemoveAt(0);
        PlayNext();
    }
}
