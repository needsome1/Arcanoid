using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float speed= 15f;

    private Rigidbody2D rb2D;
    private AudioSource _audioSource;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        rb2D.velocity = rb2D.velocity.normalized * speed;
    }

    public void Launch(Vector2 direction)
    {
        transform.parent = null;
        rb2D.simulated = true;
        rb2D.velocity = direction.normalized * speed;
    }

    public void Catch(Transform parent)
    {
        transform.parent = parent;
        rb2D.simulated = false;
        rb2D.velocity = Vector2.zero; 
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag=="Block")
        {
            _audioSource.Play();
        }
    }
}
