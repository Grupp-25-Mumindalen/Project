using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumRotationInterface : MonoBehaviour
{
    private Camera _camera;
    private bool dragging = false;
    private Vector3 touchPrevPos;
    private Vector3 touchPos;
    private float curRot; 
    private float diff;
    private int inFront;
    private Pose cameraAngle;
    private GameObject pendulum;
    private float angleDiff;

    void Start()
    {
        _camera = Camera.main;
        pendulum = this.transform.parent.gameObject; //Necessary for evaluating the angle of the pendulum
        inFront = 1;
    }

    void Update()
    {
    #if UNITY_EDITOR
        EditorTouchEval();
        return;
    #endif
        TouchEval();
    }

    // TouchEval evaluates dragging touch inputs in interaction with the weight
    private void TouchEval()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                //Creates a ray at the position of the touch
                touchPos = touch.position;
                Ray ray = _camera.ScreenPointToRay(touchPos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000.0f, LayerMask.GetMask("Weight"))) //Using the ray, ensures the user pressed the weight
                {
                    dragging = true; 
                    touchPrevPos = touchPos;
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                dragging = false;
            }
            if (dragging) //Only rotates the pendulum if the screen is currently being touched
            {
                //Retrieves the camera rotation
                var cameraForward = _camera.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                cameraAngle.rotation = Quaternion.LookRotation(cameraBearing);

                //Compares the rotation of the camera and pendulum to establish whether the user is in front of or behind the pendulum
                angleDiff = Mathf.Abs(cameraAngle.rotation.eulerAngles.y - pendulum.transform.rotation.eulerAngles.y);  
                if (angleDiff < 90 || angleDiff > 270) //The user is facing the front of the pendulum
                    inFront = 1;
                else //The user is facing the back of the pendulum
                    inFront = -1; 

                touchPos = touch.position;
                Move();
            }
        }
    }

    // EditorTouch evaluates dragging mouse inputs in interaction with the weight
    private void EditorTouchEval()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Creates a ray at the position of the click
            touchPos = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(touchPos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000.0f, LayerMask.GetMask("Weight"))) //Using the ray, ensures the user pressed the weight
            {
                dragging = true;
                touchPrevPos = touchPos;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }
        if(dragging) //Only rotates the pendulum if the button is currently being pressed
        {
            touchPos = Input.mousePosition;
            Move();
        }
    }

    // Move rotates the pendulum in accordance with the touch/mouse input
    private void Move()
    { 
        // Evaluates the change in position of the touch/mouse input
        // If the user is standing behind the pendulum, the movement is inversed to match the direction of the touch/mouse input. (InFront = 1 if the user is in front of the pendulum, -1 otherwise)
        diff = inFront*(touchPos.x - touchPrevPos.x)/10;

        //Retrieves the current rotation of the pendulum
        curRot = transform.rotation.eulerAngles.z;

        // The following if statement restricts rotation to 90 degrees (in either direction) from the rest state of the pendulum
        if ((curRot > 270 || curRot < 90 || (curRot > 90 && curRot < 180 && diff < 0) || (curRot < 270 && curRot > 180 && diff > 0)))
        {
            // Rotates the pendulum in accordance with the movement of the touch/mouse input
            transform.rotation = transform.rotation * Quaternion.Euler(0, 0, diff);
            touchPrevPos = touchPos;
        }
    }
}
