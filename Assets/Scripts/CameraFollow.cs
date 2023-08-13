using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform camera;
    [SerializeField] Transform player;
    public Vector3 offset;
    public float damping;
    Vector3 velocity= Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        camera.position= new Vector3(player.position.x+3f,camera.position.y,-10f);
    }

    // Update is called once per frame
    void Update()
    {
        camera.position = new Vector3(player.position.x + 3f, camera.position.y,-10f);
        if(player.position.y>=2)
        {
            //Camera should get higher pos
            Vector3 movePosition = new Vector3(player.position.x,player.position.y-2f,-10f)+offset;
            camera.position = Vector3.SmoothDamp(camera.position, movePosition, ref velocity, damping);
        }else if(camera.position.y>0f) 
        {
            Vector3 movePosition = new Vector3(player.position.x, 0f, -10f) + offset;
            camera.position = Vector3.SmoothDamp(camera.position, movePosition, ref velocity, damping);
        }
    }
}
