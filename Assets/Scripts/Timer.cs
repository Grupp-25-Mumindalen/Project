using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float[] formerTime = {};
    [SerializeField] private float currentTime = 0;
    private bool active;

    private void Start()
    {
        EventHandler.current.onPendulumSimulationStart += OnStartSimulation;
        EventHandler.current.onPendulumSimulationStop += OnStopSimulation;
        EventHandler.current.onPendulumNewOscillation += OnOscillation;
    }

    private void OnDestroy()
    {
        EventHandler.current.onPendulumSimulationStart -= OnStartSimulation;
        EventHandler.current.onPendulumSimulationStop -= OnStopSimulation;
        EventHandler.current.onPendulumNewOscillation -= OnOscillation;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            currentTime += Time.deltaTime;
        }
    }

    void OnStartSimulation ()
    {
        currentTime = 0;
        active = true;
    }

    void OnStopSimulation ()
    {
        active = false;
    }

    void OnOscillation ()
    {
        formerTime[1] = currentTime;
        currentTime = 0;
    }
}
