using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Timer))]
[RequireComponent(typeof (OscillationCounter))]
[DefaultExecutionOrder(-800)]
public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        [Header("Constraints")]
        [SerializeField] public float airResistance = 1;
        [SerializeField] public float gravity = 0;
        [SerializeField] public bool is3D = true;
        [SerializeField] public bool canAdjustAngle = true;
        [SerializeField] public bool canAdjustLength = true;
        [SerializeField] public bool canAdjustWeight = true;
        [SerializeField] public bool canControlAirResistance = true;
        [SerializeField] public bool canControlGravity = true;

        [Header("Success condition")]
        [SerializeField] public SuccessCondition successCondition;
        [SerializeField] public float conditionValue;
        [SerializeField] public float conditionValueBounds;
        public enum SuccessCondition
        {
            PENDULUM_OSCILLATION,
            TIME_PER_OSCILLATION,
            UNIQUE_WEIGHT_SELECTIONS
        }
    }

    [SerializeField] private Level level;
    private bool metSuccessCondition = false;
    [SerializeField] private int levelIndex = 0;
    [SerializeField] private List<Level> levels = new List<Level>();

    private Timer timer;
    private OscillationCounter counter;
    private PendulumManager pendulum;

    // Start is called before the first frame update
    void Start()
    {
        level = levels[0];
        timer = this.GetComponent<Timer>();
        counter = this.GetComponent<OscillationCounter>();
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
        CheckSuccesCondition();
    }

    void OnCreatePendulum()
    {
        pendulum = PendulumManager.current;
    }

    void OnDestroyPendulum()
    {
        pendulum = null;
    }


    void CheckSuccesCondition()
    {
        float checkValue = 0;
        if (!metSuccessCondition)
        {
            switch (level.successCondition)
            {
                case Level.SuccessCondition.PENDULUM_OSCILLATION:
                    checkValue = counter.GetOscillations();
                    break;
                case Level.SuccessCondition.TIME_PER_OSCILLATION:
                    checkValue = timer.GetLatestTime();
                    break;
                case Level.SuccessCondition.UNIQUE_WEIGHT_SELECTIONS:
                    break;
            }
        }
        float minBound = level.conditionValue - level.conditionValueBounds;
        float maxBound = level.conditionValue + level.conditionValueBounds;

        if (checkValue > minBound && checkValue < maxBound)
        {
            SuccessConditionMet();
        }
    }

    void SuccessConditionMet ()
    {
        metSuccessCondition = true;
        GeneralEventHandler.current.SuccessConditionMet();
        OnGoToNextLevel();
    }

    public void OnGoToNextLevel()
    {
        levelIndex += 1;
        level = levels[levelIndex];
        SetConstraints();
    }

    public void SetConstraints()
    {
        GeneralEventHandler.current.StopPendulumSimulation();
        pendulum.SetAirResistance(level.airResistance);
        pendulum.SetGravity(level.gravity);
        pendulum.Toggle3D(level.is3D);
    }
}


