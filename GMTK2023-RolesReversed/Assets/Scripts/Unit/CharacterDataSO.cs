using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterData",menuName = "CharacterData",order = 1)]
public class CharacterDataSO : ScriptableObject
{
    public CharacterState characterState;

    public Sprite northSprite;
    public Sprite eastSprite;
    public Sprite southSprite;
    public Sprite westSprite;

    public Sprite GetSprite(Direction orientation)
    {
        switch (orientation) {
            case Direction.Up: { return northSprite; }
            case Direction.Right: { return eastSprite; }
            case Direction.Down: { return southSprite; }
            case Direction.Left: { return westSprite; }
        }

        return eastSprite;
    }

}

public enum CharacterState { NORMAL, ANGRY }