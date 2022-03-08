using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swingyboi2d : MonoBehaviour
{
    Rigidbody rgbd;

    [SerializeField] private Vector3 moveSpeed;
    [SerializeField] private float leftAngle;
    [SerializeField] private float rightAngle;

    bool medurs; //lol need go both directions
    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        BytRiktning();
        Svingelisving();
    }

    private void BytRiktning(){
        if (transform.rotation.z < leftAngle){
            medurs = false;
        }

        if (transform.rotation.z > rightAngle){
            medurs = true;
        }
    }

    private void Svingelisving(){
        if(medurs){
            rgbd.angularVelocity = -moveSpeed;
        }
        if(!medurs){
            rgbd.angularVelocity = moveSpeed;
        }
    }
}
