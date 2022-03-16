using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * EventHandler
 * 
 * Tracks events happening in the different scripts and relays updates to subscribed entities. 
 * Using an event handler allows for better compartmentilization and scaleability.
 * 
 * NOTE: If execution breaks upon importing this to the main application, it is most likely related
 * to the script execution order. Since this is the main event handler and uses a singleton value to be
 * called by other scripts, it must execute before any dependent scripts.
 * 
 * 
 * Written by Kei Duke-Bergman
 */


[DefaultExecutionOrder(-900)]
public class GeneralEventHandler : MonoBehaviour
{
    public static GeneralEventHandler current;
    void Start()
    {
        current = this;
    }

    //Define a custom event onPendulumPassMiddle
    public event Action onPendulumPassMiddle;
    /*
     * This function gets called by the pendulum upon the bob passing through the middle
     * If it has any functions tied to the custom event declared above, it calls those.
     */
    public void PendulumPassMiddle()
    {
        if (onPendulumPassMiddle != null)
        {
            onPendulumPassMiddle();
        }
    }

    //Define a custom event onPendulumNewOscillation
    public event Action onPendulumNewOscillation;
    /*
     * This function gets called by the pendulum upon the bob having done a full oscillation
     * If it has any functions tied to the custom event declared above, it calls those.
     */
    public void PendulumNewOscillation()
    {
        if (onPendulumNewOscillation != null)
        {
            onPendulumNewOscillation();
        }
    }

    //Define a custom event onPendulumStartSimulation
    public event Action onPendulumSimulationStart;
    /*
     * This function gets called by SimulationToggle upon starting the simulation
     * If it has any functions tied to the custom event declared above, it calls those.
     */
    public void StartPendulumSimulation()
    {
        if (onPendulumSimulationStart != null)
        {
            onPendulumSimulationStart();
        }
    }

    //Define a custom event onPendulumStopSimulation
    public event Action onPendulumSimulationStop;
    /*
     * This function gets called by SimulationToggle upon stopping the simulation
     * If it has any functions tied to the custom event declared above, it calls those.
     */
    public void StopPendulumSimulation()
    {
        if (onPendulumSimulationStop != null)
        {
            onPendulumSimulationStop();
        }
    }

    public event Action onSuccessConditionMet;
    public void SuccessConditionMet()
    {
        if (onSuccessConditionMet != null)
        {
            onSuccessConditionMet();
        }
    }

    public event Action onGoToNextLevel;
    public void GoToNextLevel()
    {
        if(onGoToNextLevel != null)
        {
            onGoToNextLevel();
        }
    }

    public event Action onCreatePendulum;
    public void CreatePendulum()
    {
        if(onCreatePendulum != null)
        {
            onCreatePendulum();
        }
    }

    public event Action onDestroyPendulum;
    public void DestroyPendulum()
    {
        if(onDestroyPendulum != null)
        {
            onDestroyPendulum();
        }
    }
}
