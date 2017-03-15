using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FountainRSControllerVR : MonoBehaviour {


    [SerializeField]
    int _amount = 5;

    [SerializeField]
    GameObject _fountainJet;

    [SerializeField]
    GameObject[] fountainJetsLeft;

    [SerializeField]
    GameObject[] fountainJetsRight;

    [SerializeField]
    int displacementX = -3;

    [SerializeField]
    int displacementZ = -1;

    void Awake()
    {
        
      //  source = GetComponent<AudioSource>();  
    }



    void Start()
    {
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
        }

        fountainJetsRight = new GameObject[_amount];

        for (int i = 0; i < _amount; i++)
        {
            // instantiate fountains based on prefab and then assign to fountain jet array
            GameObject jet = Instantiate(_fountainJet, transform.position + new Vector3(2.5f + -i * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;

            Transform jetTransform = jet.GetComponent<Transform>();
            if (i > 2)
            {
                jetTransform.rotation = Quaternion.Euler(0, 0, -(i-2) * 5);
            }

            fountainJetsRight[i] = jet;
        }
    }


    public void fountainHeightLeft(int i, float fingerBendDataLeft)
    {
        //Debug.Log("finger" + i + "bend value" + fingerBendDataLeft);
        fountainJetsLeft[i].transform.localScale = new Vector3 (100,40 + 5000 *fingerBendDataLeft,100);

      
    }

    public void fountainHeightRight(int i, float fingerBendDataRight)
    {
       // Debug.Log("finger" + i + "bend value" + fingerBendDataRight);
        fountainJetsRight[i].transform.localScale = new Vector3(100, 40 + 5000 * fingerBendDataRight, 100);

    }


}
