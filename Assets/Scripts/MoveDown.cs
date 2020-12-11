using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public static float GameSpeed;

    void Update()
    {
        transform.Translate(0, -1 * GameSpeed * Time.deltaTime, 0);
    }
}
