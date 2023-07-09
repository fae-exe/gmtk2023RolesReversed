using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _tileSpriteRenderer, _tileDecorationRenderer;

    [SerializeField] private int _decorationChance;

    [SerializeField] private Sprite[] _tileSpritesArray = new Sprite[0];
    [SerializeField] private Sprite[] _tileDecorationSpritesArray = new Sprite[0];

    public void Initialize(int row)
    {
        int decorationRNG  = Random.Range(0, 100);

        if (decorationRNG <= _decorationChance)
        {
            _tileDecorationRenderer.gameObject.SetActive(true);
            _tileDecorationRenderer.sprite = _tileDecorationSpritesArray[Random.Range(0, _tileDecorationSpritesArray.Length - 1)];
        }
        else
        {
            _tileDecorationRenderer.gameObject.SetActive(false);
        }

        _tileSpriteRenderer.sprite = _tileSpritesArray[Random.Range(0, _tileSpritesArray.Length - 1)];

        _tileSpriteRenderer.sortingOrder = 10 - row;

    }
}
