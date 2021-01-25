using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpKilled : MonoBehaviour
{
    public void Kill()
    {
        GetComponent<EnemyMovement>().Kill();
    }
}