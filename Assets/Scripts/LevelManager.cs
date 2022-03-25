using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
[RequireComponent(typeof(OscillationCounter))]
[RequireComponent(typeof(WeightSelector))]
[RequireComponent(typeof(LengthHandler))]
[DefaultExecutionOrder(-800)]
public class LevelManager : MonoBehaviour
{
    public static LevelManager current;


    [SerializeField] private Level level;
    private bool metSuccessCondition = false;
    [SerializeField] private int levelIndex = 0;
    [SerializeField] private LevelList levelList;

    private Timer timer;
    private OscillationCounter counter;
    private WeightSelector weightSelector;
    private PendulumManager pendulum;

    // Start is called before the first frame update
    void Start()
    {
        level = levelList.levels[0];
        timer = this.GetComponent<Timer>();
        counter = this.GetComponent<OscillationCounter>();
        weightSelector = this.GetComponent<WeightSelector>();
        GeneralEventHandler.current.onGoToNextLevel += OnGoToNextLevel;
        GeneralEventHandler.current.onCreatePendulum += OnCreatePendulum;
        GeneralEventHandler.current.onDestroyPendulum += OnDestroyPendulum;
    }

    private void OnDestroy()
    {
        GeneralEventHandler.current.onGoToNextLevel -= OnGoToNextLevel;
        GeneralEventHandler.current.onCreatePendulum -= OnCreatePendulum;
        GeneralEventHandler.current.onDestroyPendulum -= OnDestroyPendulum;
    }

    // Update is called once per frame
    void Update()
    {
        if (!metSuccessCondition)
        {
            if (MeetsSuccesCondition())
            {
                ClearLevel();
            }
        }
    }

    void OnCreatePendulum()
    {
        pendulum = PendulumManager.current;
    }

    void OnDestroyPendulum()
    {
        pendulum = null;
    }


    bool MeetsSuccesCondition()
    {
        bool returnVal = true;
        if(level.successConditions.Length == 0)
        {
            returnVal = false;
        }
        foreach (Level.SuccessCondition condition in level.successConditions)
        {
            float checkValue = 0;
            switch (condition.successCondition)
            {
                case Level.SuccessCondition.Condition.PENDULUM_OSCILLATION:
                    checkValue = counter.GetOscillations();
                    break;
                case Level.SuccessCondition.Condition.TIME_PER_OSCILLATION:
                    checkValue = timer.GetLatestTime();
                    break;
                case Level.SuccessCondition.Condition.UNIQUE_WEIGHT_SELECTIONS:
                    checkValue = weightSelector.GetUniqueWeightSelections().Count;
                    break;
            }

            float minBound = condition.conditionValue - condition.boundMin;
            float maxBound = condition.conditionValue + condition.boundMax;

            if (checkValue < minBound || checkValue > maxBound)
            {
                returnVal = false;
            }
        }
        return returnVal;
    }

    void ClearLevel()
    {
        metSuccessCondition = true;
        GeneralEventHandler.current.SuccessConditionMet();
        OnGoToNextLevel();
    }

    public void OnGoToNextLevel()
    {
        levelIndex += 1;
        level = levelList.levels[levelIndex];
        SetConstraints();
        GeneralEventHandler.current.StopPendulumSimulation();
    }

    public void SetConstraints()
    {
        pendulum.SetAirResistance(level.airResistance);
        pendulum.SetGravity(level.gravity);
        pendulum.Toggle3D(level.is3D);
    }
}


