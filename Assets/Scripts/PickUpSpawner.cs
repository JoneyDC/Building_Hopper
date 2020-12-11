using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    public GameObject Wings, Bubble, Flamming;
    float roll;

    private void Awake()
    {
        roll = Random.Range(0, 3);
        if(roll == 0)
        {
            Instantiate(Wings, transform.position, transform.rotation);
        }
        if (roll == 1)
        {
            Instantiate(Bubble, transform.position, transform.rotation);
        }
        if (roll == 2)
        {
            Instantiate(Flamming, transform.position, transform.rotation);
        }
    }
}
