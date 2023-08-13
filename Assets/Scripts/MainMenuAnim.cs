using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnim : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] float animSpeed;

    // Update is called once per frame
    void Update()
    {
        cameraTransform.position = new Vector3(cameraTransform.position.x+animSpeed*Time.deltaTime,cameraTransform.position.y,cameraTransform.position.z);
    }
}
