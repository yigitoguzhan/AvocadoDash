using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundLightFollow : MonoBehaviour
{
    [SerializeField] Transform groundLightTransform;
    [SerializeField] Transform playerTransform;
        
    // Update is called once per frame
    void Update()
    {
        groundLightTransform.position = new Vector3(playerTransform.position.x+3.10f,groundLightTransform.position.y,groundLightTransform.position.z);
    }
}
