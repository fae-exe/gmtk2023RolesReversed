using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Grid", menuName = "Scriptable Object/Grid")]
public class GridScriptObj : ScriptableObject {

    // Grid info
    public int gridLevel;
    public Vector2 gridSize;
    public Vector2 gridPlayerStart;
    public List<EnnemyInGrid> allEnnemy = new List<EnnemyInGrid>();
    public List<Box> allBox = new List<Box>();

}




