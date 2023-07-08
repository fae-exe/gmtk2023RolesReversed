using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyUnitScript : MonoBehaviour
{

    public EnnemyType ennemyType;

    public Direction currentOrientation;
    public Direction orientationBeforeAudioEvent;
    public Vector2 currentPosition;
    public EnnemyState ennemyState;

    public SpriteRenderer spriteRenderer;
    public Sprite northSprite;
    public Sprite eastSprite;
    public Sprite southSprite;
    public Sprite westSprite;
    private bool move;
    public void OnEnnemySpawn()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void EnnemyPlay()
    {
        //audio event cases
        if(ennemyState == EnnemyState.heard)
        {

        }
        else if(ennemyState == EnnemyState.turningToward)
        {
            //ChangeOrientationTowards();
        }
        else if (ennemyState == EnnemyState.turningBack)
        {
            ChangeOrientationTowards(orientationBeforeAudioEvent);
            ennemyState = EnnemyState.play; //we go back to standard behavior

        }
        //normal behavior
        else if (DoesUnitMove(ennemyType))
        {
            if (!CheckNextBoxContent())
            {
                ChangeOrientationTowards(Uturn(currentOrientation));
            }
            else
            {
                move = true;
            }
        }

        //CheckSight();
        if(move)MoveUnit();
        move = false;


    }

    public bool CheckNextBoxContent()
    {
        Vector2 stepToAdd = new();
        switch (currentOrientation) //on détermine la box a check en fonction de l'orientation actuel de l'unité
        {
            case Direction.Up:
                stepToAdd = new Vector2(0 , 1);
                break;
            case Direction.Left:
                stepToAdd = new Vector2(-1 , 0);
                break;
            case Direction.Down:
                stepToAdd = new Vector2(0 , -1);
                break;
            case Direction.Right:
                stepToAdd = new Vector2(1 , 0);
                break;
        }
        Vector2 boxToCheckCoordinates = currentPosition + stepToAdd;
        if (CouldMoveOnBox(boxToCheckCoordinates)) //on check si l'unité peut aller sur la box
        {
            //l'unité peut bouger sur la box
            return true;
        }
        else
        {
            return false;
            //l'unité rencontre un obstacle OU les limites de la grille et entame un demi-tour
        }

    }

    void ChangeOrientationTowards(Direction direction)
    {
        currentOrientation = direction;
    }

    Direction Uturn(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Direction.Down;
            case Direction.Down:
                return Direction.Up;
            case Direction.Left:
                return Direction.Right;
            default:
                return Direction.Left;
        }
    }
    private bool CouldMoveOnBox(Vector2 boxPosition)
    {
        if (boxPosition.x < 0 || boxPosition.y < 0) return false;
        if (boxPosition.x >= GridManager.instance.gridSize.x) return false;
        if (boxPosition.y >= GridManager.instance.gridSize.y) return false;
        if (GridManager.instance.GetBoxInfo(boxPosition).isObstacle) return false;
        return true;
    }

    bool DoesUnitMove(EnnemyType ennemyType)
    {
        switch (ennemyType)
        {
            case EnnemyType.Cat:
                return true;
            default:
                return false;
        }

    }

    void MoveUnit()
    {
        switch (ennemyType)
        {
            case EnnemyType.Cat:

                break;
            default:
                break;        
        }
    }
}


[Serializable]
public struct EnnemyInGrid
{
    public string name;
    public EnnemyType ennemyType;
    public Vector2 startInGrid;
    public Direction directionStart;

}

public enum EnnemyType
{
    Cat,
    Snake
}

public enum Direction
{
    Up,
    Right,
    Down,
    Left
}

public enum EnnemyState
{
    play, //the unit will do its base behavior
    heard, //the unit detected a sound this turn  
    turningToward,//the unit turn toward the sound
    turningBack,//the unit turn back
}
