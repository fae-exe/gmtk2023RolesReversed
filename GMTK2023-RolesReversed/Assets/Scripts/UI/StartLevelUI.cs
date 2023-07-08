using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartLevelUI : MonoBehaviour {
    public static StartLevelUI instance;
    
    // Cheese info
    [SerializeField] private GameObject startLevelUI;


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

        if(state == GameState.StartLevel) {
            startLevelUI.SetActive(true);
            return;
        }

        startLevelUI.SetActive(false);
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
