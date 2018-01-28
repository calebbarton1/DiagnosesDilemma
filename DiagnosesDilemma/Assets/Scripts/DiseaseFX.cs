using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseFX : MonoBehaviour
{
    private Vector3 origPos;
    // Use this for initialization
    void Start()
    {
        origPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Shake(float _speed, float _amount, float _timer)
    {
        while (_timer > 0)
        {
            Vector3 pos = new Vector3();
            pos.x = Mathf.Sin(Time.time * _speed) * _amount;
            pos.z = Mathf.Cos(Time.time * _speed) * _amount;
            transform.position = pos;

            _timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = origPos;
    }
}
