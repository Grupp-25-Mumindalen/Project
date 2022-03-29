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

    private void Start()
    {
        lengthHandler = GetComponent<LengthHandler>();
        timer = GetComponent<Timer>();
        weightSelector = GetComponent<WeightSelector>();
    }

    private void Update()
    {
        if(!pendulumManager && PendulumManager.current)
        {
            pendulumManager = PendulumManager.current;
        }
    }

    public void StartPendulum()
    {
        GeneralEventHandler.current.StartPendulumSimulation();
    }
    public void StopPendulum()
    {
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
}
