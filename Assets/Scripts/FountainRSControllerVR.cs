using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FountainRSControllerVR : MonoBehaviour
{


    [SerializeField]
    int _amount = 5;

    [SerializeField]
    GameObject _fountainJet;

    [SerializeField]
    GameObject _fountainParticles;

    [SerializeField]
    GameObject[] fountainJetParticlesLeft;

    [SerializeField]
    GameObject[] fountainJetParticlesRight;

    [SerializeField]
    GameObject[] fountainJetsLeft;

    [SerializeField]
    GameObject[] fountainJetsRight;

    [SerializeField]
    int displacementX = -3;

    [SerializeField]
    int displacementZ = -1;

    public ZoneControllerSingleCamera zoneController;
    public ParticleSystem mainParticleSystem;

    int[] fountainJetHeightLeft = { 2000, 2500, 2000, 2000, 1000 };
    GameObject fountainChild;
    GameObject jetParticles;

    void Awake()
    {

        //  source = GetComponent<AudioSource>();  
    }


    void Start()
    {
        //_fountainParticles.transform.GetChild(0).gameObject.SetActive(true);
        fountainChild = _fountainParticles.transform.GetChild(0).gameObject;
        fountainJetsLeft = new GameObject[_amount];

        for (int i = 0; i < _amount; i++)
        {
            // instantiate fountains based on prefab and then assign to fountain jet array
            GameObject jet = Instantiate(_fountainJet, transform.position + new Vector3(-2.5f + i * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;

            Transform jetTransform = jet.GetComponent<Transform>();
            if (i > 2)
            {
                jetTransform.rotation = Quaternion.Euler(0, 0, (i - 2) * 5);
            }
            fountainJetsLeft[i] = jet;

            //   jetParticles = Instantiate(_fountainParticles, transform.position + new Vector3(-2.5f + i * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;
            //  jetParticles.transform.GetChild(0).gameObject.SetActive(true);

            // fountainJetParticlesLeft[i] = jetParticles;

        }


        fountainJetParticlesLeft = new GameObject[_amount];

        for (int i = 0; i < _amount; i++)
        {


            jetParticles = Instantiate(_fountainParticles, transform.position + new Vector3(-2.5f + i * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;
            jetParticles.transform.GetChild(0).gameObject.SetActive(true);

            fountainJetParticlesLeft[i] = jetParticles;

        }



        fountainJetsRight = new GameObject[_amount]; // initialize jet array

        for (int i = 0; i < _amount; i++)
        {
            // instantiate fountains based on prefab and then assign to fountain jet array
            GameObject jet = Instantiate(_fountainJet, transform.position + new Vector3(2.5f  -i * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;

            Transform jetTransform = jet.GetComponent<Transform>();
            if (i > 2)
            {
                jetTransform.rotation = Quaternion.Euler(0, 0, -(i - 2) * 5);
            }

            fountainJetsRight[i] = jet;
        }


        fountainJetParticlesRight = new GameObject[_amount];

        for (int i = 0; i < _amount; i++)
        {


            jetParticles = Instantiate(_fountainParticles, transform.position + new Vector3(2.5f - i * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;
            jetParticles.transform.GetChild(0).gameObject.SetActive(true);

            fountainJetParticlesRight[i] = jetParticles;

        }
    }


    public void fountainHeightLeft(int i, float fingerBendDataLeft)
    {
        if (zoneController.isWaterGloveLeft)
        {
            //Debug.Log("finger" + i + "bend value" + fingerBendDataLeft);
            fountainJetsLeft[i].transform.localScale = new Vector3(100, 40 + 5000 * fingerBendDataLeft, 100);
        }
        else
        {
            Debug.Log("fountain off");
        }
    }

    public void fountainHeightRight(int i, float fingerBendDataRight)
    {
        if (zoneController.isWaterGloveRight)
        {
            // Debug.Log("finger" + i + "bend value" + fingerBendDataRight);
            fountainJetsRight[i].transform.localScale = new Vector3(100, 40 + 5000 * fingerBendDataRight, 100);
        }
        else
        {
            Debug.Log("fountain off");
        }

    }

    public void fountainMidiLeft(int i)
    {
        if (zoneController.isWaterGloveLeft)
        {
            Debug.Log("finger" + i);

            fountainJetParticlesLeft[i].transform.GetChild(0).gameObject.SetActive(true); // single test jet
            if (fountainJetParticlesLeft[i].GetComponentInChildren<ParticleSystem>().IsAlive() == false)
            {
                fountainJetParticlesLeft[i].transform.GetChild(0).gameObject.SetActive(false); ;
            }
            // fountainJetsLeft[i].transform.localScale = new Vector3(100, fountainJetHeightLeft[i], 100); // setting the old fountain jets by finger

            // fountainJetsLeft[i].transform.localScale = new Vector3(100, 40 + 5000, 100);
            /* {

                 fountainChild.SetActive(true);
             }
             if (fountainChild.GetComponentInChildren<ParticleSystem>().IsAlive() == false)
             {
                 fountainChild.SetActive(false);
             }*/
        }
        else
        {
            Debug.Log("fountain off");
        }
    }

    public void fountainMidiRight(int i)
    {
        if (zoneController.isWaterGloveRight)
        {
            Debug.Log("finger" + i);

            fountainJetParticlesRight[i].transform.GetChild(0).gameObject.SetActive(true); // single test jet
            if (fountainJetParticlesRight[i].GetComponentInChildren<ParticleSystem>().IsAlive() == false)
            {
                fountainJetParticlesRight[i].transform.GetChild(0).gameObject.SetActive(false); ;
            }
            // fountainJetsLeft[i].transform.localScale = new Vector3(100, fountainJetHeightLeft[i], 100); // setting the old fountain jets by finger

            // fountainJetsLeft[i].transform.localScale = new Vector3(100, 40 + 5000, 100);
            /* {

                 fountainChild.SetActive(true);
             }
             if (fountainChild.GetComponentInChildren<ParticleSystem>().IsAlive() == false)
             {
                 fountainChild.SetActive(false);
             }*/
        }
        else
        {
            Debug.Log("fountain off");
        }
    }

    void Update()
    {
        /*
        for (int i = 0; i < _amount; i++)
        {

        
            if (Input.GetKeyUp(KeyCode.Space))
        
            
            {
                fountainJetParticlesLeft[i].SetActive(true);
            }
            if (fountainJetParticlesLeft[i].GetComponentInChildren<ParticleSystem>().IsAlive() == false)
                fountainJetParticlesLeft[i].SetActive((false));
        }
        */


        // check why 2 presses to make jets happen
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            for (int i = 0; i < _amount; i++)

            {
                jetParticles.transform.GetChild(0).gameObject.SetActive(true);
                //fountainChild.SetActive(true);
                fountainJetParticlesLeft[i].transform.GetChild(0).gameObject.SetActive(true);
                if (fountainJetParticlesLeft[i].GetComponentInChildren<ParticleSystem>().IsAlive() == false)
                {
                    fountainJetParticlesLeft[i].transform.GetChild(0).gameObject.SetActive(false); ;
                }
            }
        }
        /* single jet tester
        if (fountainChild.GetComponentInChildren<ParticleSystem>().IsAlive() == false)
        {
            fountainChild.SetActive(false);
        }
        */


        //   _fountainParticles.SetActive((false));
    }
}

