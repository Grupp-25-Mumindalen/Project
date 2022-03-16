using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// InputManager checks the number of touch inputs, and flags it
public class InputManager : MonoBehaviour
{
    private int touchCount;
    private ARTapPlace place;
    private TouchPhase touchPhase;
    // Start is called before the first frame update
    void Start()
    {
        place = this.GetComponent<ARTapPlace>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the screen is being touched, if so, checks the phase of the fingers (the first) to see if it just began
        touchCount = Input.touchCount;
        switch(touchCount){
            case 0:
            break;
            case 1:
                touchPhase = Input.GetTouch(0).phase;
                if(touchPhase == TouchPhase.Began){
                    // If the object has not been placed, place it
                    if (!place.active)
                    {
                        place.PlaceObject();
                    }
                    /* This should not be the primary way to Start/Stop Simulation.
                    else if(!PendulumManager.isActive)
                    {
                        EventHandler.current.PendulumSimulationStart();
                    }
                    else
                    {
                        EventHandler.current.PendulumSimulationStop();
                    }
                    */
                }
            break;
            case 2:
                touchPhase = Input.GetTouch(0).phase;
                if(touchPhase == TouchPhase.Began && place.active){
                    // If the object has been placed, remove it
                        place.ResetAR();
                }
            break;
            default:
            break;
        }
    }
}
