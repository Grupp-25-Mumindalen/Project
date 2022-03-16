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
        GeneralEventHandler.current.onPendulumSimulationStart += OnStartSimulation;
        GeneralEventHandler.current.onPendulumSimulationStop += OnStopSimulation;
        GeneralEventHandler.current.onPendulumNewOscillation += OnOscillation;
    }

    private void OnDestroy()
    {
        GeneralEventHandler.current.onPendulumSimulationStart -= OnStartSimulation;
        GeneralEventHandler.current.onPendulumSimulationStop -= OnStopSimulation;
        GeneralEventHandler.current.onPendulumNewOscillation -= OnOscillation;
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
        formerTime[0] = currentTime;
        currentTime = 0;
    }

    public float GetLatestTime()
    {
        return GetFormerTime(0);
    }

    public float GetFormerTime(int index)
    {
        return formerTime[index];
    }

    public void ResetTimes()
    {
        formerTime = new float[3];
    }
}
