using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    public GameObject[] Tiles;
    public Transform SpawnPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnTile()
    {
        int roll = Random.Range(0, Tiles.Length);
        Instantiate(Tiles[roll], SpawnPoint.position, Quaternion.identity);
    }
}
