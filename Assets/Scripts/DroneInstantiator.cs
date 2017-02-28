using UnityEngine;
using System.Collections;



public class DroneInstantiator : MonoBehaviour
{

    [SerializeField]
    GameObject _prefab;

    [SerializeField]
    int _amount = 100;

    [SerializeField]
    int displacementX = 5;

    [SerializeField]
        int displacementZ = 5;


    [SerializeField]
    GameObject[] drones;


    // Use this for initialization
    void Start()
    {
        drones = new GameObject[_amount];

        for (int i = 0; i < _amount; i++)
        {
            GameObject drone = Instantiate(_prefab, transform.position + new Vector3(i * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;
            drone.GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(i / (float)_amount, .5f, .5f);
            drones[i] = drone;
        }

        //Destroy(this);
    }
}