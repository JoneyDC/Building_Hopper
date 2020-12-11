using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleOrbs : MonoBehaviour
{
    [HideInInspector] public int Exp;

    private void Awake()
    {
        Exp = 100;

        if(Effects.doubleOrbs)
        {
            int roll = Random.Range(0, 10);
            if(roll == 9)
            {
                GetComponent<SpriteRenderer>().color = Color.magenta;
                Exp = 200;
            }
        }
    }


}
