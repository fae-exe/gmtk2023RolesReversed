using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager instance;

    // Player info
    [SerializeField] private Transform player;
    [SerializeField] private PlayerInfo playerInfo;

    // Event
    public static event Action OnGetDaCheese;
    public static event Action<bool> OnFuryChange;
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
            playerInfo.cheeseLevel = 0;
            playerInfo.isSmashing = false;
            OnGetDaCheese?.Invoke();
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
        OnGetDaCheese?.Invoke();
        IsOnFury();

        // MAYBE TO SWITCH PLACE TO ADD AN ANIM OF CHEESE DESTROY ??
        box.isCheese = false;
        AudioManager.instance.PlaySound("CheeseEat", 0.01f);
        
        Destroy(box.cheeseObject);
    }

    public void IsSmashingEnnemy(Box box) {
        if(box.isEnnemy && playerInfo.isOnFury) {
            playerInfo.isSmashing = true;
            playerInfo.cheeseLevel--;
            IsOnFury();

            OnSmashingEnnemy?.Invoke(box.ennemyObject);
            box.isEnnemy = false;
            box.ennemyObject = null;
        } else {
            GameManager.instance.UpdateGameState(GameState.EnnemyTurn);
        }
    }

    public void IsOnFury() {
        if(playerInfo.isOnFury) {
            // Is already on fury and check if lose all cheese
            if(playerInfo.cheeseLevel == 0) {
                OnFuryChange?.Invoke(false);
                playerInfo.isOnFury = false;
            } 
        } else {
            // Is not already on fury and check if add at least one cheese
            if(playerInfo.cheeseLevel > 0) {
                OnFuryChange?.Invoke(true);
                playerInfo.isOnFury = true;
            } 
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
