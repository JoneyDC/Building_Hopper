using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetExperience : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            GameObject.FindGameObjectWithTag("UI").GetComponent<ScoreSystem>().exp += collision.GetComponent<DoubleOrbs>().Exp;
            Destroy(collision.gameObject);
        }
    }
}
