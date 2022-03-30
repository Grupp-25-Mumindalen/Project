using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public bool rotate { get; private set; } = false;
    private ARTapPlace place;
    private TouchPhase touchPhase;
    [SerializeField]
    private GameObject obj;
    private float startingPos;

    // Start is called before the first frame update
    void Start()
    {
        place = this.GetComponent<ARTapPlace>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void RotateRoundCenter(Touch touch1, Touch touch2)
    {
        switch (touch1.phase)
        {
            case TouchPhase.Moved:
                rotate = true;
                break;
            case TouchPhase.Ended:
                rotate = false;
                break;
            default:
                return;
        }

        //obj = place.getCurrent();
        var touchPos1 = touch1.position;
        var touchPos2 = touch2.position;
        var diff1 = touchPos1 - touch1.deltaPosition;
        var diff2 = touchPos2 - touch2.deltaPosition;

        obj.transform.RotateAround(obj.transform.position, Vector3.up, Vector3.SignedAngle(diff2 - diff1, touchPos2 - touchPos1, Vector3.back));
    }

    /*
    public void RotateOneTouch()
    {
        var pos = Input.GetTouch(0).position;
        touchPhase = Input.GetTouch(0).phase;
        switch (touchPhase)
        {
            case TouchPhase.Began:
                startingPos = pos.x;
                break;
            case TouchPhase.Moved:
                rotate = true;
                if (startingPos > pos.x)
                {
                    obj.transform.Rotate(Vector3.down, -50f * Time.deltaTime);
                }
                else if (startingPos < pos.x)
                {
                    obj.transform.Rotate(Vector3.down, 50f * Time.deltaTime);
                }
                rotate = false;
                break;
            case TouchPhase.Ended:
                if(!rotate)
                    {
                    place.PlaceObject();
                }
                break;
        }
    }
    */
}