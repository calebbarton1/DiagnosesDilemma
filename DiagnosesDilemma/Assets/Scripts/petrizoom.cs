using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class petrizoom : MonoBehaviour {

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update ()
    {
        float input = Input.GetAxis("Mouse ScrollWheel");

        if (input > 0)
            cam.fieldOfView -= 5;
        else if (input < 0)
            cam.fieldOfView += 5;

        if (cam.fieldOfView < 10)
            cam.fieldOfView = 10;
        if (cam.fieldOfView > 50)
            cam.fieldOfView = 50;
    }    
}
