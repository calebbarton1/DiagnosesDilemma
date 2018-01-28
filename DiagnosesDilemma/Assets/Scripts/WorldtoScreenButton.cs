using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldtoScreenButton : MonoBehaviour
{

    public Camera m_cam;
    public GameObject buttonTarget;

    private RectTransform rect;

    // Use this for initialization
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePos();
    }

    private void UpdatePos()
    {
        Vector2 pos = m_cam.WorldToViewportPoint(buttonTarget.transform.TransformPoint(buttonTarget.transform.position));
        pos.y *= -1;
        rect.anchorMax = pos;
        rect.anchorMin = pos;
    }
}
