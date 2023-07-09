using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyManager : MonoBehaviour {
    public static EnnemyManager instance;

    public List<GameObject> currentEnnemies;
    
    [SerializeField] private List<EnnemyInGrid> allEnnemyInfo = new List<EnnemyInGrid>();
    [SerializeField] private GameObject ennemyContainer;
    [SerializeField] private GameObject ennemyPrefab;
    public EnnemyListSO ennemyList;

    // Event
    public static event Action<GameObject> OnPlayerSeen;


    #region Starts
    private void Awake() {
        CheckInstance();
    }

    private void OnEnable() {
        GameManager.OnGameStateChanged += OnGameStateChanged;
        GridManager.OnGridSetUp += OnGridSetUp;
    }

    private void OnDisable() {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
        GridManager.OnGridSetUp -= OnGridSetUp;
    }
    #endregion


    #region Event
    private void OnGameStateChanged(GameState state) {
        
        if(state == GameState.EnnemyTurn) {
            StartCoroutine(PlayEnnemyTurn(EnnemyParameters.instance.ennemyTurnTime));
            return;
        }
    }   

    private void OnGridSetUp(List<EnnemyInGrid> allEnnemy) {
        // Reset enemmies
        if(ennemyContainer != null) Destroy(ennemyContainer);
        ennemyContainer = new GameObject("EnnemyContainer");
        ennemyContainer.transform.parent = this.transform;

        allEnnemyInfo = allEnnemy;
        SpawnEnnemies();
    }   
    #endregion


    #region Ennemy Manager
    private void SpawnEnnemies() {
        currentEnnemies = new List<GameObject>();

        foreach(EnnemyInGrid ennemyInfo in allEnnemyInfo) {
            GameObject newEnnemy = Instantiate(ennemyPrefab, transform.position, Quaternion.identity, ennemyContainer.transform);
            newEnnemy.GetComponent<EnnemyUnitScript>().OnEnnemySpawn(ennemyInfo);
            
            currentEnnemies.Add(newEnnemy);
        }        
    }



    private IEnumerator PlayEnnemyTurn(float timeToWait) {

        yield return new WaitForSecondsRealtime(timeToWait);
        foreach (GameObject ennemy in currentEnnemies)
        {
            EnnemyUnitScript ennemyUnitScript = ennemy.GetComponent<EnnemyUnitScript>();
            //ennemyUnitScript.EnnemyPlay();
        }
        // Next turn
        GameManager.instance.UpdateGameState(GameState.PlayerTurn);

    }

    public void PlayerSeenByEnnemy(GameObject ennemyObject) {
        OnPlayerSeen?.Invoke(ennemyObject);
        GameManager.instance.UpdateGameState(GameState.Lose);
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

    private void PlaceEnnemy(string ennemyID, int cellID)
    {

    }
    #endregion

}
