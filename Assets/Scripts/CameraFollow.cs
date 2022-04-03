using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour

{
    public Transform target;
    public Vector3 offset;
    public Vector3 minVal, maxVal;
    public GameObject stairways;
    public bool isUpstairs = false;

    void Update()
    {
        Follow();
    }

    // Follows player on the x and z axis
    void Follow() {
        // Player position
        Vector3 targetPos = target.position+offset;

        // Moves The camera up when going upstairs
        if(isUpstairs) {
            offset.y += 0.02f;
            if(offset.y >= 2.85f) {
                offset.y = 2.85f;
            }
        }
        else if(!isUpstairs) {
            offset.y -= 0.02f;
            if(offset.y <=0f) {
                offset.y = 0f;
            }
        }

        // Sets camera bound
        Vector3 camBoundPos = new Vector3(Mathf.Clamp(targetPos.x,minVal.x,maxVal.x),offset.y,Mathf.Clamp(targetPos.z,minVal.z,maxVal.z));
        transform.position = camBoundPos;
    }
    
}
