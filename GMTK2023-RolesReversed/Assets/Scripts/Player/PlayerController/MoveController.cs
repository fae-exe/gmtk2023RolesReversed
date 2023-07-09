using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour {
    PlayerInputs playerInputs;

    // Player info
    [SerializeField] private Transform player;
    [SerializeField] private PlayerInfo playerInfo;

    // Event
    public static event Action<Direction> OnPlayerDirection;
    public static event Action OnPlayerMove;
    public static event Action OnPlayerBlocked;

    
    #region Starts
    private void Awake() {
        GetPlayerActionMaps();
    }

    private void GetPlayerActionMaps() {
        playerInputs = new PlayerInputs();
        playerInputs.Move.Move.started += Move;
    }
    
    private void OnEnable() {
        playerInputs.Move.Enable();
    }

    private void OnDisable() {
        playerInputs.Move.Disable();
    }
    #endregion


    #region Move
    private void Move(InputAction.CallbackContext context) {
        if(!CouldUpdate()) return;
        
        Vector2 boxDirection = GetDirection(playerInputs.Move.Move.ReadValue<Vector2>());
        Vector2 boxToGo = playerInfo.playerPositionInGrid + boxDirection;

        if(CouldMoveOnBox(boxToGo)) {
            Vector2 step = GridSetUp.instance.gridStep;
            Vector3 newMove = new Vector3(boxDirection.x * step.x, boxDirection.y * step.y, 0);
            OnPlayerDirection?.Invoke(GetEnumDirection(boxDirection));
            player.transform.position += newMove;
            playerInfo.playerPositionInGrid = boxToGo; 
            OnPlayerMove?.Invoke();
            // Check player new states (on cheese, attack)
            PlayerManager.instance.IsOnCheese(GridManager.instance.GetBoxInfo(boxToGo));
            PlayerManager.instance.IsSmashingEnnemy(GridManager.instance.GetBoxInfo(boxToGo));
            // Next turn
            GameManager.instance.UpdateGameState(GameState.EnnemyTurn);
        } else {
            OnPlayerDirection?.Invoke(GetEnumDirection(boxDirection));
            OnPlayerBlocked?.Invoke();
            Debug.Log("Can't go on this box !");
        }
        
    }

    private bool CouldUpdate() {
        if(GameManager.instance.state == GameState.PlayerTurn) return true;
        return false;
    }

    private Vector2 GetDirection(Vector2 playerInput) {
        // Get x movement
        float x = 0;
        if(playerInput.x > 0.1f) {
            x = 1;  
        } else if(playerInput.x < -0.1f) {
            x = -1;
        }
        // Get y movement
        float y = 0;
        if(playerInput.y > 0.1f) {
            y = 1;  
        } else if(playerInput.y < -0.1f) {
            y = -1;
        }  
        // return Vector
        return new Vector2(x, y);
    }

    private Direction GetEnumDirection(Vector2 vectorDirection) {
        if(vectorDirection.x == 0 && vectorDirection.y == 1) return Direction.Up;
        if(vectorDirection.x == 1 && vectorDirection.y == 0) return Direction.Right;
        if(vectorDirection.x == 0 && vectorDirection.y == -1) return Direction.Down;
        if(vectorDirection.x == -1 && vectorDirection.y == 0) return Direction.Left;
        
        Debug.Log(vectorDirection + " is not good, so Direction.Right by default");
        return Direction.Right;
    }

    private bool CouldMoveOnBox(Vector2 boxPosition) {
        if(boxPosition.x < 0 || boxPosition.y < 0) return false;
        if(boxPosition.x >= GridManager.instance.gridSize.x) return false;
        if(boxPosition.y >= GridManager.instance.gridSize.y) return false;
        if(GridManager.instance.GetBoxInfo(boxPosition).isObstacle) return false;
        if(GridManager.instance.GetBoxInfo(boxPosition).isEnnemy && !playerInfo.isOnFury) return false;
        return true;
    }
    #endregion

}
