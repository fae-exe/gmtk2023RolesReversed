using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _tileSpriteRenderer, _tileDecorationRenderer;

    [SerializeField] private int _decorationChance;

    [SerializeField] private Sprite[] _tileSpritesArray = new Sprite[0];
    [SerializeField] private Sprite[] _tileDecorationSpritesArray = new Sprite[0];

    [SerializeField] private int _tilesSpriteNumber;
    [SerializeField] private bool _isVisible;


    public void Initialize(int row)
    {
        int decorationRNG  = Random.Range(0, 100);

        if (decorationRNG <= _decorationChance)
        {
            _tileDecorationRenderer.gameObject.SetActive(true);
            _tileDecorationRenderer.sprite = _tileDecorationSpritesArray[Random.Range(0, _tileDecorationSpritesArray.Length)];
        }
        else
        {
            _tileDecorationRenderer.gameObject.SetActive(false);
        }

        _tilesSpriteNumber = Random.Range(0, _tileSpritesArray.Length);
        _tileSpriteRenderer.sprite = _tileSpritesArray[_tilesSpriteNumber];

        _tileSpriteRenderer.sortingOrder = 10 - row;

        _isVisible = false; // just to be sure

    }

    public void IsBecomeVisible(bool newCheck) {
        if(_isVisible != newCheck) { // Check if change in box state
            if(_isVisible) { // if becoming non visible, remove length of tileArray and get non highlighted tile
                _tilesSpriteNumber -= _tileSpritesArray.Length;
                _tileSpriteRenderer.sprite = _tileSpritesArray[_tilesSpriteNumber];
                _isVisible = false;
            } else { // if becoming visible, add length of tileArray and get highlighted tile
                _tilesSpriteNumber += _tileSpritesArray.Length;
                _tileSpriteRenderer.sprite = _tileSpritesArray[_tilesSpriteNumber];
                _isVisible = true;
            }
        }

    }
}
