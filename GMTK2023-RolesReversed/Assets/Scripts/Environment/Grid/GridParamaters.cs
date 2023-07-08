using System;
using System.Collections.Generic;
using UnityEngine;

public class GridParamaters : MonoBehaviour {
    public static GridParamaters instance;


    // All Grids info
    public Vector2 grid01Size;
    public Vector2 grid01Start;
    public List<Box> grid01 = new List<Box>();


    #region Starts
    private void Awake() {
        CheckInstance();
    }
    #endregion


    #region General Functions
    private void CheckInstance() {
        if(instance != null && instance != this) { 
            Destroy(this.gameObject); 
        } else { 
            instance = this; 
        } 
    }
    #endregion

}

[Serializable]
public class Box {
    public string name;
    public Vector2 positionInGrid;
    public GameObject boxObject;
    public bool isObstacle;
    public bool isEnnemyObstacle;
    public GameObject obstacleObject;
    public bool isCheese;
    public GameObject cheeseObject;
}
