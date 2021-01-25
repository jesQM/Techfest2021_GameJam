using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float direction;
    public float MoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * direction * MoveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag.Equals("Player")) return;
        if (go.tag.Equals("Finish")) return;

        if (go.tag.Equals("Enemy"))
        {
            go.GetComponent<EnemyMovement>().Kill();
        }

        Destroy(this.gameObject);
    }
}
