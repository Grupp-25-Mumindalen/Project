using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * GoalHandler
 * 
 * Tracks events happening in the different scripts and relays updates to subscribed entities. 
 * Using an event handler allows for better compartmentilization and scaleability.
 * 
 * NOTE: If execution breaks upon importing this to the main application, it is most likely related
 * to the script execution order. Since this is the main event handler and uses a singleton value to be
 * called by other scripts, it must execute before any dependent scripts.
 * 
 * 
 * Written by Kei Duke-Bergman, [fill in here if you have worked on this script]
 */

public class GoalHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventHandler.current.onPendulumNewOscillation += OnPendulumNewOscillation;
        EventHandler.current.onPendulumPassMiddle += OnPendulumPassMiddle;
    }

    private void OnDestroy()
    {
        EventHandler.current.onPendulumNewOscillation -= OnPendulumNewOscillation;
        EventHandler.current.onPendulumPassMiddle -= OnPendulumPassMiddle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPendulumNewOscillation()
    {
        
    }

    private void OnPendulumPassMiddle ()
    {
        GetComponent<AudioSource>().Play();
    }
}
