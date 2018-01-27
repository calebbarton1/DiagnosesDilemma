using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotate : MonoBehaviour
{
    Quaternion currRot;
    Quaternion targetRot;

    // Use this for initialization
    void Start()
    {
        ChooseNewRotTarget();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Quaternion.Angle(currRot, targetRot) > 40f)
        {
            currRot = Quaternion.Lerp(currRot, targetRot, Time.fixedDeltaTime * 0.1f);
            transform.rotation = currRot;
        }
        else
            ChooseNewRotTarget();
    }

    private void ChooseNewRotTarget()
    {
        targetRot = Random.rotation;
        //Debug.Log("New rotation target: " + targetRot);
    }
}
