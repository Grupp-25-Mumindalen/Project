using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(PendulumManager))]
public class RopeEffect : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private PendulumManager pendulumManager;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        pendulumManager = GetComponent<PendulumManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pendulumPosition = pendulumManager.GetAnchorPosition();
        Vector3[] positions = {transform.position, pendulumPosition };
        lineRenderer.SetPositions(positions);
    }
}
