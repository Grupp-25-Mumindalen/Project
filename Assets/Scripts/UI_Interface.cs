using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelManager))]
public class UI_Interface : MonoBehaviour
{
    LengthHandler lengthHandler;
    Timer timer;
    WeightSelector weightSelector;
    PendulumManager pendulumManager;

    private bool simulating = false;
    private void Start()
    {
        lengthHandler = GetComponent<LengthHandler>();
        timer = GetComponent<Timer>();
        weightSelector = GetComponent<WeightSelector>();
        GeneralEventHandler.current.onPendulumSimulationStart += OnStartSimulation;
        GeneralEventHandler.current.onPendulumSimulationStop += OnStopSimulation;
    }

    private void OnDestroy()
    {
        GeneralEventHandler.current.onPendulumSimulationStart -= OnStartSimulation;
        GeneralEventHandler.current.onPendulumSimulationStop -= OnStopSimulation;
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










    //Handling of events
    private void OnStartSimulation ()
    {
        simulating = true;
    }
    private void OnStopSimulation ()
    {
        simulating = false;
    }
}
