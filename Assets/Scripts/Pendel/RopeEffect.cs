using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(PendulumManager))]
public class RopeEffect : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private PendulumManager pendulumManager;
    public GameObject gm;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        pendulumManager = GetComponent<PendulumManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = (pendulumManager.transform.position.y + pendulumManager.GetAnchorPosition().y)/2;
        Vector3 pos = gm.transform.localPosition;
        pos.y = distance;
        gm.transform.position = pos;
        Vector3 ls = gm.transform.localScale;
        ls.y = distance * 2;
        gm.transform.localScale = ls;
    }
}
