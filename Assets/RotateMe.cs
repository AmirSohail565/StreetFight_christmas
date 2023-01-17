using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMe : MonoBehaviour
{
    public float rotateFactor=25f;

    int dir = 1;
    public float rotationSpeed=10f;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localEulerAngles.z>rotateFactor)
        {
            dir *= -1;
        }
        //else if(transform.localEulerAngles.z < (-1 * rotateFactor))
        //{
        //    dir = 1;
        //}
        transform.Rotate(rotationSpeed*Time.deltaTime* (dir * Vector3.forward));
        Debug.Log("Rotation of cat : " + transform.localEulerAngles.z);
    }
}
