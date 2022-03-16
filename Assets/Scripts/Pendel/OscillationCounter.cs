using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillationCounter : MonoBehaviour
{
    [SerializeField] private int oscillations;
    private void Start()
    {
        GeneralEventHandler.current.onPendulumSimulationStop += OnStopSimulation;
        GeneralEventHandler.current.onPendulumNewOscillation += OnOscillation;
    }

    private void OnDestroy()
    {
        GeneralEventHandler.current.onPendulumSimulationStop -= OnStopSimulation;
        GeneralEventHandler.current.onPendulumNewOscillation -= OnOscillation;
    }

    private void OnOscillation()
    {
        oscillations += 1;
    }

    private void OnStopSimulation()
    {
        oscillations = 0;
    }

    public int GetOscillations()
    {
        return oscillations;
    }
}
