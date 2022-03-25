using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    [Header("General information")]
    public string name;
    public string description;

    [Header("UI")]
    public Sprite icon;

    [Header("Constraints")]
    public float airResistance = 1;
    public float gravity = 0;
    public bool is3D = true;
    public bool canAdjustAngle = true;
    public bool canAdjustLength = true;
    public bool canAdjustWeight = true;
    public bool canControlAirResistance = true;
    public bool canControlGravity = true;

    [Header("Success condition(s)")]
    public SuccessCondition[] successConditions;

    [System.Serializable]
    public class SuccessCondition
    {
        public Condition successCondition;
        public float conditionValue;
        [Tooltip("How much the actual value is allowed to diverge below the conditionValue")]
        public float boundMin;
        [Tooltip("How much the actual value is allowed to diverge above the conditionValue")]
        public float boundMax;
        public enum Condition
        {
            PENDULUM_OSCILLATION,
            TIME_PER_OSCILLATION,
            UNIQUE_WEIGHT_SELECTIONS
        }
    }
}