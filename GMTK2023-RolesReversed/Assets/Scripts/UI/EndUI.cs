using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndUI : MonoBehaviour {

    // Cheese info
    [SerializeField] private GameObject endUIObject;
    [SerializeField] private GameObject loseUIObject;
    [SerializeField] private GameObject winUIObject;


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
            return;
        }
        if(state == GameState.Win) {
            loseUIObject.SetActive(false);
            winUIObject.SetActive(true);
            return;
        }
        loseUIObject.SetActive(false);
        winUIObject.SetActive(false);
    }   
    #endregion

}
