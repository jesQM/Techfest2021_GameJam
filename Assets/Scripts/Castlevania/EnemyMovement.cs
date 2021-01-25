using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public LayerMask GroundLayer;
    public float xSpeed;

    void Update()
    {
        ChangeDirection();
        transform.Translate(Vector3.right* xSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PlayerCastlevania p = collision.gameObject.GetComponent<PlayerCastlevania>();
            collision.gameObject.GetComponent<Rigidbody2D>().velocity += collision.contacts[0].normal * -10;
            p.Hurt();
        }
    }

    private void ChangeDirection()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.right;
        float distance = 1f;

        if (xSpeed > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, GroundLayer);
            if (hit.collider != null)
            {
                xSpeed *= -1;
            }
        } else {
            RaycastHit2D hit2 = Physics2D.Raycast(position, -direction, distance, GroundLayer);
            if (hit2.collider != null)
            {
                xSpeed *= -1;
            }
        }
    }

    public void Kill()
    {
        Destroy(this.gameObject);
    }
}
