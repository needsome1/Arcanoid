using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBounc : MonoBehaviour
{
    private BoxCollider2D bc2D;

    private void Awake()
    {
        bc2D = GetComponent<BoxCollider2D>();
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag=="Ball")
        {
            float relativePosition = RelativePosition(col.transform);
            col.rigidbody.velocity = new Vector2(relativePosition,1).normalized * col.rigidbody.velocity.magnitude;
        }
    }

    public float RelativePosition(Transform col)
    {
        return (col.transform.position.x - transform.position.x) / bc2D.bounds.size.x;
    }
}
