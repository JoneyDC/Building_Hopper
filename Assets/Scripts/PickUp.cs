using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool Flamming,Wings,Bubble;
    private PlayerMove player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CoinCollider"))
        {
            if (Flamming)
            {
                if (player.flamming)
                {
                    player.flammingTimer = 0;
                }
                else
                {
                    player.flamming = true;
                }
                if (player.FlammingCoroutine != null)
                {
                    player.SwitchFireOff();
                    player.flameC = false;
                }
            }
            if (Wings)
            {
                if (player.wings)
                {
                    player.wingsTimer = 0;
                }
                else
                {
                    player.wings = true;
                }
                if (player.WingsCoroutine != null)
                {
                    player.SwitchWingsOff();
                    player.wingC = false;
                }
            }
            if (Bubble)
            {
                if (player.bubble)
                {
                    player.bubbleTimer = 0;
                }
                else
                {
                    player.bubble = true;
                }
                if (player.BubbleCoroutine != null)
                {
                    player.SwitchBubbleOff();
                    player.bubbleC = false;
                }
            }
            Destroy(gameObject);
        }
    }
}
