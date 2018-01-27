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
        Vector2 pos = RectTransformUtility.WorldToScreenPoint(m_cam, buttonTarget.transform.position);
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponentInParent<RectTransform>(), pos, m_cam, out localPos);
        rect.transform.position = localPos;
    }
}
