using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachedBottom : MonoBehaviour
{
    public bool isTile;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            if (isTile)
            {
                GameObject.FindGameObjectWithTag("GameController").SendMessage("SpawnTile");
            }
            Destroy(gameObject);
        }
    }
}
