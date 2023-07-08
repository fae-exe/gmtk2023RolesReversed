using UnityEngine;
using UnityEngine.InputSystem;

public class ActionController : MonoBehaviour {
    PlayerInputs playerInputs;

    // Player info
    [SerializeField] private PlayerInfo playerInfo;


    #region Starts
    private void Awake() {
        GetPlayerActionMaps();
    }

    private void GetPlayerActionMaps() {
        playerInputs = new PlayerInputs();
        // playerInputs.Action.Attack.started += TriggerAttack;
        playerInputs.Action.SkipTurn.started += SkipTurn;
        playerInputs.Action.RestartLevel.started += RestartLevel;
    }
    
    private void OnEnable() {
        playerInputs.Action.Enable();
    }

    private void OnDisable() {
        playerInputs.Action.Disable();
    }
    #endregion


    #region Attack
    // private void TriggerAttack(InputAction.CallbackContext context) {
    //     if(!CouldUpdate()) return;

    //     if(playerInfo.isAttacking) {
    //         playerInfo.isAttacking = false;
    //         AttackUI.instance.RefreshAttackUI();
    //     } else if(CouldAttack()){
    //         playerInfo.isAttacking = true;
    //         AttackUI.instance.RefreshAttackUI();
    //     }        
    // }

    // private bool CouldAttack() {
    //     if(playerInfo.cheeseLevel > 0) return true;
    //     return false;
    // }
    #endregion


    #region Level and Turn
    private void SkipTurn(InputAction.CallbackContext context) {   
        if(!CouldUpdate()) return;
        
        GameManager.instance.UpdateGameState(GameState.EnnemyTurn);
    }

    private void RestartLevel(InputAction.CallbackContext context) {           
        GameManager.instance.UpdateGameState(GameState.StartLevel);
    }

    private bool CouldUpdate() {
        if(GameManager.instance.state == GameState.PlayerTurn) return true;
        return false;
    }
    #endregion

}
