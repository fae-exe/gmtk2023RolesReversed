using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager instance;

    // Player info
    [SerializeField] private Transform player;
    [SerializeField] private PlayerInfo playerInfo;

    // Ennemy
    public static event Action<GameObject> OnSmashingEnnemy;


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
            CheeseUI.instance.SetCheeseUI(playerInfo.maxCheeseLevel);
            return;
        }
        
    }   
    #endregion


    #region Player Manager
    public void SetPlayerPosition(Vector2 startPosition) {
        Vector2 step = GridSetUp.instance.gridStep;
        player.transform.position = new Vector3(startPosition.x * step.x, startPosition.y * step.y, 0);;
        playerInfo.playerPositionInGrid = startPosition; 
    }

    public void IsOnCheese(Box box) {
        if(!box.isCheese) return;

        playerInfo.cheeseLevel++;
        CheeseUI.instance.SetCheeseUILevel(playerInfo.maxCheeseLevel, playerInfo.cheeseLevel);
        IsOnFury();

        box.isCheese = false;
        Destroy(box.cheeseObject);
    }

    public void IsSmashingEnnemy(Box box) {
        if(!box.isEnnemy || !playerInfo.isAttacking) return;

        playerInfo.cheeseLevel--;
        CheeseUI.instance.SetCheeseUILevel(playerInfo.maxCheeseLevel, playerInfo.cheeseLevel);
        IsOnFury();

        OnSmashingEnnemy?.Invoke(box.ennemyObject);
    }

    public void IsOnFury() {
        playerInfo.isAttacking = playerInfo.cheeseLevel > 0 ? true : false;
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
