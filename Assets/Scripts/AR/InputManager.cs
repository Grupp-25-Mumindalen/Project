using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// InputManager checks the number of touch inputs, and flags it
public class InputManager : MonoBehaviour
{
    private int touchCount;
    private ARTapPlace place;
    private TouchPhase touchPhase;
    private RotateModel rot;

    // Start is called before the first frame update
    void Start()
    {
        place = this.GetComponent<ARTapPlace>();
        rot = this.GetComponent<RotateModel>();
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
                if(!place.active) {
                    place.PlaceObject();
                }
            break;
            case 2:
                rot.RotateRoundCenter(Input.GetTouch(0), Input.GetTouch(1));
            break;
            default:
            break;
        }
    }
}
