using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CheeseUI : MonoBehaviour {
    public static CheeseUI instance;

    // Player info
    public PlayerInfo playerInfo;

    // Cheese info
    [SerializeField] private GameObject cheeseUIObject;
    [SerializeField] private CheeseLevel cheeseLevelPrefab;

    [SerializeField] private AnimationQueue _cheeseUIAnimationQueue;

    private List<CheeseLevel> _cheeseLevelList = new List<CheeseLevel>();

    [SerializeField] private Vector2 step;
    [SerializeField] private Vector2 start;
    [SerializeField] private Color colorEmpty;
    [SerializeField] private Color colorFull;


    #region Starts
    private void Awake() {
        CheckInstance();
    }

    private void Start() {
        SetCheeseUI();
    }

    private void OnEnable() {
        GameManager.OnGameStateChanged += OnGameStateChanged;
        PlayerManager.OnGetDaCheese += OnGetDaCheese;
        PlayerManager.OnSmashingEnnemy += OnSmashingEnnemy;
    }

    private void OnDisable() {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
        PlayerManager.OnGetDaCheese -= OnGetDaCheese;
        PlayerManager.OnSmashingEnnemy -= OnSmashingEnnemy;
    }
    #endregion


    #region Event
    private void OnGameStateChanged(GameState state) {
         
        if(state == GameState.PlayerTurn) {
            // cheeseUIObject.SetActive(true);
            return;
        }
        if(state == GameState.EnnemyTurn) {
            // cheeseUIObject.SetActive(true);
            return;
        }

        // cheeseUIObject.SetActive(true);
    }   

    private void OnGetDaCheese() {
        SetCheeseUILevel();
    }  

    private void OnSmashingEnnemy(GameObject ennemyObject) {
        SetCheeseUILevel();
    }   
    #endregion
    

    #region Cheese UI
    private void SetCheeseUI() {
        for(int i = 0; i < playerInfo.maxCheeseLevel; i++) {
            Vector3 newPosition = cheeseUIObject.transform.position + new Vector3(i * step.x - start.x, i * step.y - start.y, 0);
            CheeseLevel newCheeseLevel = Instantiate(cheeseLevelPrefab, newPosition, Quaternion.identity, cheeseUIObject.transform);
            newCheeseLevel.Initialize(_cheeseUIAnimationQueue);
            _cheeseLevelList.Add(newCheeseLevel);
        }        
    }

    private void SetCheeseUILevel() {
        for(int i = 0; i < playerInfo.cheeseLevel; i++) {
            _cheeseLevelList[i].Show();
        }     
        for(int i = playerInfo.cheeseLevel; i < playerInfo.maxCheeseLevel; i++) {
            _cheeseLevelList[i].Hide();
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
