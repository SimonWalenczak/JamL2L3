using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.name == "Player")
        {
            GameData._currentXp += 2;
            Destroy(gameObject);
        }
    }
}
