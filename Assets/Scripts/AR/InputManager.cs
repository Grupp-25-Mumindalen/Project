using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// InputManager checks the number of touch inputs, and flags it
public class InputManager : MonoBehaviour
{
    private int touchCount;
    private ARTapPlace place;
    private TouchPhase touchPhase;
    private bool rotate;
    private float startingPos;
    private Vector2 touchPos;
    [SerializeField]
    private GameObject pos;

    // Start is called before the first frame update
    void Start()
    {
        place = this.GetComponent<ARTapPlace>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the screen is being touched, if so, checks the phase of the fingers (the first) to see if it just began
        touchPos = Input.GetTouch(0).position;
        touchCount = Input.touchCount;
        switch(touchCount){
            case 0:
            break;
            case 1:
                RotateOneTouch();
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

    private void RotateOneTouch()
    {
        touchPhase = Input.GetTouch(0).phase;
        switch (touchPhase)
        {
            case TouchPhase.Began:
                startingPos = touchPos.x;
                break;
            case TouchPhase.Moved:
                rotate = true;
                if (startingPos > touchPos.x)
                {
                    pos.transform.Rotate(Vector3.down, -50f * Time.deltaTime);
                }
                else if (startingPos < touchPos.x)
                {
                    pos.transform.Rotate(Vector3.down, 50f * Time.deltaTime);
                }
                rotate = false;
                break;
            case TouchPhase.Ended:
                if (!place.active && !rotate)
                {
                    place.PlaceObject();
                }
                break;
        }
    }
}
