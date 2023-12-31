using System;
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

    // Event
    public static event Action<List<EnnemyInGrid>> OnGridSetUp;


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
            SpawnEnnemies();
            return;
        }
        if(state == GameState.Win) {
            if(gridLevel >= GridParamaters.instance.allGrid.Count-1) {
                GameManager.instance.UpdateGameState(GameState.Finish);
                return;
            }
        }
        if(state == GameState.NextLevel) {
            gridLevel++;
            GameManager.instance.UpdateGameState(GameState.StartLevel);
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

    public MapTile GetTileScript(Vector2 boxPosition)
    {
        foreach (Box box in allBox)
        {
            if (boxPosition == box.positionInGrid) return box.boxObject.GetComponent<MapTile>();
        }
        return null;
    }
    #endregion


    #region Ennemy
    private void SpawnEnnemies() {
        allEnnemy = new List<EnnemyInGrid>();
        foreach(EnnemyInGrid ennemy in GridParamaters.instance.allGrid[gridLevel].allEnnemy) {
            EnnemyInGrid newEnnemy = new EnnemyInGrid();
            newEnnemy.name = ennemy.name;
            newEnnemy.ennemyType = ennemy.ennemyType;
            newEnnemy.startInGrid = ennemy.startInGrid;
            newEnnemy.directionStart = ennemy.directionStart;
            allEnnemy.Add(newEnnemy);
        }
        OnGridSetUp?.Invoke(allEnnemy);
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
