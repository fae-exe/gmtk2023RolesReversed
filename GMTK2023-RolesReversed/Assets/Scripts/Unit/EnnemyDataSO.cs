using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnnemyData",menuName = "EnnemyData",order = 1)]
public class EnnemyDataSO : ScriptableObject
{
    public string unitName;
    public string unitID;

    public Sprite northSprite;
    public Sprite eastSprite;
    public Sprite southSprite;
    public Sprite westSprite;

    public EnnemyUnitScript script;

}
