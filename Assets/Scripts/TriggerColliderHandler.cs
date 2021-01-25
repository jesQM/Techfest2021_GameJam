using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerColliderHandler : MonoBehaviour
{
    public UnityEvent OnTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggered?.Invoke();
    }
}
