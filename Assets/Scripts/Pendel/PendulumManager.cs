using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(100)]
public class PendulumManager : MonoBehaviour
{
    public static PendulumManager current;
    private GameObject pendulum;
    [SerializeField] private GameObject anchor;
    [SerializeField] private float defaultAngle = -50;
    private bool is3D = false;
    private Vector3 gravity;
    private float armLength;
    private float formerLength;
    private float objectMass;
    private float objectDrag;
    private float objectArea;
    private float prototypeDamp;
    private static float simulationSpeed = 200;
    [SerializeField] private int formerDirection = 0;
    [SerializeField] private int formerSpeedDirection = 0;

    private float angularVelocity = 0;
    private float acceleration = 0;
    private float pendulumAngle;

    private int phaseCounter = 0;

    private bool initializedDirections = false;
    private bool isActive = false;
    private float dragScale = 0;

    private float baselength = -4.45f;

    private void Start()
    {
        if (current != null)
            throw new System.Exception("Only one PendulumManager should be active at a time.");
        else
            current = this;
        GeneralEventHandler.current.CreatePendulum();
        GeneralEventHandler.current.onPendulumSimulationStart += OnStartSimulation;
        GeneralEventHandler.current.onPendulumSimulationStop += OnStopSimulation;
        ResetPendulum();
    }

    private void OnDestroy()
    {
        GeneralEventHandler.current.StopPendulumSimulation();
        current = null;
        GeneralEventHandler.current.DestroyPendulum();
        GeneralEventHandler.current.onPendulumSimulationStart -= OnStartSimulation;
        GeneralEventHandler.current.onPendulumSimulationStop -= OnStopSimulation;
    }

    public void OnStartSimulation()
    {
        //ResetPendulum(); this shit is honestly not needed?
        InitPendulum();
        SetPendulumActivity(true);
    }

    public void OnStopSimulation()
    {
        SetPendulumActivity(false);
        PauseReset();
        //ResetPendulum();
    }

    public void SetPendulum2DRotation(float angle)
    {
        pendulumAngle = angle;
        Quaternion q = Quaternion.Euler(0, 0, pendulumAngle);
        transform.rotation = q;
    }

    public void ChangeWeight(GameObject newObject)
    {
        if(pendulum)
        {
            Destroy(pendulum);
        }
        GameObject obj = Instantiate(newObject, anchor.transform, false);
        obj.transform.localPosition = Vector3.zero;
        pendulum = obj;
    }

    public float GetDefaultAngle(){
        return defaultAngle;
    }

    /*
     * This function Initializes the pendulum. Call it anytime you start the experiment.
     */
    public void InitPendulum()
    {
        if (gravity.y == 0)
        {
            gravity.y = Physics.gravity.y;
        }

        armLength = Vector3.Distance(pendulum.transform.position, transform.position);
        Weight weight = pendulum.GetComponent<Weight>();
        objectMass = weight.GetWeight();
        prototypeDamp = weight.GetDamping();
        pendulumAngle = transform.rotation.eulerAngles.z;
    }

    //Set whether or not the pendulum should simulate
    public void SetPendulumActivity (bool value)
    {
        isActive = value;
    }

    //Set the scaling of drag. Convenient for defining a vacuum, earth-like drag or something inbetween.
    public void SetDragScale(float dragScale)
    {
        this.dragScale = dragScale;
    }

    //Get baselength for pendulum, for resetting between levels
    public float GetBaselength(){
        return baselength;
    }

    //Reset all pendulum values, speed, length, angle
    public void ResetPendulum ()
    {
        initializedDirections = false;
        acceleration = 0;
        angularVelocity = 0;
        formerDirection = 0;
        formerSpeedDirection = 0;
        phaseCounter = 0;
        //this.SetLength(baselength); //okej detta funkar inte just nu BUG NEJ DEN FINNS I LENGTHHANDLER NU

    }

    // Done on pause, resets speed and supposedly angle
    public void PauseReset(){
        initializedDirections = false;
        acceleration = 0;
        angularVelocity = 0;
        formerDirection = 0;
        formerSpeedDirection = 0;
        phaseCounter = 0;
        // TODO: RESET ANGLE
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if(pendulumAngle>180)
            {
                pendulumAngle -= 360; //one side of the pendulum arc gets positive degrees, the other gets negatives
            }
            acceleration = (gravity.y / armLength) * Mathf.Sin(pendulumAngle * Mathf.Deg2Rad) * simulationSpeed;

            angularVelocity += acceleration * Time.deltaTime;

            angularVelocity *= 1 - (prototypeDamp * Time.deltaTime) * dragScale;

            pendulumAngle += this.angularVelocity * Time.deltaTime;

            Quaternion q = Quaternion.Euler(0, 0, pendulumAngle);

            transform.rotation = q;

            DoPhaseCheck();
        }
    }



    /*
     * Checks in which direction the pendulum currently points, and where its velocity vector points towards. 
     * When the velocity switches sign, we have switched phase of oscillation
     * When the angle passes the 360-degree mark from any direction, the pendulum has gone through the mid-point
     */
    public void DoPhaseCheck()
    {
        if (!initializedDirections) InitializeDirections();

        int currentDir = pendulumAngle > 0 ? 1 : -1;

        if (currentDir != formerDirection)
        {
            MidPass();
        }

        int currentSpeedDir = angularVelocity > 0 ? 1 : -1;

        if (currentSpeedDir != formerSpeedDirection)
        {
            PhaseChange();
        }

        formerDirection = pendulumAngle > 0 ? 1 : -1;

        formerSpeedDirection = angularVelocity > 0 ? 1 : -1;
    }



    //Set the initial values for the tracer variables
    public void InitializeDirections()
    {
        formerDirection = pendulumAngle > 0 ? 1 : -1;
        formerSpeedDirection = angularVelocity > 0 ? 1 : -1;
        initializedDirections = true;
    }

    //Call the event manager to call a custom event for passing the middle
    public void MidPass ()
    {
        GeneralEventHandler.current.PendulumPassMiddle();
    }

    //The pendulum has reached a high point and will switch phase. If this is the second switch in a row, the pendulum has done a full oscillation
    public void PhaseChange ()
    {
        phaseCounter++;
        if(phaseCounter == 2)
        {
            phaseCounter = 0;
            FinishOscillation();
        }
    }

    //The pendulum has made a full oscillation. Call a custom event from the event manager
    public void FinishOscillation()
    {
        GetComponent<AudioSource>().Play();
        GeneralEventHandler.current.PendulumNewOscillation();
    }


    public float GetPendulumDistance()
    {
        return anchor.transform.localPosition.y;
    }

    public void SetPendulumDistance(float distance)
    {
        Vector3 v = anchor.transform.localPosition;
        v.y = -Mathf.Abs(distance);
        anchor.transform.localPosition = v;
    }

    public Vector3 GetAnchorPosition()
    {
        return anchor.transform.position;
    }

    public void SetAirResistance(float resistance)
    {
        dragScale = resistance;
    }

    public void SetGravity(float gravity)
    {
        this.gravity.y = gravity;
    }

    public void Toggle3D(bool value)
    {
        is3D = value; 
    }
}
