﻿using UnityEngine;
using System.Collections;

class Player : MonoBehaviour
{
    private float persitent;
    public Camera TheCameraIAmResponsibleFor;

    public PlayerState State;

    public Player()
    {
        State = new Magician();
    }

    void Start()
    {
        State.Start();
    }

    void Update()
    {
           transform.position = new Vector3(0.0f, Mathf.Sin(Time.realtimeSinceStartup), 0.0f);
        State.Update();
    }
}
