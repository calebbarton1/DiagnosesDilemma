using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPress : MonoBehaviour
{
    private Vector3 origPos;
    public Camera cam;
    private Vector3 dir;
    // Use this for initialization
    void Start()
    {
        origPos = transform.position;
        dir = cam.transform.forward;
    }

    public void StartPress(float _dist)
    {
        StartCoroutine(Press(_dist));
    }

    private IEnumerator Press(float _dist)
    {
        Vector3 targetPos = transform.position;
        targetPos += dir * 0.03f;

        transform.position = targetPos;

        yield return new WaitForSeconds(0.5f);

        transform.position = origPos;
    }

    public void DisableButt()
    {
        Vector3 targetPos = transform.position;
        targetPos += dir * 0.03f;

        transform.position = targetPos;
    }

    public void ReEnable()
    {
        transform.position = origPos;
    }
}
