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

    private bool m_TransmissionSolved;
    private bool m_StrainSolved;

    public List<GameObject> buttons;

    private int testCount = 0;

    private Color failedCol;

    public GameObject water;

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

        ColorUtility.TryParseHtmlString("#AC27008B", out failedCol);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateTimer();

        if (testCount > 2)
        {
            //endgame
        }
    }

    private void UpdateTimer()
    {
        if (timer > 0)
            timer -= Time.deltaTime;

        else
        {
            //end game?
        }

        TimeSpan time = TimeSpan.FromSeconds(timer);
        timerText.text = string.Format("Time remaining till outbreak:\n {0}m:{1}s", time.Minutes, time.Seconds);

    }

    private void ChangeWaterColor()
    {
        water.GetComponent<MeshRenderer>().material.color = failedCol;
    }

    public void TestTransmission(int _buttonNum)
    {
        ++testCount;
        if (_buttonNum == diseaseHolder.currDisease.myTransmission.myPair.button)
        {
            Debug.LogFormat("Correct Transmission vector: {0}", diseaseHolder.currDisease.myTransmission.transmissionType);
            m_TransmissionSolved = true;
            //press down all buttons
            //foreach(GameObject go in buttons)
            //{
            //    go.GetComponent<buttonPress>().DisableButt();
            //}

            for (int index = 0; index < buttons.Count-2; ++index)
            {
                buttons[index].GetComponent<buttonPress>().DisableButt();
            }
        }

        else
        {
            //do bad thing
            Debug.LogFormat("INCORRECT Transmission vector: {0}", diseaseHolder.currDisease.myTransmission.transmissionType);
            StartCoroutine(fx.Shake(200, 0.01f, 0.3f));
            buttons[_buttonNum-1].GetComponent<buttonPress>().StartPress(0);
            ChangeWaterColor();
        }
    }

    public void TestStrain(int _buttonNum)
    {
        ++testCount;
        if (_buttonNum == diseaseHolder.currDisease.myStrain.button)
        {
            m_StrainSolved = true;
            Debug.LogFormat("Correct Strain Value: {0}", diseaseHolder.currDisease.myStrain.strainType);
            for (int index = 4; index < buttons.Count; ++index)
            {
                buttons[index].GetComponent<buttonPress>().DisableButt();
            }
        }

        else
        {
            //bad thing
            Debug.LogFormat("INCORRECT Strain Value: {0}", diseaseHolder.currDisease.myStrain.strainType);
            StartCoroutine(fx.Shake(200, 0.01f, 0.3f));
            buttons[_buttonNum - 1].GetComponent<buttonPress>().StartPress(0);
            ChangeWaterColor();
        }
    }

    private void CheckAnswer()
    {
        if (m_StrainSolved && m_TransmissionSolved)
        {
            //good game
        }
    }
}
