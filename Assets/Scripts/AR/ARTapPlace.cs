using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems; // Needed for TrackableType

/*

ARTapPlace handles the placement of the experiment, enable/disable planes and resetting of the experiment model

*/
public class ARTapPlace : MonoBehaviour
{
    [SerializeField]
    private GameObject placement;
    [SerializeField]
    private GameObject objToPlace;
    [SerializeField]
    private ARSession session;
    
    private Pose placementPose; // Simple data structure that represents a 3D-point
    private ARSessionOrigin arOrigin;
    private ARRaycastManager rayCastMgr; // Needed to Raycast
    private ARPlaneManager arPlaneMgr;

    public bool placementValid { get; private set;} = false;
    public bool active { get; private set;} = false;

    // Start is called before the first frame update
    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        session = FindObjectOfType<ARSession>();
        rayCastMgr = this.GetComponent<ARRaycastManager>();
        arPlaneMgr = this.GetComponent<ARPlaneManager>();
        objToPlace.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!active){
            UpdatePlacementPose();
            UpdatePlacementIndicator();
        }
    }

    /*  PlaceObject()
        Checks for input and validity of placement indicator to place the object
    */
    public void PlaceObject() {
        objToPlace.SetActive(true);
        placementPose.position.y += (float)0.65;
        objToPlace.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        active = true;
        DisablePlanes();
    }

    /*  ResetAR()
        Removes the placed experiment and enables planes again.
    */
    public void ResetAR() {
        objToPlace.SetActive(false);
        active = false;
        EnablePlanes();
    }

    /*
    
    */
    public void EnablePlanes() {
        arPlaneMgr.enabled = true;
    }

    /*
        Should automatically be done when outside frame with the current AR Plane component
    */
    public void DisablePlanes() {
        arPlaneMgr.enabled = false;
        placement.SetActive(false);
        foreach(GameObject plane in GameObject.FindGameObjectsWithTag("Plane")) {
            Destroy(plane);
        }
    }

    /*  UpdatePlacementIndicator()
        Updates placement of object
    */
    private void UpdatePlacementIndicator() {
        if (placementValid) {
            placement.SetActive(true);
            placement.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        } else {
            placement.SetActive(false);
        }
    }

    /*  UpdatePlacementPose()
        Records where to place object
    */
    private void UpdatePlacementPose() {
        // Finding a plane to hit
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f)); // Describes the center point of our screen
        var hitmonchan = new List<ARRaycastHit>();
        // Physics.Raycast();
        // Casts a ray, from point origin, in direction direction, of length maxDistance, against all colliders in the Scence
        rayCastMgr.Raycast(screenCenter, hitmonchan, TrackableType.Planes);
        // Screenpoint where it shoots the ray from,
        // hitResults are objects where the ray hits a physical surface, 
        // Optional trackable type - defaults to .all, collisions with the physical world that can be detected. FeaturePoint is any distinguishing point that can be identified

        placementValid = hitmonchan.Count > 0;
        if (placementValid) {
            // Placement on hit plane
            placementPose = hitmonchan[0].pose;

            // Rotation and placement in relation to camera
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized; // Enhetsvektor, just a direction no scalar
            placementPose.rotation = Quaternion.LookRotation(cameraBearing); // Creates rotation from forward and upward direction
        }
    }
}