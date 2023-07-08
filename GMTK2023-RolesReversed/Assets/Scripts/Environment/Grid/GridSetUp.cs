using System;
using System.Collections.Generic;
using UnityEngine;

public class GridSetUp : MonoBehaviour {
    public static GridSetUp instance;

    // Grid info
    [SerializeField] private Transform gridContainer;
    [SerializeField] private List<GameObject> boxPrefab = new List<GameObject>();
    [SerializeField] private List<GameObject> obstaclePrefab = new List<GameObject>();
    [SerializeField] private List<GameObject> cheesePrefab = new List<GameObject>();
    public Vector2 gridStep;


    #region Starts
    private void Awake() {
        CheckInstance();
    }
    #endregion


    #region Grid SetUp
    public void SetGrid(List<Box> gridInfo, Vector2 gridSize, Vector2 gridStart) {
        int i = 0;
        for(int y = 0; y < gridSize.y; y++) {
            for(int x = 0; x < gridSize.x; x++) {
                Vector2 boxPosition = new Vector2(x * gridStep.x, y * gridStep.y);
                // Change prefab selection to random or predefine
                GameObject boxObject = Instantiate(boxPrefab[0], boxPosition, Quaternion.identity, gridContainer);
                gridInfo[i].boxObject = boxObject;
                gridInfo[i].positionInGrid = new Vector2(x, y);
                IsObstacleOnBox(gridInfo[i]);
                IsCheeseOnBox(gridInfo[i]);
                i++;
            }
        }
        PlayerManager.instance.SetPlayerPosition(gridStart);
    }

    private void IsObstacleOnBox(Box box) {
        // Change prefab selection to random or predefine
        if(box.isObstacle) {
            GameObject obstacleObject = Instantiate(obstaclePrefab[0], box.boxObject.transform.position, Quaternion.identity, box.boxObject.transform);
            box.obstacleObject = obstacleObject;
            return;
        }
    }

    private void IsCheeseOnBox(Box box) {
        // Change prefab selection to random or predefine
        if(box.isCheese) {
            GameObject cheeseObject = Instantiate(cheesePrefab[0], box.boxObject.transform.position, Quaternion.identity, box.boxObject.transform);
            box.cheeseObject = cheeseObject;
            return;
        }
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
