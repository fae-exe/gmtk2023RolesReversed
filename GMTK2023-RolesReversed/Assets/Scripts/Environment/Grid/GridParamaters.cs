using System;
using System.Collections.Generic;
using UnityEngine;

public class GridParamaters : MonoBehaviour {
    public static GridParamaters instance;

    // All Grids info
    public List<GridScriptObj> allGrid = new List<GridScriptObj>();


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
    public string text1 = new string("Stuff to check");
    public bool isObstacle;
    public bool isMakeSound;
    public bool isHiding;
    public bool isCheese;
    public string text2 = new string("No need, it for code");
    public Vector2 positionInGrid;
    public GameObject boxObject;
    public bool isEnnemyObstacle;
    public GameObject obstacleObject;
    public GameObject cheeseObject;
}
