using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-700)]
public class WeightSelector : MonoBehaviour
{
    // Start is called before the first frame update
    private PendulumManager pendulum;
    private int currentSelection;
    [SerializeField] private WeightList weightList;
    private bool canDoSelection = true;
    [SerializeField] private List<int> uniqueSelections = new List<int>();
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

    private void OnCreatePendulum ()
    {
        canDoSelection = true;
        pendulum = PendulumManager.current;
        ChangeWeight(0);
    }

    private void OnDestroyPendulum ()
    {
        pendulum = null;
    }

    private void OnPendulumSimulationStart()
    {
        canDoSelection = false;
        if (!uniqueSelections.Contains(currentSelection))
            uniqueSelections.Add(currentSelection);
        ExperimentUIController.current.RemoveElementsReplaceObject();
    }

    private void OnPendulumSimulationStop()
    {
        canDoSelection = true;
        ExperimentUIController.current.AddElementsPlaceObject();
    }

    private void OnGoToNextLevel ()
    {
        uniqueSelections = new List<int>();
    }

    public void ChangeWeight(int index)
    {
        GameObject prefab = weightList.weights[index].prefab;
        if (pendulum && prefab && canDoSelection)
        {
            currentSelection = index;
            pendulum.ChangeWeight(prefab);
            PendulumManager.current.SetPendulum2DRotation(PendulumManager.current.GetDefaultAngle());
        }
    }

    public List<int> GetUniqueWeightSelections()
    {
        return uniqueSelections;
    }
}
