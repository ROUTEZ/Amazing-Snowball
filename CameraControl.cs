using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;


    public float smoothSpeed = 0.1f;
    public float xOffset, yOffset, zOffset;
    public float moveRate = 1f;

    private float csX, csY, csZ;


    void Start()
    {

        
    }
   
    void Update()
    {
        //csX = target.transform.lossyScale.x / 2;
        csY = target.transform.localScale.y/2;
        csZ = target.transform.localScale.z;

        //xOffset += csX;
        yOffset += (csY * Time.deltaTime * moveRate);
        zOffset -= (csZ * Time.deltaTime * moveRate);

        //transform.position = setPosition;


        ///transform.position = target.transform.position + new Vector3(xOffset, yOffset, zOffset);
        //transform.LookAt(target.position);
    }


    void LateUpdate()
    {
        //Vector3 desiredPosition = target.position + offset;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //transform.position = smoothedPosition;
        //transform.LookAt(target);

        Vector3 setPosition = transform.position;
        setPosition.x = target.transform.position.x + xOffset;
        setPosition.y = target.transform.position.y + yOffset;
        setPosition.z = target.transform.position.z + zOffset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, setPosition, smoothSpeed);

        transform.position = smoothedPos;




    }
}

