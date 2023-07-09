using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class MapTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _tileSpriteRenderer, _tileDecorationRenderer, _tileSpriteHighlitRenderer, _tileDecorationHighlitRenderer;

    [SerializeField] private int _decorationChance;

    [SerializeField] private Sprite[] _tileSpritesArray = new Sprite[0];
    [SerializeField] private Sprite[] _tileSpriteSeenArray = new Sprite[0];

    [SerializeField] private Sprite[] _tileDecorationSpritesArray = new Sprite[0];
    [SerializeField] private Sprite[] _tileDecorationSeenSpritesArray = new Sprite[0];

    [SerializeField] private int _tilesSpriteNumber, _tileDecorationNumber;
    [SerializeField] private bool _isVisible, _newState, _hasDecoration;

    [SerializeField] private MMF_Player _turnVisible, _turnVisibleLogic, _turnNormal, _decorationTurnNormal, _decorationTurnSeen;

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        Unsubscribe();
        GameManager.OnGameStateChanged += OnPlayerTurn;
    }

    private void Unsubscribe()
    {
        GameManager.OnGameStateChanged -= OnPlayerTurn;
    }

    public void Initialize(int row)
    {
        int decorationRNG = Random.Range(0, 100);

        if (decorationRNG <= _decorationChance)
        {
            _hasDecoration = true;
            
            _tileDecorationRenderer.gameObject.SetActive(true);
            _tileDecorationHighlitRenderer.gameObject.SetActive(true);

            _tileDecorationNumber = Random.Range(0, _tileDecorationSpritesArray.Length);

            _tileDecorationRenderer.sprite = _tileDecorationSpritesArray[_tileDecorationNumber];
            _tileDecorationHighlitRenderer.sprite = _tileDecorationSeenSpritesArray[_tileDecorationNumber];
        }
        else
        {
            _hasDecoration = false;

            _tileDecorationRenderer.gameObject.SetActive(false);
            _tileDecorationHighlitRenderer.gameObject.SetActive(false);
        }

        _tilesSpriteNumber = Random.Range(0, _tileSpritesArray.Length);

        _tileSpriteRenderer.sprite = _tileSpritesArray[_tilesSpriteNumber];
        _tileSpriteHighlitRenderer.sprite = _tileSpriteSeenArray[_tilesSpriteNumber];

        _tileSpriteRenderer.sortingOrder = 10 - row;
        _tileSpriteHighlitRenderer.sortingOrder = 10 - row;

        _isVisible = false; // just to be sure

    }

    public void IsBecomeVisible(bool newCheck) {
        //
    }

    public void OnTileSeen()
    {
        if (_isVisible == true)
        {
            return;
        }

        if (_hasDecoration) { _decorationTurnSeen.PlayFeedbacks(); }

        _turnVisible.PlayFeedbacks();

        _isVisible = true;
    }

    public void OnPlayerTurn(GameState newState)
    {
        if (newState != GameState.PlayerTurn)
        {
            return;
        }

        if (_isVisible == false)
        {
            return;
        }

        _turnNormal.PlayFeedbacks();
        if (_hasDecoration) { _decorationTurnNormal.PlayFeedbacks(); }

        _isVisible = false;
    }

    public MMF_Player GetSeenTrigger ()
    {
        return _turnVisibleLogic;
    }
}
