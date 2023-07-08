using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnnemyUnitScript : MonoBehaviour
{
    public Sprite upSprite;
    public UnitOrientation currentOrientation;
    public Vector2 currentPosition;

    public virtual void EnnemyPlay()
    {
        //l'unité fait quelque chose à son tour (e.g. rien : serpent, e.g. bouger ou faire demi tour)
    }

    public bool CheckNextBoxContent()
    {
        Vector2 stepToAdd = new();
        switch (currentOrientation) //on détermine la box a check en fonction de l'orientation actuel de l'unité
        {
            case UnitOrientation.north:
                stepToAdd = new Vector2(0,1);
                break;
            case UnitOrientation.west:
                stepToAdd = new Vector2(-1, 0);
                break;
            case UnitOrientation.south:
                stepToAdd = new Vector2(0, -1);
                break;
            case UnitOrientation.east:
                stepToAdd = new Vector2(1, 0);
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

    private bool CouldMoveOnBox(Vector2 boxPosition)
    {
        if (boxPosition.x < 0 || boxPosition.y < 0) return false;
        if (boxPosition.x >= GridManager.instance.gridSize.x) return false;
        if (boxPosition.y >= GridManager.instance.gridSize.y) return false;
        if (GridManager.instance.GetBoxInfo(boxPosition).isObstacle) return false;
        return true;
    }

}


public enum UnitOrientation
{
    north,
    east,
    south,
    west
}
