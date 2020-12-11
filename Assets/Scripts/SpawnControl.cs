using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    private GameObject[] EasyTiles;
    private GameObject[] MediumTiles;
    private GameObject[] HardTiles;

    [Header("Stage1")]
    public GameObject[] EasyTiles1;
    public GameObject[] MediumTiles1;
    public GameObject[] HardTiles1;
    [Header("Stage2")]
    public GameObject[] EasyTiles2;
    public GameObject[] MediumTiles2;
    public GameObject[] HardTiles2;
    [Header("Stage3")]
    public GameObject[] EasyTiles3;
    public GameObject[] MediumTiles3;
    public GameObject[] HardTiles3;
    [Header("Stage4")]
    public GameObject[] EasyTiles4;
    public GameObject[] MediumTiles4;
    public GameObject[] HardTiles4;
    [Header("Stage5")]
    public GameObject[] EasyTiles5;
    public GameObject[] MediumTiles5;
    public GameObject[] HardTiles5;

    public GameObject Gap, PlainTile, buff;
    public GameObject lastTile;
    public float MovementSpeed;
    public float MaximumSpeed;
    public float Decelerator;
    int TileCount = 0;
    int Stage = 0;
    bool set1,set2,set3,set4,set5;
    void Start()
    {
        TileCount = 1;
        Stage = 1;
    }

    void Update()
    {
        if (MovementSpeed < MaximumSpeed)
        {
            MovementSpeed += Time.deltaTime / Decelerator;
            MoveDown.GameSpeed = MovementSpeed;
        }
        CheakStage();
    }
    void CheakStage()
    {
        if(Stage == 1 && !set1)
        {
            EasyTiles = EasyTiles1;
            MediumTiles = MediumTiles1;
            HardTiles = HardTiles1;
            set1 = true;
        }
        if (Stage == 2 && !set2)
        {
            EasyTiles = EasyTiles2;
            MediumTiles = MediumTiles2;
            HardTiles = HardTiles2;
            set2 = true;
        }
        if (Stage == 3 && !set3)
        {
            EasyTiles = EasyTiles3;
            MediumTiles = MediumTiles3;
            HardTiles = HardTiles3;
            set3 = true;
        }
        if (Stage == 4 && !set4)
        {
            EasyTiles = EasyTiles4;
            MediumTiles = MediumTiles4;
            HardTiles = HardTiles4;
            set4 = true;
        }
        if (Stage == 5 && !set5)
        {
            EasyTiles = EasyTiles5;
            MediumTiles = MediumTiles5;
            HardTiles = HardTiles5;
            set5 = true;
        }
    }
    public void SpawnTile()
    {
        if (TileCount < 4)
        {
            int roll = Random.Range(0, EasyTiles.Length);
            GameObject LastTile = Instantiate(EasyTiles[roll], lastTile.transform.GetChild(0).transform.position, Quaternion.identity);
            lastTile = LastTile;
        }
        if (TileCount == 4)
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
        if (TileCount == 7)
        {
            int roll = Random.Range(0, HardTiles.Length);
            GameObject LastTile = Instantiate(HardTiles[roll], lastTile.transform.GetChild(0).transform.position, Quaternion.identity);
            lastTile = LastTile;
        }
        if (TileCount > 7 && TileCount < 10)
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

        if (GapRoll < 2)
        {
            int buffRoll = Random.Range(0, 6);
            if (buffRoll <= 4)
            {
                GameObject LastTile = Instantiate(Gap, lastTile.transform.GetChild(0).transform.position, Quaternion.identity);
                lastTile = LastTile;
            }
            if(buffRoll == 5)
            {
                GameObject LastTile = Instantiate(buff, lastTile.transform.GetChild(0).transform.position, Quaternion.identity);
                lastTile = LastTile;
            }
        }
        if (TileCount == 10)
        {
            //Change gap to a iconic stage changer later
            GameObject LastTile = Instantiate(PlainTile, lastTile.transform.GetChild(0).transform.position, Quaternion.identity);
            lastTile = LastTile;
            Stage += 1;
            TileCount = 0;
        }

        TileCount += 1;
    }
}
