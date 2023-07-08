using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackUI : MonoBehaviour {
    public static AttackUI instance;

    // Player info
    public PlayerInfo playerInfo;

    // Attack info
    [SerializeField] private GameObject attackUIObject;
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private string isAttackText;
    [SerializeField] private string isNonAttackText;


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
            attackUIObject.SetActive(true);
            RefreshAttackUI();
            return;
        }
        if(state == GameState.EnnemyTurn) {
            attackUIObject.SetActive(true);
            RefreshAttackUI();
            return;
        }

        attackUIObject.SetActive(false);
    }   
    #endregion
    

    #region Attack UI
    public void RefreshAttackUI() {
        if(playerInfo.isAttacking) {
            attackText.text = isAttackText;
        } else {
            attackText.text = isNonAttackText;
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
