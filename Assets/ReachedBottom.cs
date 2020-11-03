using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachedBottom : MonoBehaviour
{
    public bool isTile;
    bool Done;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            if (isTile && !Done)
            {
                GameObject.FindGameObjectWithTag("GameController").SendMessage("SpawnTile");
                Done = true;
            }
            Destroy(gameObject);
        }
    }
}
