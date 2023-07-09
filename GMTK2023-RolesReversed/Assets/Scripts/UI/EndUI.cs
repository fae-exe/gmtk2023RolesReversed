using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndUI : MonoBehaviour {

    // Cheese info
    [SerializeField] private GameObject endUIObject;
    [SerializeField] private GameObject loseUIObject;
    [SerializeField] private GameObject winUIObject;
    [SerializeField] private GameObject endAllUIObject;

    #region Starts
    private void OnEnable() {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDisable() {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }
    #endregion


    #region Event
    private void OnGameStateChanged(GameState state) {
         
        if(state == GameState.Lose) {
            loseUIObject.SetActive(true);
            winUIObject.SetActive(false);
            endAllUIObject.SetActive(false);
            return;
        }
        if(state == GameState.Win) {            
            if(GridManager.instance.gridLevel >= GridParamaters.instance.allGrid.Count-1) {
                loseUIObject.SetActive(false);
                winUIObject.SetActive(false);
                endAllUIObject.SetActive(true);
                return;
            } else {
                loseUIObject.SetActive(false);
                winUIObject.SetActive(true);
                endAllUIObject.SetActive(false);
                return;
            }
        }
        if(state == GameState.Finish) {
            loseUIObject.SetActive(false);
            winUIObject.SetActive(false);
            endAllUIObject.SetActive(true);
            return;
        }
        loseUIObject.SetActive(false);
        winUIObject.SetActive(false);
        endAllUIObject.SetActive(false);
    }   
    #endregion

}
