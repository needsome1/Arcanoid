using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private int scoreValue = 10;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag=="Ball")
        {
            health--;
            if (health <=0)
            {
                FindObjectOfType<GameSession>().IncreaseScore(scoreValue);
                Destroy(gameObject);
            }
        }
    }
    
}
