using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LengthHandler : MonoBehaviour
{
    private bool canAdjust = true;
    private float pendulumDistance;
    private PendulumManager pendulum;

    void Start()
    {
        GeneralEventHandler.current.onCreatePendulum += OnCreatePendulum;
        GeneralEventHandler.current.onDestroyPendulum += OnDestroyPendulum;
        GeneralEventHandler.current.onPendulumSimulationStart += OnPendulumSimulationStart;
        GeneralEventHandler.current.onPendulumSimulationStop += OnPendulumSimulationStop;
        GeneralEventHandler.current.onGoToNextLevel += OnGoToNextLevel;
    }

    private void OnDestroy()
    {
        GeneralEventHandler.current.onCreatePendulum -= OnCreatePendulum;
        GeneralEventHandler.current.onDestroyPendulum -= OnDestroyPendulum;
        GeneralEventHandler.current.onPendulumSimulationStart -= OnPendulumSimulationStart;
        GeneralEventHandler.current.onPendulumSimulationStop -= OnPendulumSimulationStop;
        GeneralEventHandler.current.onGoToNextLevel -= OnGoToNextLevel;
    }

    private void OnCreatePendulum()
    {
        pendulum = PendulumManager.current;
        pendulumDistance = pendulum.GetPendulumDistance();
        print(pendulumDistance);
        canAdjust = true;
    }
    private void OnDestroyPendulum()
    {
        pendulum = null;
    }
    private void OnPendulumSimulationStart()
    {
        canAdjust = false;
    }
    private void OnPendulumSimulationStop()
    {
        canAdjust = true;
    }
    private void OnGoToNextLevel()
    {
        
        
    }

    public void SetLength(float length)
    {
        if (canAdjust)
        {
            pendulumDistance = length;
            if (pendulum){
                 pendulum.SetPendulumDistance(pendulumDistance);
            }
               
        }
    }

    public void AdjustLength(float adjustment)
    {
        
        if (canAdjust && LengthEditedCheck(adjustment))
        {
            if (pendulum)
                SetLength(pendulumDistance - adjustment);
        }
    }

    public void ResetLength()
    {
         AdjustLength(PendulumManager.current.GetPendulumDistance()-PendulumManager.current.GetBaselength());
    }

    public bool LengthEditedCheck(float adjustment){
        if ((PendulumManager.current.GetPendulumDistance() - adjustment )> -1.5f || ((PendulumManager.current.GetPendulumDistance() - adjustment )< -7f)){
            return false;
        }
        else{
            return true;
        }
    }

}