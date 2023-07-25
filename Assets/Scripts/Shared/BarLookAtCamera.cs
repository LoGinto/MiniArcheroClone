using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarLookAtCamera : MonoBehaviour
{
    [SerializeField] bool inverted = false;
    Transform cameraTransform;
    Vector3 dirToCamera;

    void Awake()
    {
        cameraTransform = Camera.main.transform;
    }
    private void LateUpdate()
    {
        //if(Time.frameCount % 2 == 1){
        if (inverted)
        {
            dirToCamera = (cameraTransform.position - transform.position).normalized;
            transform.LookAt(transform.position + dirToCamera * -1);
        }
        else
        {
            transform.LookAt(cameraTransform);
        }
        //}
    }
}
