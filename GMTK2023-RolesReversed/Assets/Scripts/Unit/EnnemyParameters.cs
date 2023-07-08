using UnityEngine;

public class EnnemyParameters : MonoBehaviour {
    public static EnnemyParameters instance;

    // Ennemy info
    public float ennemyTurnTime;


    #region Starts
    private void Awake() {
        CheckInstance();
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
