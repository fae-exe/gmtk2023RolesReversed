using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyManager : MonoBehaviour {
    public static EnnemyManager instance;


    #region Starts
    private void Awake() {
        CheckInstance();
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
        
        if(state == GameState.EnnemyTurn) {
            StartCoroutine(PlayEnnemyTurn(EnnemyParameters.instance.ennemyTurnTime));
            return;
        }
    }   
    #endregion


    #region Ennemy Manager
    private IEnumerator PlayEnnemyTurn(float timeToWait) {

        yield return new WaitForSecondsRealtime(timeToWait);
        // Next turn
        // TEST IF Lose ///////////////////////////////////////////////////////
        GameManager.instance.UpdateGameState(GameState.PlayerTurn);

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
