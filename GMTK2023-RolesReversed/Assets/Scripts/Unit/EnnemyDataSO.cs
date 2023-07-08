using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnnemyData",menuName = "EnnemyData",order = 1)]
public class EnnemyDataSO : ScriptableObject
{
    public EnnemyType ennemyType;

    public Sprite northSprite;
    public Sprite eastSprite;
    public Sprite southSprite;
    public Sprite westSprite;
}
