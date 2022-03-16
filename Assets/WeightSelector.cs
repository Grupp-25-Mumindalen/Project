using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightSelector : MonoBehaviour
{
    // Start is called before the first frame update
    private PendulumManager pendulum;
    [SerializeField] private WeightList weightList;
    private GameObject currentWeight;
    void Start()
    {
        GeneralEventHandler.current.onCreatePendulum += OnCreatePendulum;
        GeneralEventHandler.current.onDestroyPendulum += OnDestroyPendulum;
    }

    private void OnDestroy()
    {
        GeneralEventHandler.current.onCreatePendulum -= OnCreatePendulum;
        GeneralEventHandler.current.onDestroyPendulum -= OnDestroyPendulum;
    }

    private void OnCreatePendulum ()
    {
        pendulum = PendulumManager.current;
    }

    private void OnDestroyPendulum ()
    {
        pendulum = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeWeight(int index)
    {
        if(pendulum)
        {

        }
    }
}
