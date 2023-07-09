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

    [SerializeField] private MMF_Player _turnVisible, _turnVisibleLogic, _turnNormal;


    public void Initialize(int row)
    {
        int decorationRNG = Random.Range(0, 100);

        if (decorationRNG <= _decorationChance)
        {
            _hasDecoration = true;
            _tileDecorationRenderer.gameObject.SetActive(true);
            _tileDecorationNumber = Random.Range(0, _tileDecorationSpritesArray.Length);
            _tileDecorationRenderer.sprite = _tileDecorationSpritesArray[_tileDecorationNumber];
            _tileSpriteHighlitRenderer.sprite = _tileDecorationSeenSpritesArray[_tileDecorationNumber];
        }
        else
        {
            _hasDecoration = false;
            _tileDecorationRenderer.gameObject.SetActive(false);
        }

        _tilesSpriteNumber = Random.Range(0, _tileSpritesArray.Length);
        _tileSpriteRenderer.sprite = _tileSpritesArray[_tilesSpriteNumber];
        _tileSpriteHighlitRenderer.sprite = _tileSpriteSeenArray[_tilesSpriteNumber];

        _tileSpriteRenderer.sortingOrder = 10 - row;

        _isVisible = false; // just to be sure

    }

    public void IsBecomeVisible(bool newCheck) {
        _newState = newCheck;
    }

    public void OnTileSeen()
    {
        _turnVisible.PlayFeedbacks();
        _isVisible = true;
    }

    public void OnTileNormal()
    {
        //
    }

    public MMF_Player GetSeenTrigger () 
    {
        return _turnVisibleLogic;
    }
}
