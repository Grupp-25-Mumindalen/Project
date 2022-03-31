using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelList", menuName = "ScriptableObjects/LevelList", order = 0)]
public class LevelList : ScriptableObject
{
    public List<Level> levels = new List<Level>();
}
