using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public static float GameSpeed;
    void Start()
    {
        GameSpeed = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -1 * GameSpeed, 0);
    }
}
