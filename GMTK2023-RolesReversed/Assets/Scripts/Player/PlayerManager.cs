using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager instance;

    // Player info
    [SerializeField] private Transform player;
    [SerializeField] private PlayerInfo playerInfo;


    #region Starts
    private void Awake() {
        CheckInstance();
    }

    private void Start() {
        CheeseUI.instance.SetCheeseUI(playerInfo.maxCheeseLevel);
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

        box.isCheese = false;
        Destroy(box.cheeseObject);
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
