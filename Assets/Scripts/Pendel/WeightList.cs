using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeightList",menuName = "ScriptableObjects/WeightList", order = 0)]
public class WeightList : ScriptableObject
{
    [System.Serializable]
    public class Weight
    {
        public string name;
        public GameObject prefab;
    }

    public List<Weight> weights = new List<Weight>();
}
