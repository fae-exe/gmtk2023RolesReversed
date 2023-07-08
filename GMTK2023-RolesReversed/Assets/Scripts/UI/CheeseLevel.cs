using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class CheeseLevel : MonoBehaviour
{
    [SerializeField] MMF_Player _showLevel, _hideLevel, _initHide;
    
    AnimationQueue _cheeseLevelAnimationQueue;

    private CheeseStates _currentState = CheeseStates.Hidden;

    public void Initialize ()
    {
        _cheeseLevelAnimationQueue = new AnimationQueue();
        _currentState = CheeseStates.Hidden;
        _initHide.PlayFeedbacks();
    }

    public void Initialize(AnimationQueue animationQueue) { 
        _cheeseLevelAnimationQueue = animationQueue;
        _currentState = CheeseStates.Hidden;
        _initHide.PlayFeedbacks();
    }

    private void InstantStateUpdate()
    {
        if (_currentState == CheeseStates.Shown)
        {
            _showLevel.Initialization();
            _showLevel.PlayFeedbacks();
            _showLevel.SkipToTheEnd();
        }

        else if (_currentState == CheeseStates.Hidden)
        {
            _hideLevel.Initialization();
            _hideLevel.PlayFeedbacks();
            _hideLevel.SkipToTheEnd();
        }
    }

    public void Show()
    {
        if (_currentState == CheeseStates.Shown) { return; }
        else if (_currentState == CheeseStates.Hidden) {
            _currentState = CheeseStates.Shown;
            _cheeseLevelAnimationQueue.Enqueue(_showLevel); 
        }
    }

    public void Hide()
    {
        if (_currentState == CheeseStates.Hidden) { return; }
        else if (_currentState == CheeseStates.Shown) {
            _currentState = CheeseStates.Hidden;
            _cheeseLevelAnimationQueue.Enqueue(_hideLevel); 
        }
    }

}

public enum CheeseStates { Hidden = 0, Shown = 1 }