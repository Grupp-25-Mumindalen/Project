using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopHammerTime : MonoBehaviour
{

    [SerializeField] private Button m_button;
    private bool physicsOn;
    private EventHandler eventHandler;

    // Start is called before the first frame update
    void Start()
    {
        m_button.onClick.AddListener(TaskOnClick);
        eventHandler = EventHandler.current;
    }

    void TaskOnClick()
    {
        /*
        var physicScript = GameObject.Find("Cube").GetComponent<PendulumusPrototypus>();
        if (physicsOn) {
            physicScript.ResetPendulum();
            physicScript.SetPendulumActivity(false);
            physicsOn = false;

        } else {
            physicScript.InitPendulum();
            physicScript.SetPendulumActivity(true);
            physicsOn = true;
        }
        */
        physicsOn = !physicsOn;
        
        if (physicsOn) eventHandler.PendulumSimulationStart();
        else eventHandler.PendulumSimulationStop();
    }

}
