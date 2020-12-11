using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    float timer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Police"))
        {
            Destroy(collision.gameObject);
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 0.2f)
        {
            Destroy(gameObject);
        }
    }
}
