using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour


{
    public Transform target;
    public Vector3 offset;
    public Vector3 minVal, maxVal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    // Follows player on the x and z axis
    void Follow() {
        // Player position
        Vector3 targetPos = target.position+offset;

        // Sets camera bound
        Vector3 camBoundPos = new Vector3(Mathf.Clamp(targetPos.x,minVal.x,maxVal.x),0f,Mathf.Clamp(targetPos.z,minVal.z,maxVal.z));

        // sets the camera position
        Vector3 setPosition = transform.position;
        setPosition.x = target.transform.position.x + offset.x;
        setPosition.z = target.transform.position.z + offset.z;
        transform.position = camBoundPos;
    }
    
}
