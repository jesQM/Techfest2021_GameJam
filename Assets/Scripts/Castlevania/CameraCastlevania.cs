using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCastlevania : MonoBehaviour
{

    public float Y_Position;

    private GameObject _follow;

    void Start()
    {
        _follow = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3( _follow.transform.position.x, Y_Position, -10 );
    }
}
