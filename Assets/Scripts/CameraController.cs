using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public float followSpeed;
    public float rotateSpeed;

    void Start()
    {
        transform.parent = null; //disconnects camera from car upon game start 
    }
    void Update()
    {
        // syncing the rotation and position of the camera and the car over time
        transform.position = Vector3.Lerp(transform.position,target.position, followSpeed * Time.deltaTime); //Lerp stands for Linear interpolation 
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotateSpeed * Time.deltaTime);
    }
}
