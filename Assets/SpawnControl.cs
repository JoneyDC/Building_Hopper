using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    [Header("Stage1")]
    public GameObject[] EasyTiles;
    public GameObject[] MediumTiles;
    public GameObject[] HardTiles;
    public GameObject Gap;
    public GameObject lastTile;
    public float MovementSpeed;
    public float Decelerator;
    public int TileCount = 0;
    public int MaxTilesInScene;
    int Stage = 0;
    void Start()
    {
        TileCount = 1;
        Stage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        MovementSpeed += Time.deltaTime / Decelerator;
        MoveDown.GameSpeed = MovementSpeed / 100;
    }
    public void SpawnTile()
    {
        if (Stage == 1)
        {
            if (TileCount < 4)
            {
                int roll = Random.Range(0, EasyTiles.Length);
                GameObject LastTile = Instantiate(EasyTiles[roll], lastTile.transform.GetChild(0).transform.position, Quaternion.identity);
                lastTile = LastTile;
            }
            if(TileCount == 4)
            {
                int roll = Random.Range(0, MediumTiles.Length);
                GameObject LastTile = Instantiate(MediumTiles[roll], lastTile.transform.GetChild(0).transform.position, Quaternion.identity);
                lastTile = LastTile;
            }
            if (TileCount > 4 && TileCount < 7)
            {
                int TileRoll = Random.Range(1, 4);
                if (TileRoll == 1)
                {
                    int roll = Random.Range(0, EasyTiles.Length);
                    GameObject LastTile = Instantiate(EasyTiles[roll], lastTile.transform.GetChild(0).transform.position, Quaternion.identity);
                    lastTile = LastTile;
                }
                if (TileRoll > 1)
                {
                    int roll = Random.Range(0, MediumTiles.Length);
                    GameObject LastTile = Instantiate(MediumTiles[roll], lastTile.transform.GetChild(0).transform.position, Quaternion.identity);
                    lastTile = LastTile;
                }
            }
            if(TileCount == 7)
            {
                int roll = Random.Range(0, HardTiles.Length);
                GameObject LastTile = Instantiate(HardTiles[roll], lastTile.transform.GetChild(0).transform.position, Quaternion.identity);
                lastTile = LastTile;
            }
            if (TileCount > 7)
            {
                int TileRoll = Random.Range(1, 3);
                if (TileRoll == 1)
                {
                    int roll = Random.Range(0, MediumTiles.Length);
                    GameObject LastTile = Instantiate(MediumTiles[roll], lastTile.transform.GetChild(0).transform.position, Quaternion.identity);
                    lastTile = LastTile;
                }
                if (TileRoll == 2)
                {
                    int roll = Random.Range(0, HardTiles.Length);
                    GameObject LastTile = Instantiate(HardTiles[roll], lastTile.transform.GetChild(0).transform.position, Quaternion.identity);
                    lastTile = LastTile;
                }
            }
            int GapRoll = Random.Range(0, 3);

            if(GapRoll < 2)
            {
                GameObject LastTile = Instantiate(Gap, lastTile.transform.GetChild(0).transform.position, Quaternion.identity);
                lastTile = LastTile;
            }
        }
        TileCount += 1;
    }
}
