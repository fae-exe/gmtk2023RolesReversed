using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
    public static GridManager instance;

    // Grid info
    public Vector2 gridSize;
    public Vector2 gridStart;
    public List<Box> gridInfo = new List<Box>();


    #region Starts
    private void Awake() {
        CheckInstance();
    }

    private void Start() {
        // Set Grid 01
        gridSize = GridParamaters.instance.grid01Size;
        gridInfo = GridParamaters.instance.grid01;
        gridStart = GridParamaters.instance.grid01Start;
        GridSetUp.instance.SetGrid(gridInfo, gridSize, gridStart);
    }
    #endregion


    #region Grid Manager
    public Box GetBoxInfo(Vector2 boxPosition) {
        foreach(Box box in gridInfo) {
            if(boxPosition == box.positionInGrid) return box;
        }
        return null;
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
