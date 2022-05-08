using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelManager))]
[DefaultExecutionOrder(-700)]
public class UIInterface : MonoBehaviour
{
    private LevelManager levelManager;
    private LengthHandler lengthHandler;
    private Timer timer;
    private WeightSelector weightSelector;
    private PendulumManager pendulumManager;
    [SerializeField] private ExperimentUIController uiController;
    [SerializeField] private GameObject stopImage;


    private bool simulating = false;
    private void Start()
    {
        if (!uiController)
            Debug.LogError("No UI controller assigned.");
        else
            uiController.SetInterface(this);
        levelManager = LevelManager.current;
        lengthHandler = GetComponent<LengthHandler>();
        timer = GetComponent<Timer>();
        weightSelector = GetComponent<WeightSelector>();
        GeneralEventHandler.current.onPendulumSimulationStart += OnStartSimulation;
        GeneralEventHandler.current.onPendulumSimulationStop += OnStopSimulation;
        GeneralEventHandler.current.onNextLevelLoaded += OnNextLevelLoaded;
        GeneralEventHandler.current.onSuccessConditionMet += OnSuccessConditionMet;
    }

    private void OnDestroy()
    {
        GeneralEventHandler.current.onPendulumSimulationStart -= OnStartSimulation;
        GeneralEventHandler.current.onPendulumSimulationStop -= OnStopSimulation;
        GeneralEventHandler.current.onNextLevelLoaded -= OnNextLevelLoaded;
        GeneralEventHandler.current.onSuccessConditionMet -= OnSuccessConditionMet;
    }

    private void Update()
    {
        if(!pendulumManager && PendulumManager.current)
        {
            pendulumManager = PendulumManager.current;
        }
    }



    public void TogglePendulumSimulation()
    {
        if(!simulating)
            GeneralEventHandler.current.StartPendulumSimulation();
        else
            GeneralEventHandler.current.StopPendulumSimulation();
    }
    public void AdjustPendulumLength(float length)
    {
        lengthHandler.AdjustLength(length);
    }
    public void SetPendulumLength(float totalLength)
    {
        lengthHandler.SetLength(totalLength);
    }
    public void SelectWeight(int weightID)
    {
        if(weightSelector)
        {
            weightSelector.ChangeWeight(weightID);
        }
    }
    public void ToggleAirResistance()
    {
      if(pendulumManager.dragScale==0)
        pendulumManager.SetDragScale(1);
      else
        pendulumManager.SetDragScale(0);

    }




    //Handling of events
    private void OnNextLevelLoaded()
    {
        Level level = levelManager.GetLevel();
        uiController.LoadLevelUI(level);
    }
    private void OnSuccessConditionMet()
    {
        uiController.ClearLevel();
    }
    private void OnStartSimulation ()
    {
        simulating = true;
        stopImage.SetActive(true);
    }
    private void OnStopSimulation ()
    {
        simulating = false;
        stopImage.SetActive(false);
    }
}
