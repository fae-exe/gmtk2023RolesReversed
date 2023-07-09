using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterList", menuName = "CharacterList", order = 1)]

public class CharacterListSO : ScriptableObject
{
    public List<CharacterDataSO> CharacterData;
    public Dictionary<CharacterState, CharacterDataSO> CharacterDic = new();

    private void OnEnable()
    {
        foreach (CharacterDataSO characterData in CharacterData)
        {
            CharacterDic.Add(characterData.characterState, characterData);
        }
    }

    public CharacterDataSO GetData(CharacterState state)
    {
        return CharacterDic[state];
    }
}
