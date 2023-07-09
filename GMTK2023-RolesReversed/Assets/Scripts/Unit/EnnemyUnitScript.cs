using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnnemyUnitScript : MonoBehaviour
{

    public EnnemyType _ennemyType;

    public Direction currentOrientation;
    public Direction orientationBeforeAudioEvent;
    public Vector2 currentPosition;
    public Vector2 positionOfAudioEvent;
    public EnnemyState ennemyState;

    public SpriteRenderer spriteRenderer;
    public Sprite northSprite;
    public Sprite eastSprite;
    public Sprite southSprite;
    public Sprite westSprite;
    private bool move;
    public void OnEnnemySpawn(EnnemyInGrid ennemy)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentOrientation = ennemy.directionStart;
        currentPosition = ennemy.startInGrid;
        ChangeSpriteTo(currentOrientation);

    }

    public void OnNoiseHeard()
    {

    }
    //public void EnnemyPlay()
    //{
    //    //audio event cases
    //    if(ennemyState == EnnemyState.heard)
    //    {
    //        orientationBeforeAudioEvent=currentOrientation;
    //    }
    //    else if(ennemyState == EnnemyState.turningToward)
    //    {
    //        ChangeOrientationTowards(DirectionOfNoise(positionOfAudioEvent));
    //    }
    //    else if (ennemyState == EnnemyState.turningBack)
    //    {
    //        ChangeOrientationTowards(orientationBeforeAudioEvent);
    //        ennemyState = EnnemyState.play; //we go back to standard behavior

    //    }


    //    //normal behavior
    //    else if (DoesUnitMove(_ennemyType))
    //    {
    //        if (!CheckNextBoxContent())
    //        {
    //            ChangeOrientationTowards(Uturn(currentOrientation));
    //        }
    //        else
    //        {
    //            move = true;
    //        }
    //    }

    //    CheckSight(currentOrientation);
        
    //    if(move)MoveUnit(currentOrientation);
    //    move = false;


    //}

    public bool CheckNextBoxContent(float range = 1)
    {
        Vector2 stepToAdd = new();
        switch (currentOrientation) //on d�termine la box a check en fonction de l'orientation actuel de l'unit�
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
        Vector2 boxToCheckCoordinates = currentPosition + stepToAdd * range;
        if (CouldMoveOnBox(boxToCheckCoordinates)) //on check si l'unit� peut aller sur la box
        {
            //l'unit� peut bouger sur la box
            return true;
        }
        else
        {
            return false;
            //l'unit� rencontre un obstacle OU les limites de la grille et entame un demi-tour
        }

    }

    Direction DirectionOfNoise(Vector2 positionOfAudioEvent)
    {
        Direction direction;
        if(Mathf.Abs(positionOfAudioEvent.x) < Mathf.Abs(positionOfAudioEvent.y)) //x est plus proche
        {
            direction = Mathf.Sign(positionOfAudioEvent.x) == 1 ? Direction.Right : Direction.Left; 
        }
        else if(Mathf.Abs(positionOfAudioEvent.x) > Mathf.Abs(positionOfAudioEvent.y)) //y est plus proche
        {
            direction = Mathf.Sign(positionOfAudioEvent.y) == 1 ? Direction.Up : Direction.Down;
        }
        else //diagonal
        {
            float xSign = Mathf.Sign(positionOfAudioEvent.x);
            float ySign = Mathf.Sign(positionOfAudioEvent.y);
            float diffSign = ySign * xSign;

            if(xSign == 1)
            {
                direction = diffSign == 1 ? Direction.Up: Direction.Right;
            }
            else
            {
                direction = diffSign == 1 ? Direction.Down : Direction.Left;
            }
        }
        return direction;
    }

    void ChangeOrientationTowards(Direction direction)
    {
        currentOrientation = direction;
        ChangeSpriteTo(direction);
        //Rajouter feel la
    }

    void CheckSight(Direction direction)
    {
        bool keepChecking = true;
        float i = 1;
        Vector2 stepToAdd = new();
        switch (direction) //on d�termine la box a check en fonction de l'orientation actuel de l'unit�
        {
            case Direction.Up:
                stepToAdd = new Vector2(0, 1);
                break;
            case Direction.Left:
                stepToAdd = new Vector2(-1, 0);
                break;
            case Direction.Down:
                stepToAdd = new Vector2(0, -1);
                break;
            case Direction.Right:
                stepToAdd = new Vector2(1, 0);
                break;
        }

        List<MapTile> tilesChecked = new();
        while (keepChecking)
        {

            Vector2 boxPosition = currentPosition + stepToAdd * i;
            MapTile tileToCheck = GridManager.instance.GetTileScript(boxPosition);
            if (!CheckNextBoxContent(i))
            {
                keepChecking = false;
            }
            else
            {
                tilesChecked.Add(tileToCheck);
            }

            if (GridManager.instance.GetBoxInfo(boxPosition).isPlayer)
            {
                GameManager.instance.UpdateGameState(GameState.Lose);
            }
        }
       
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

    void ChangeSpriteTo(Direction orientation)
    {
        switch (orientation)
        {
            case Direction.Up:
                spriteRenderer.sprite = northSprite;
                break;
            case Direction.Right:
                spriteRenderer.sprite = eastSprite;
                break;
            case Direction.Down:
                spriteRenderer.sprite = southSprite;
                break;
            case Direction.Left:
                spriteRenderer.sprite = westSprite;
                break;
        }
    }
    private bool CouldMoveOnBox(Vector2 boxPosition)
    {
        if (boxPosition.x < 0 || boxPosition.y < 0) return false;
        if (boxPosition.x >= GridManager.instance.gridSize.x) return false;
        if (boxPosition.y >= GridManager.instance.gridSize.y) return false;
        if (GridManager.instance.GetBoxInfo(boxPosition).isObstacle) return false;
        // Check if isEnnemy ????
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

    void MoveUnit(Direction orientation, float range = 1)
    {
        Vector2 stepToAdd = new(); 

        switch (orientation) //on d�termine la box a check en fonction de l'orientation actuel de l'unit�
        {
            case Direction.Up:
                stepToAdd = new Vector2(0, 1);
                break;
            case Direction.Left:
                stepToAdd = new Vector2(-1, 0);
                break;
            case Direction.Down:
                stepToAdd = new Vector2(0, -1);
                break;
            case Direction.Right:
                stepToAdd = new Vector2(1, 0);
                break;
        }

        stepToAdd *= GridSetUp.instance.gridStep ;

        switch (_ennemyType)
        {
            case EnnemyType.Cat:
                Vector3 newMove = new Vector3(stepToAdd.x * range, stepToAdd.y * range, 0);
                this.transform.position += newMove;
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
