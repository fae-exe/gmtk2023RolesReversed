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


    #region Starts    
    private void OnEnable() {
        MoveController.OnPlayerDirection += OnPlayerDirection;
        MoveController.OnPlayerMove += OnPlayerMove;
        MoveController.OnPlayerBlocked += OnPlayerBlocked;

        PlayerManager.OnGetDaCheese += OnGetDaCheese;
        PlayerManager.OnFuryChange += OnFuryChange;
        PlayerManager.OnSmashingEnnemy += OnSmashingEnnemy;
    }

    private void OnDisable() {
        MoveController.OnPlayerDirection -= OnPlayerDirection;
        MoveController.OnPlayerMove -= OnPlayerMove;
        MoveController.OnPlayerBlocked -= OnPlayerBlocked;
        
        PlayerManager.OnGetDaCheese -= OnGetDaCheese;
        PlayerManager.OnFuryChange -= OnFuryChange;
        PlayerManager.OnSmashingEnnemy -= OnSmashingEnnemy;
    }
    #endregion

    
    #region Player Event
    // Movement events below
    private void OnPlayerDirection(Direction newOrientation) {
        if (newOrientation == _characterOrientation) { return; }

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

    private void OnPlayerMove()
    {
        _movementFeedback.PlayFeedbacks();
    }   

    private void OnPlayerBlocked()
    {
        _cantMove.PlayFeedbacks();
    }  

    // Cheese and attack event
    private void OnGetDaCheese() {

    }  

    private void OnFuryChange(bool isOnFury) {
        if (isOnFury)
        {
            _currentState = CharacterState.ANGRY;
        }
        
        else
        {
            _currentState = CharacterState.NORMAL;
        }

        _curentCharacterData = _characterList.GetData(_currentState);
        UpdateSprite();
    } 

    private void OnSmashingEnnemy(GameObject ennemyObject) {

    }  
    #endregion

    public void UpdateSprite()
    {
        _curentCharacterData = _characterList.GetData(_currentState);
        _characterSpriteRenderer.sprite = _curentCharacterData.GetSprite(_characterOrientation);
    }
}
