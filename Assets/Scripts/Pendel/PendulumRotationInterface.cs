using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumRotationInterface : MonoBehaviour
{
    private Camera _camera;
    private float firstHitPosX;
    private float firstHitPosY;

    private float direction;
    private int change = 0;

    [SerializeField] int incrementSize = 10; // in angles
    [SerializeField] int maxIncrements = 10; // before change direction

    void Start()
    {
        direction = 1;
        _camera = Camera.main;
    }

    void Update()
    {

#if UNITY_EDITOR
        EditorTouch();
        return;
#endif

        // One finger
        if (Input.touchCount == 1)
        {

            // Tap on Object
            // if (Input.GetTouch(0).phase == TouchPhase.Began)
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = _camera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                // !!! I WILL MURDER SOMEONE IF I CHANGE THIS INSTEAD OF EDITOR VERSION AGAIN AND SPEND
                // 20 MIN SEARCHING FOR ANSWERS THAT GOD IS NOT WILLING TO GIVE ME
                // (meaning this if-statement is basically going to be the edit one copy pasted but its not here rn)
            }

            // Release
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
            }
        }
    }

    private void EditorTouch()
    {
        // One finger
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000.0f, LayerMask.GetMask("Surface")))
            {
                if (hit.transform == hit.transform)
                {
                    firstHitPosX = transform.position.x;
                    // firstHitPosY = transform.position.y;

                    // increment one step
                    Move();

                    // change direction each time
                    // change direction uuh sometimes
                        if (change == maxIncrements)
                        {
                            direction = -direction;
                            change = 0;
                            Debug.Log("Direction changed");
                        } else {
                            change = change + 1;
                        }
                    ;
                }
            }
        }

        // Release
            if (Input.GetMouseButtonUp(0))
            {
            }
        }

    private float _moved = 0.0f;

    void Move()
    {
#if UNITY_EDITOR
        EditorMove();
        return;
#endif

        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.GetTouch(0).position);

        // !!! implement rest of fn exactly as EditorMove
    }

    void EditorMove()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
         if (direction > 0) {
                transform.rotation = transform.rotation * Quaternion.Euler(0, 0, incrementSize);
            } else {
                transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -incrementSize);
            }

            /*
        if (Physics.Raycast(ray, out hit, 300.0f))
        {
            if (direction > 0) {
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z + 10);
            } else {
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z -10);
            }
        }*/
    }
}
