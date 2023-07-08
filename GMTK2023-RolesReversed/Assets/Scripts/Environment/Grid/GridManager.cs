using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
    public static GridManager instance;

    // Grid info
    public int gridLevel;
    public Vector2 gridSize;
    public Vector2 gridPlayerStart;
    public List<EnnemyInGrid> allEnnemy = new List<EnnemyInGrid>();
    public List<Box> allBox = new List<Box>();


    #region Starts
    private void Awake() {
        CheckInstance();
    }

    private void Start() {
        gridLevel = 0;
    }

    private void OnEnable() {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDisable() {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }
    #endregion

    
    #region Event
    private void OnGameStateChanged(GameState state) {
        
        if(state == GameState.StartLevel) {
            gridSize = GridParamaters.instance.allGrid[gridLevel].gridSize;
            gridPlayerStart = GridParamaters.instance.allGrid[gridLevel].gridPlayerStart;
            // add all Ennemy //////////////////////////////////////////////////////////////
            allBox = new List<Box>();
            foreach(Box box in GridParamaters.instance.allGrid[gridLevel].allBox) {
                Box newBox = new Box();
                newBox.isObstacle = box.isObstacle;
                newBox.isMakeSound = box.isMakeSound;
                newBox.isHiding = box.isHiding;
                newBox.isCheese = box.isCheese;
                allBox.Add(newBox);
            }
            GridSetUp.instance.SetGrid(gridSize, gridPlayerStart, allBox);
            return;
        }
        if(state == GameState.Win) {
            gridLevel++;
            return;
        }
        
    }   
    #endregion


    #region Grid Manager
    public Box GetBoxInfo(Vector2 boxPosition) {
        foreach(Box box in allBox) {
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
