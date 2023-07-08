using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public GameState previousState;
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;
    public GameState startState;


    #region Start
    private void Awake() {
        CheckInstance();
    }

    private void Start() {
        UpdateGameState(startState);
    }
    #endregion


    #region Game State
    public void UpdateGameState(GameState newState) {
        previousState = state;
        state = newState;

        switch(newState) {
            case GameState.HomeScreen:
                break;
            case GameState.StartLevel:
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.EnnemyTurn:
                break;
            case GameState.Lose:
                break;
            case GameState.Win:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        Debug.Log(state);
        OnGameStateChanged?.Invoke(newState);
    }

    public void StartToPlay() {
        UpdateGameState(GameState.PlayerTurn);
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

public enum GameState {
    HomeScreen,
    StartLevel,
    PlayerTurn,
    EnnemyTurn,
    Lose,
    Win
}
