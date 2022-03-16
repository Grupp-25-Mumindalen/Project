using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationToggle : MonoBehaviour
{

    [SerializeField] private Button m_button;
    private bool physicsOn;
    private GeneralEventHandler eventHandler;

    // Start is called before the first frame update
    void Start()
    {
        m_button.onClick.AddListener(TaskOnClick);
        eventHandler = GeneralEventHandler.current;
        GeneralEventHandler.current.onPendulumSimulationStart += OnStartSimulation;
        GeneralEventHandler.current.onPendulumSimulationStop += OnStopSimulation;
    }

    private void OnDestroy()
    {
        GeneralEventHandler.current.onPendulumSimulationStart -= OnStartSimulation;
        GeneralEventHandler.current.onPendulumSimulationStop -= OnStopSimulation;
    }
    void TaskOnClick()
    {
        physicsOn = !physicsOn;
        if (physicsOn) eventHandler.StartPendulumSimulation();
        else eventHandler.StopPendulumSimulation();
    }

    void OnStartSimulation ()
    {
        physicsOn = true;
    }

    void OnStopSimulation ()
    {
        physicsOn = false;
    }

}
