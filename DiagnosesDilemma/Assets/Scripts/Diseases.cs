using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Collections;

[Serializable]
public class DiseaseData
{
    public static DiseaseData Instance;
    //add all the prefabs for all these options
    public List<GameObject> diseaseBasePrefabs;
    
    public List<GameObject> transmissionAppendageOptions;
    public List<Color> transmissionColorOptions;
    public List<int> transmissionButton;

    public DiseaseData()
    {
        if (Instance == null)
            Instance = this;
        else
            return;
    }
}

[Serializable]
public class Disease
{
    public enum DiseaseBaseType
    {
        e_Bacteria,
        e_Virus,
        e_Fungus,
        e_Parasite,

        e_DEFAULT
    }

    public DiseaseBaseType myDisease { get; private set; }
    public Transmission myTransmission = new Transmission();
    public Strain myStrain = new Strain();
    public GameObject diseasePrefab;

    public void ChooseDisease()
    {
        int enumSize = 4;//hardcoded enum size
        myDisease = (DiseaseBaseType)UnityEngine.Random.Range(0, enumSize);
        diseasePrefab = DiseaseData.Instance.diseaseBasePrefabs[(int)myDisease];
        myTransmission.ChooseTransmission((int)myDisease);
        myStrain.ChooseStrain();

        Debug.LogFormat("Disease is {0}. Transmission method is {1}. Strain of disease is {2}.\nCorrect Button for Transmission identifying is {3}. Correct Button for Strain Identifying is {4}.", myDisease, myTransmission.transmissionType, myStrain.strainType, myTransmission.myPair.button, myStrain.strainType == Strain.StrainVariants.e_Common ? 5 : 6);
    }
}

public class Transmission
{
    public enum TransmissionTypes
    {
        e_Bird,
        e_Rodent,
        e_Insect,
        e_Livestock,
        e_Airborne,
        e_Waterborne,

        e_DEFAULT
    }

    public TransmissionTypes transmissionType { get; private set; }
    public TransmissionPair myPair;
    //choose transmission
    //based on transmission, choose pair
    //a pair is a colour and appendage
    //two pairs possible
    //each pair has a button

    public void ChooseTransmission(int _diseaseType)
    {
        int enumSize = 6;//hardcoded enum size
        transmissionType = (TransmissionTypes)UnityEngine.Random.Range(0, enumSize);
        GetPairData(_diseaseType);
    }

    public void GetPairData(int _diseaseType)
    {
        bool rand = HelperClass.RandomBool();
        int offset = _diseaseType * 6;//offset means it gets the correct appendage
        switch (transmissionType)
        {
            case TransmissionTypes.e_Bird:
                //TODO: allocate the list data and hardcode it in
                myPair.appendagePrefab = DiseaseData.Instance.transmissionAppendageOptions[offset + (rand ? 4 : 2)];
                myPair.transmissionColor = DiseaseData.Instance.transmissionColorOptions[rand ? 0 : 1];
                myPair.button = DiseaseData.Instance.transmissionButton[rand ? 0 : 2];
                break;
            case TransmissionTypes.e_Rodent:
                myPair.appendagePrefab = DiseaseData.Instance.transmissionAppendageOptions[offset + (rand ? 0 : 1)];
                myPair.transmissionColor = DiseaseData.Instance.transmissionColorOptions[rand ? 2 : 3];
                myPair.button = DiseaseData.Instance.transmissionButton[rand ? 0 : 3];
                break;
            case TransmissionTypes.e_Insect:
                myPair.appendagePrefab = DiseaseData.Instance.transmissionAppendageOptions[offset + (rand ? 1 : 5)];
                myPair.transmissionColor = DiseaseData.Instance.transmissionColorOptions[3];
                myPair.button = DiseaseData.Instance.transmissionButton[rand ? 1 : 2];
                break;
            case TransmissionTypes.e_Livestock:
                myPair.appendagePrefab = DiseaseData.Instance.transmissionAppendageOptions[offset + (rand ? 5 : 2)];
                myPair.transmissionColor = DiseaseData.Instance.transmissionColorOptions[2];
                myPair.button = DiseaseData.Instance.transmissionButton[rand ? 1 : 3];
                break;
            case TransmissionTypes.e_Airborne:
                myPair.appendagePrefab = DiseaseData.Instance.transmissionAppendageOptions[offset + (rand ? 0 : 5)];
                myPair.transmissionColor = DiseaseData.Instance.transmissionColorOptions[rand ? 2 : 1];
                myPair.button = DiseaseData.Instance.transmissionButton[rand ? 2 : 0];
                break;
            case TransmissionTypes.e_Waterborne:
                myPair.appendagePrefab = DiseaseData.Instance.transmissionAppendageOptions[offset + (rand ? 4 : 5)];
                myPair.transmissionColor = DiseaseData.Instance.transmissionColorOptions[rand ? 0 : 3];
                myPair.button = DiseaseData.Instance.transmissionButton[rand ? 1 : 3];
                break;

            case TransmissionTypes.e_DEFAULT:
                Debug.LogWarning("Haven't set transmission type");
                break;
            default:
                Debug.LogWarning("Haven't set transmission type");
                break;
        }
    }
}

public struct TransmissionPair
{
    public Color transmissionColor;
    public GameObject appendagePrefab;
    public int button;
}

public class Strain
{
    public enum StrainVariants
    {
        e_Common,
        e_Primordial,
        e_Mutated,

        e_DEFAULT
    }

    public StrainVariants strainType { get; private set; }
    public int button;

    public void ChooseStrain()
    {
        strainType = HelperClass.RandomBool() ? StrainVariants.e_Common : StrainVariants.e_Primordial;
        button = strainType == StrainVariants.e_Common ? 5 : 6;
    }

    public void MutateStrain()
    {
        strainType = StrainVariants.e_Mutated;
    }
}

public class Diseases : MonoBehaviour
{
    public DiseaseData data = new DiseaseData();
    [HideInInspector]
    public Disease currDisease = new Disease();

    [SerializeField]
    private GameObject m_DiseaseInstance;
    private GameObject m_transmissionAppendage;
    // Use this for initialization
    private void Start()
    {
        CreateDisease();
    }

    private void ChangeColour()
    {
        Color col = currDisease.myTransmission.myPair.transmissionColor;
        m_DiseaseInstance.GetComponent<MeshRenderer>().material.color = col;

        for (int index = 0; index < m_transmissionAppendage.transform.childCount; ++index)
        {
            MeshRenderer meshrend = m_transmissionAppendage.transform.GetChild(index).GetComponent<MeshRenderer>();
            meshrend.material.color = col;
        }
    }

    private void CreateDisease()
    {
        if (m_DiseaseInstance != null)
        {
            Destroy(m_DiseaseInstance);
        }

        currDisease.ChooseDisease();
        m_DiseaseInstance = Instantiate(currDisease.diseasePrefab, Vector3.zero, Quaternion.identity);
        m_transmissionAppendage = Instantiate(currDisease.myTransmission.myPair.appendagePrefab, m_DiseaseInstance.transform.GetChild(0).transform);

        ChangeColour();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //    CreateDisease();
    }
}
