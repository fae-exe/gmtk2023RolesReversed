using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnUI : MonoBehaviour {
    public static TurnUI instance;

    // Cheese info
    [SerializeField] private GameObject turnUI;
    [SerializeField] private TMP_Text turnText;
    [SerializeField] private string playerTurnText;
    [SerializeField] private string ennemyTurnText;


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

        if(state == GameState.PlayerTurn) {
            turnUI.SetActive(true);
            turnText.text = playerTurnText;
            return;
        }
        if(state == GameState.EnnemyTurn) {
            turnUI.SetActive(true);
            turnText.text = ennemyTurnText;
            return;
        }

        turnUI.SetActive(false);
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
