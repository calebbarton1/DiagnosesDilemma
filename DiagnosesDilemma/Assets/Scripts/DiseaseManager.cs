using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiseaseManager : MonoBehaviour
{
    public Diseases diseaseHolder;
    private GameObject disease;
    private DiseaseFX fx;

    #region TimerStuff
    private double timer;
    public double startTimer;
    public Text timerText;
    #endregion
    // Use this for initialization
    void Start()
    {
        timer = startTimer;
        disease = GameObject.FindGameObjectWithTag("disease");
        fx = disease.GetComponent<DiseaseFX>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if (timer > 0)
            timer -= Time.deltaTime;


        else
        {
            //do thing with time
            //end game?
        }

        TimeSpan time = TimeSpan.FromSeconds(timer);
        timerText.text = string.Format("{0}m:{1}s", time.Minutes, time.Seconds);

    }

    public void TestTransmission(int _buttonNum)
    {
        if (_buttonNum == diseaseHolder.currDisease.myTransmission.myPair.button)
        {
            Debug.LogFormat("Correct Transmission vector: {0}", diseaseHolder.currDisease.myTransmission.transmissionType);
        }

        else
        {
            //do bad thing
            Debug.LogFormat("INCORRECT Transmission vector: {0}", diseaseHolder.currDisease.myTransmission.transmissionType);
            StartCoroutine(fx.Shake(200, 0.01f, 0.3f));
        }
    }

    public void TestStrain(int _buttonNum)
    {
        if (_buttonNum == diseaseHolder.currDisease.myStrain.button)
        {
            Debug.LogFormat("Correct Strain Value: {0}", diseaseHolder.currDisease.myStrain.strainType);
        }

        else
        {
            //bad thing
            Debug.LogFormat("INCORRECT Strain Value: {0}", diseaseHolder.currDisease.myStrain.strainType);
            StartCoroutine(fx.Shake(200, 0.01f, 0.3f));
        }
    }
}
