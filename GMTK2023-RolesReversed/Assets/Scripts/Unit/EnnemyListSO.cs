using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnnemyList", menuName = "EnnemyList", order = 1)]

public class EnnemyListSO : ScriptableObject
{
    public List<EnnemyDataSO> ennemyDatas;
    public Dictionary<EnnemyType, EnnemyDataSO> ennemyDic = new();
    private void OnEnable()
    {
        foreach (EnnemyDataSO ennemyData in ennemyDatas)
        {
            ennemyDic.Add(ennemyData.ennemyType, ennemyData);
        }
    }
}
