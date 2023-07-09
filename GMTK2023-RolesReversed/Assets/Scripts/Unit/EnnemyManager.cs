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
    public bool isLose;

    public float ennemyStartTime;
    public float ennemyEndTime;
    public float killTime;

    // Event
    public static event Action<GameObject> OnPlayerSeen;


    #region Starts
    private void Awake() {
        CheckInstance();
    }

    private void OnEnable() {
        GameManager.OnGameStateChanged += OnGameStateChanged;
        GridManager.OnGridSetUp += OnGridSetUp;
        PlayerManager.OnSmashingEnnemy += OnSmashingEnnemy;
    }

    private void OnDisable() {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
        GridManager.OnGridSetUp -= OnGridSetUp;
        PlayerManager.OnSmashingEnnemy -= OnSmashingEnnemy;
    }
    #endregion


    #region Event
    private void OnGameStateChanged(GameState state) {

        if(state == GameState.StartLevel) {
            isLose = false;
            return;
        }        
        if(state == GameState.EnnemyTurn) {
            StartCoroutine(PlayEnnemyTurn());
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

    private void OnSmashingEnnemy(GameObject ennemyObject) {
        StartCoroutine(KillDaEnnemy(ennemyObject));
    }

    private IEnumerator KillDaEnnemy(GameObject ennemyObject) {
        currentEnnemies.Remove(ennemyObject);
        // Destroy(ennemyObject);
        yield return new WaitForSecondsRealtime(killTime);
        GameManager.instance.UpdateGameState(GameState.EnnemyTurn);
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

    private IEnumerator PlayEnnemyTurn() {

        yield return new WaitForSecondsRealtime(ennemyStartTime);
        foreach (GameObject ennemy in currentEnnemies)
        {
            EnnemyUnitScript ennemyUnitScript = ennemy.GetComponent<EnnemyUnitScript>();
            ennemyUnitScript.EnnemyPlay();
        }
        if(!isLose) {   
            // Next turn
            yield return new WaitForSecondsRealtime(ennemyEndTime);
            GameManager.instance.UpdateGameState(GameState.PlayerTurn);
        }

    }

    public void PlayerSeenByEnnemy(GameObject ennemyObject) {
        isLose = true;
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
