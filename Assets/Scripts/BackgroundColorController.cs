using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorController : MonoBehaviour
{
    [SerializeField] float colorChangeSpeed;
    [SerializeField] Camera cam;
    [SerializeField] int r, g, b;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeBackGroundColor()
    {
        int changeValue = (int)((cam.transform.position.x * colorChangeSpeed) % 176f);
        r=changeValue;
        if (changeValue == 0) 
        {

        }
        r = (int)((cam.transform.position.x * colorChangeSpeed) % 176f);
    }

}
