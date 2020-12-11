using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticShield : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            collision.gameObject.transform.position = Vector3.MoveTowards(collision.gameObject.transform.position, transform.parent.position, Effects.PullStrength * Time.deltaTime);
        }
    }
  
}
