using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class MovementFeebdack : MonoBehaviour
{
    [SerializeField] private CharacterListSO _characterList;
    [SerializeField] private CharacterState _currentState = CharacterState.NORMAL;
    [SerializeField] private Direction _characterOrientation = Direction.Right;

    [SerializeField] private SpriteRenderer _characterSpriteRenderer;

    [SerializeField] private AnimationQueue _characterAnimationQueue;

    [SerializeField] private MMF_Player _hideSpriteHorizontal, _hideSpriteVertical, _showSprite, _updateSpriteLogic, _movementFeedback, _cantMove;

    private CharacterDataSO _curentCharacterData;

    public void OnStateChanged(CharacterState newState)
    {
        _currentState = newState;
        _curentCharacterData = _characterList.GetData(newState);
        UpdateSprite();
    }

    public void UpdateSprite ()
    {
        _characterSpriteRenderer.sprite = _curentCharacterData.GetSprite(_characterOrientation);
    }

    public void OnOrientationChanged (Direction newOrientation)
    {
        if (_characterOrientation == Direction.Down || _characterOrientation == Direction.Up)
        {
            _characterAnimationQueue.Enqueue(_hideSpriteVertical);
        }
        else
        {
            _characterAnimationQueue.Enqueue(_hideSpriteHorizontal);
        }

        _characterOrientation = newOrientation;

        _characterAnimationQueue.Enqueue(_updateSpriteLogic);
        _characterAnimationQueue.Enqueue(_showSprite);
    }

    public void OnCharacterMoved ()
    {
        _movementFeedback.PlayFeedbacks();
    }

    public void OnCharacterCantMove()
    {
        _cantMove.PlayFeedbacks();
    }
}
