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
        AdjustLength(PendulumManager.current.GetPendulumDistance()-PendulumManager.current.GetBaselength());
        PendulumManager.current.SetPendulum2DRotation(PendulumManager.current.GetDefaultAngle());
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
        if (canAdjust)
        {
            if (pendulum)
                SetLength(pendulumDistance - adjustment);
        }
    }


}