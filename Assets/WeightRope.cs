using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightRope : MonoBehaviour
{
    public Transform parent;
    public Transform anchor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (parent.position + anchor.position) / 2;
        transform.localScale = new Vector3(0.1f, Vector2.Distance(anchor.position, parent.position)/0.4f, 0.1f);
    }
}
