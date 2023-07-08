using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheeseUI : MonoBehaviour {
    public static CheeseUI instance;


    // Player info
    public PlayerInfo playerInfo;

    // Cheese info
    [SerializeField] private GameObject cheeseUIObject;
    [SerializeField] private GameObject cheeseLevelPrefab;
    [SerializeField] private Vector2 step;
    [SerializeField] private Vector2 start;
    [SerializeField] private Color colorEmpty;
    [SerializeField] private Color colorFull;



    #region Starts
    private void Awake() {
        CheckInstance();
    }
    #endregion

    #region Cheese UI
    public void SetCheeseUI(int maxCheeseLevel) {
        for(int i = 0; i < maxCheeseLevel; i++) {
            Vector3 newPosition = cheeseUIObject.transform.position + new Vector3(i * step.x - start.x, i * step.y - start.y, 0);
            GameObject newSlot = Instantiate(cheeseLevelPrefab, newPosition, Quaternion.identity, cheeseUIObject.transform);
            newSlot.GetComponent<Image>().color = colorEmpty;
        }        
    }

    public void SetCheeseUILevel(int maxCheeseLevel, int playerCheese) {
        for(int i = 0; i < maxCheeseLevel; i++) {
            // cheeseUIObject.GetChild(i).GetComponent<Image>().color = colorFull;
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
