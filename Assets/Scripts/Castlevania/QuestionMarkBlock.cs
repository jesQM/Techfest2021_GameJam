using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMarkBlock : MonoBehaviour
{
    public GameObject SpawnObject;
    public GameObject WhereToSpawn;
    public GameObject WhereToMove;
    public GameObject CreatedObjectParent;

    private bool _hasBeenTriggered = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (collision.contacts[0].normal.y > 0) TriggerBox();
        }
    }

    private void TriggerBox()
    {
        if (!_hasBeenTriggered)
        {
            _hasBeenTriggered = !_hasBeenTriggered;
            StartCoroutine(MoveObjectToPosition(
                Instantiate(SpawnObject, WhereToSpawn.transform.position, Quaternion.identity),
                WhereToMove.transform,
                0.5f)
            );
        }
    }

    IEnumerator MoveObjectToPosition(GameObject o, Transform position, float movespeed)
    {
        o.transform.parent = CreatedObjectParent.transform;

        Vector2 direction = (position.position - o.transform.position).normalized;
        while (true)
        {
            Vector2 newPos = direction * movespeed * Time.deltaTime;
            o.transform.Translate(newPos);
            float distance = Vector2.Distance(o.transform.position, position.position);
            
            if (distance < 0.1) {
                break;
            }
            yield return null;
        }

        o.GetComponent<BoxCollider2D>().enabled = true;
    }
}
