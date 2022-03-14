using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weightusweightus : MonoBehaviour
{
    [SerializeField] private float mass;
    //[SerializeField] private float dragCoefficient;
    //[SerializeField] private float frontalArea;
    [SerializeField] private float prototypeDamping;
    public float GetWeight()
    {
        return mass;
    }

    /*
    public float GetDragCoefficient()
    {
        return dragCoefficient;
    }

    public float GetFrontalArea ()
    {
        return frontalArea;
    }
    */

    public float GetPrototypeDamping()
    {
        return prototypeDamping;
    }
}
