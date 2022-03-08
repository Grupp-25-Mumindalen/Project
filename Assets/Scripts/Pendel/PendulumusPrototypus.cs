using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumusPrototypus : MonoBehaviour
{
    [Header("Debug variables")]
    [SerializeField] private GameObject pendulum;
    [SerializeField] private float objectMass;
    [SerializeField] private Vector3 gravity;
    [SerializeField] private Vector3 cumulativeForce;
    [SerializeField] private Vector3 angularVelocity;
    [SerializeField] private float potentialEnergy;
    [SerializeField] private float totalEnergy;
    [SerializeField] private float currentHeight;
    private float armLength;





    [Header("Real hot variables in your area")]
    private static float simulationSpeed = 100;
    [SerializeField] int formerDirection = 0;
    [SerializeField] int formerSpeedDirection = 0;
    int phaseCounter = 0;
    [SerializeField] float dumbRotationSpeed = 0;
    [SerializeField] float dumbAcceleration = 0;
    [SerializeField] float dumbAngle;
    private void Start()
    {
        if (gravity.y == 0)
        {
            gravity.y = Physics.gravity.y;
        }
        armLength = Vector3.Distance(pendulum.transform.position, transform.position);
        objectMass = pendulum.GetComponent<Weightusweightus>().GetWeight();

        potentialEnergy = CalculatePotentialEnergy();

        totalEnergy = potentialEnergy;
        totalEnergy += 0.1f;
        dumbAngle = transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 ropeDirection = pendulum.transform.up;

        Vector3 forceDir = Vector3.ProjectOnPlane(gravity, ropeDirection);

        Vector3 currentRotation = transform.rotation.eulerAngles;

        float kEnergi = totalEnergy - CalculatePotentialEnergy();

        float velocityMagnitude = Mathf.Sqrt((kEnergi*2)/objectMass);

        Vector3 pendiSpeed = forceDir.normalized * velocityMagnitude;

        print(velocityMagnitude);
        */

        dumbAcceleration = (gravity.y / armLength) * Mathf.Sin(dumbAngle * Mathf.Deg2Rad) * simulationSpeed;

        dumbRotationSpeed += dumbAcceleration * Time.deltaTime;

        dumbAngle += this.dumbRotationSpeed * Time.deltaTime;

        dumbAngle = Mathf.Clamp(dumbAngle, 225, 360 + 135);

        if(formerDirection == 0)
        {
            formerDirection = dumbAngle > 360 ? 1 : -1;
        }

        if (formerSpeedDirection == 0)
        {
            formerSpeedDirection = dumbRotationSpeed > 0 ? 1 : -1;
        }

        Quaternion q = Quaternion.Euler(0, 0, dumbAngle);

        transform.rotation = q;

        int currentDir = dumbAngle > 360 ? 1 : -1;

        if (currentDir != formerDirection)
        {
            MidPass();
        }

        int currentSpeedDir = dumbRotationSpeed > 0 ? 1 : -1;

        if (currentSpeedDir != formerSpeedDirection)
        {
            PhaseChange();
        }

        formerDirection = dumbAngle > 360 ? 1 : -1;
        formerSpeedDirection = dumbRotationSpeed > 0 ? 1 : -1;
    }

    public float CalculatePotentialEnergy ()
    {
        Vector3 pendulumDir = pendulum.transform.position - this.transform.position;

        float angle = Vector3.Angle(pendulumDir, Vector3.down) * Mathf.Deg2Rad;

        float heightFromMommy = Mathf.Cos(angle) * armLength;

        float startHeight = (armLength - heightFromMommy);

        return - (objectMass* (startHeight) * gravity.y);
    }

    public void MidPass ()
    {
        print("Ding in the middle!!!!");
        GetComponent<AudioSource>().Play();
    }

    public void PhaseChange ()
    {
        phaseCounter++;
        if(phaseCounter == 2)
        {
            phaseCounter = 0;
            FinishOscillation();
        }
        print("Halfway there");
    }

    public void FinishOscillation()
    {
        print("Did one full Oscillation");
    }
}
