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

    public GameObject newspaper;
    public Text newspaperText;

    public AudioClip success;
    public AudioClip fail;
    private AudioSource audsource;

    public Transform otherCamTarget;
    public Camera otherCam;
    public GameObject disableCanv;

    public GameObject targetBlocker;

    #region TimerStuff
    private double timer;
    public double startTimer;
    public Text timerText;
    #endregion
    // Use this for initialization
    void Start()
    {
        audsource = GetComponent<AudioSource>();
        newspaper.SetActive(false);
        timer = startTimer;
        disease = GameObject.FindGameObjectWithTag("disease");
        fx = disease.GetComponent<DiseaseFX>();

        ColorUtility.TryParseHtmlString("#AC27008B", out failedCol);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateTimer();

        if (testCount >= 3)
        {
            EnableNewspaper("IDIOT SCIENTIST KILLS EVERYONE!");
            audsource.PlayOneShot(fail);
        }
    }

    private void UpdateTimer()
    {
        if (timer > 0)
            timer -= Time.deltaTime;

        else
        {
            EnableNewspaper("NO CURE FOR PLAGUE FOUND. END OF HUMANITY!");            
        }

        TimeSpan time = TimeSpan.FromSeconds(timer);
        timerText.text = string.Format("Time remaining till outbreak:\n {0}m:{1}s", time.Minutes, time.Seconds);

    }

    private void EnableNewspaper(string _text)
    {
        newspaper.SetActive(true);
        newspaperText.text = _text;
        targetBlocker.SetActive(true);
    }

    private void ChangeWaterColor()
    {
        water.GetComponent<MeshRenderer>().material.color = failedCol;
    }

    public void TestTransmission(int _buttonNum)
    {
        if (!m_TransmissionSolved)
        {
            if (_buttonNum == diseaseHolder.currDisease.myTransmission.myPair.button)
            {
                Debug.LogFormat("Correct Transmission vector: {0}", diseaseHolder.currDisease.myTransmission.transmissionType);
                m_TransmissionSolved = true;
                //press down all buttons
                //foreach(GameObject go in buttons)
                //{
                //    go.GetComponent<buttonPress>().DisableButt();
                //}

                for (int index = 0; index < buttons.Count - 2; ++index)
                {
                    buttons[index].GetComponent<buttonPress>().DisableButt();
                }
                buttons[_buttonNum - 1].GetComponent<MeshRenderer>().material.color = Color.yellow;
            }

            else
            {
                ++testCount;
                //do bad thing
                Debug.LogFormat("INCORRECT Transmission vector: {0}", diseaseHolder.currDisease.myTransmission.transmissionType);
                StartCoroutine(fx.Shake(200, 0.01f, 0.3f));
                buttons[_buttonNum - 1].GetComponent<buttonPress>().StartPress(0);
                ChangeWaterColor();
            }
        }
        CheckAnswer();
    }

    public void TestStrain(int _buttonNum)
    {
        if (!m_StrainSolved)
        {
            if (_buttonNum == diseaseHolder.currDisease.myStrain.button)
            {
                m_StrainSolved = true;
                Debug.LogFormat("Correct Strain Value: {0}", diseaseHolder.currDisease.myStrain.strainType);
                for (int index = 4; index < buttons.Count; ++index)
                {
                    buttons[index].GetComponent<buttonPress>().DisableButt();
                }

                buttons[_buttonNum - 1].GetComponent<MeshRenderer>().material.color = Color.yellow;
            }

            else
            {
                //bad thing
                ++testCount;

                Debug.LogFormat("INCORRECT Strain Value: {0}", diseaseHolder.currDisease.myStrain.strainType);
                StartCoroutine(fx.Shake(200, 0.01f, 0.3f));
                buttons[_buttonNum - 1].GetComponent<buttonPress>().StartPress(0);
                ChangeWaterColor();
            }
        }
        CheckAnswer();
    }

    private void CheckAnswer()
    {
        if (m_StrainSolved && m_TransmissionSolved)
        {
            //EnableNewspaper("CURE FOUND! SCIENTIST GIVEN NOBEL PRIZE");
            //move othercam down to show vial
            //new buttons
            //players select what they literally just pressed
            otherCam.gameObject.transform.position = otherCamTarget.position;
            disableCanv.SetActive(false);
        }
    }

    public void WinGame()
    {
        EnableNewspaper("CURE FOUND! SCIENTIST GIVEN NOBEL PRIZE");
        audsource.PlayOneShot(success);
    }
}
