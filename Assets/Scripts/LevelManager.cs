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
        current = this;
        level = levelList.levels[0];
        timer = this.GetComponent<Timer>();
        counter = this.GetComponent<OscillationCounter>();
        weightSelector = this.GetComponent<WeightSelector>();
        GeneralEventHandler.current.onGoToNextLevel += OnGoToNextLevel;
        GeneralEventHandler.current.onCreatePendulum += OnCreatePendulum;
        GeneralEventHandler.current.onDestroyPendulum += OnDestroyPendulum;

        StartCoroutine(SetUpLevelLoad());
    }

    private IEnumerator SetUpLevelLoad()
    {
      yield return new WaitForEndOfFrame();
      GeneralEventHandler.current.NextLevelLoaded();
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
            float minBound = condition.conditionValue - condition.boundMin;
            float maxBound = condition.conditionValue + condition.boundMax;
            
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
                case Level.SuccessCondition.Condition.LONGER_LENGTH:
                    checkValue = Mathf.Abs(PendulumManager.current.GetPendulumDistance()) + PendulumManager.current.GetBaselength(); //!!!
                    
                    minBound = 0.5f; // New one longer than old one, diff>0
                    maxBound = 100f; // Godtyckligt stort tal
                    break;
                case Level.SuccessCondition.Condition.SHORTER_LENGTH:
                    checkValue = Mathf.Abs(PendulumManager.current.GetPendulumDistance()) + PendulumManager.current.GetBaselength(); //!!!
                    
                    
                    minBound = -100f; // Godtyckligt stort (but more like litet) tal
                    maxBound = -0.5f; // New one shorter than old one, diff<0
                    break;
            }

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
        PendulumManager.current.SetPendulumActivity(false);
       // baselength = Mathf.Abs(PendulumManager.current.GetPendulumDistance());// knas b??r 100% flyttas typ NU but this shit works so don't touch
        GeneralEventHandler.current.SuccessConditionMet();
      
    }

    public void OnGoToNextLevel()
    {
        metSuccessCondition = false;
        levelIndex += 1;
        if (levelList.levels[levelIndex] != null)
            level = levelList.levels[levelIndex];
        else
            Debug.LogError("Tried going to a null level. Ascertain that the last level in the list has no success conditions.");
        SetConstraints();
        GeneralEventHandler.current.StopPendulumSimulation();
        PendulumManager.current.ResetPendulum();
        GetComponent<LengthHandler>().ResetLength();
        PendulumManager.current.SetPendulum2DRotation(PendulumManager.current.GetDefaultAngle());
        GeneralEventHandler.current.NextLevelLoaded();
    }

    public Level GetLevel ()
    {
        return level;
    }

    public void SetConstraints()
    {
        pendulum.SetAirResistance(level.airResistance);
        pendulum.SetGravity(level.gravity);
        pendulum.Toggle3D(level.is3D);
    }
}
