using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnnemyUnitScript : MonoBehaviour
{

    public EnnemyType _ennemyType;
    public EnnemyDataSO ennemyData;
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
    private bool couldMove;

    #region Ennemy Start
    public void OnEnnemySpawn(EnnemyInGrid ennemy) {
        ennemyData = EnnemyManager.instance.ennemyList.ennemyDic[ennemy.ennemyType];

        // Get correct
        _ennemyType = ennemy.ennemyType;
        // Get Correct sprite
        spriteRenderer = GetComponent<SpriteRenderer>();
        northSprite = ennemyData.northSprite;
        eastSprite = ennemyData.eastSprite;
        southSprite = ennemyData.southSprite;
        westSprite = ennemyData.westSprite;
        // Get correct direction
        currentOrientation = ennemy.directionStart;
        ChangeSpriteToDirection(currentOrientation);
        // Get start position
        SetStartPosition(ennemy.startInGrid);

        couldMove = false;
    }

    private void SetStartPosition(Vector2 startPosition) {
        Vector2 step = GridSetUp.instance.gridStep;
        transform.position = new Vector3(startPosition.x * step.x, startPosition.y * step.y, 0);
        currentPosition = startPosition;
        GridManager.instance.GetBoxInfo(startPosition).isEnnemy = true;
        GridManager.instance.GetBoxInfo(startPosition).ennemyObject = this.gameObject;
    }
    #endregion


    #region Ennemy Play
    public void EnnemyPlay() {
        // IsHeardSomething();
        IsEnnemyMove();
        IsPlayerInView();

        if(!couldMove) return;

        MoveUnit();
        couldMove = false;
    }
    #endregion


    #region Move
    private void IsEnnemyMove() {
        if(!DoesUnitMove(_ennemyType)) return;

        if(CouldGoOnNextBox()) {
            couldMove = true;
        } else {
            couldMove = false;
            ChangeOrientationTowards(Uturn(currentOrientation));
        }
    }

    private bool DoesUnitMove(EnnemyType ennemyType) {
        if(ennemyType == EnnemyType.Cat) return true;
        return false;
    }

    private bool CouldGoOnNextBox(float range = 1) {
        Vector2 boxToGo = currentPosition + GetVectorDirection(currentOrientation) * range;
        return(CouldMoveOnBox(boxToGo));
        // could go on new box or make turn in opposite direction
    }

    private bool CouldMoveOnBox(Vector2 boxPosition) {
        if (boxPosition.x < 0 || boxPosition.y < 0) return false;
        if (boxPosition.x >= GridManager.instance.gridSize.x) return false;
        if (boxPosition.y >= GridManager.instance.gridSize.y) return false;
        if (GridManager.instance.GetBoxInfo(boxPosition).isObstacle) return false;
        if (GridManager.instance.GetBoxInfo(boxPosition).isCheese) return false;
        return true;
    }
    
    private void MoveUnit(float range = 1) {
        Vector2 step = GridSetUp.instance.gridStep;
        Vector2 boxDirection = GetVectorDirection(currentOrientation);
        Vector3 newMove = new Vector3(boxDirection.x * step.x * range, boxDirection.y * step.y * range, 0);
        transform.position += newMove;
        GridManager.instance.GetBoxInfo(currentPosition).isEnnemy = false;
        GridManager.instance.GetBoxInfo(currentPosition).ennemyObject = null;
        currentPosition += GetVectorDirection(currentOrientation) * range;
        GridManager.instance.GetBoxInfo(currentPosition).isEnnemy = true;
        GridManager.instance.GetBoxInfo(currentPosition).ennemyObject = this.gameObject;
    }
    #endregion


    #region Sight
    void IsPlayerInView() {
        Vector2 currentBoxSeen = currentPosition;
        // Loop on ennemy front box until end or obstacle
        for(int i = 0; i < GetMaxBoxSeen(); i++) {
            Vector2 boxToSee = currentBoxSeen + GetVectorDirection(currentOrientation);
            if(!CouldSeeOnBox(boxToSee)) return;

            MapTile seenBox = GridManager.instance.GetTileScript(boxToSee);
            seenBox.GetSeenTrigger().PlayFeedbacks();

            if(GridManager.instance.GetBoxInfo(boxToSee).isPlayer) {
                Debug.Log("Player seen !");
                EnnemyManager.instance.PlayerSeenByEnnemy(this.gameObject);
                return;
            } else {
                currentBoxSeen = boxToSee;
            }            
        }       
    }

    private int GetMaxBoxSeen() {
        int maxBoxSeen = 0;
        if(currentOrientation == Direction.Left || currentOrientation == Direction.Right) {
            maxBoxSeen = (int)GridManager.instance.gridSize.x;
        } else {
            maxBoxSeen = (int)GridManager.instance.gridSize.y;
        }
        return maxBoxSeen;
    }

    private bool CouldSeeOnBox(Vector2 boxPosition) {
        if (boxPosition.x < 0 || boxPosition.y < 0) return false;
        if (boxPosition.x >= GridManager.instance.gridSize.x) return false;
        if (boxPosition.y >= GridManager.instance.gridSize.y) return false;
        if (GridManager.instance.GetBoxInfo(boxPosition).isEnnemy) return false;
        if (GridManager.instance.GetBoxInfo(boxPosition).isObstacle) return false;
        if (GridManager.instance.GetBoxInfo(boxPosition).isCheese) return false;
        return true;
    }
    #endregion


    #region Direction
    private void ChangeSpriteToDirection(Direction orientation) {
        switch (orientation) {
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

    private void ChangeOrientationTowards(Direction direction) {
        currentOrientation = direction;
        ChangeSpriteToDirection(direction);
        //Rajouter feel la
    }

    private Direction Uturn(Direction direction) {
        switch (direction) {
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

    private Vector2 GetVectorDirection(Direction orientation) {
        Vector2 newOrientation = Vector2.zero;
        switch (orientation) {
            case Direction.Up:
                newOrientation = new Vector2(0, 1);
                break;
            case Direction.Left:
                newOrientation = new Vector2(-1, 0);
                break;
            case Direction.Down:
                newOrientation = new Vector2(0, -1);
                break;
            case Direction.Right:
                newOrientation = new Vector2(1, 0);
                break;
        }
        return newOrientation;
    }
    #endregion


    #region Noise
    private void IsHeardSomething() {
        //audio event cases
        if (ennemyState == EnnemyState.heard)
        {
            orientationBeforeAudioEvent = currentOrientation;
        }
        else if (ennemyState == EnnemyState.turningToward)
        {
            ChangeOrientationTowards(DirectionOfNoise(positionOfAudioEvent));
        }
        else if (ennemyState == EnnemyState.turningBack)
        {
            ChangeOrientationTowards(orientationBeforeAudioEvent);
            ennemyState = EnnemyState.play; //we go back to standard behavior

        }
    }

    private Direction DirectionOfNoise(Vector2 positionOfAudioEvent) {
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
    #endregion
    
}

[Serializable]
public struct EnnemyInGrid {
    public string name;
    public EnnemyType ennemyType;
    public Vector2 startInGrid;
    public Direction directionStart;

}

public enum EnnemyType {
    Cat,
    Snake
}

public enum Direction {
    Up,
    Right,
    Down,
    Left
}

public enum EnnemyState {
    play, //the unit will do its base behavior
    heard, //the unit detected a sound this turn  
    turningToward,//the unit turn toward the sound
    turningBack,//the unit turn back
}
