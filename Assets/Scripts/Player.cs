using UnityEngine;
using System.Collections;

class Player : MonoBehaviour
{
    public Camera TheCameraIAmResponsibleFor;

    public PlayerState State;

    public Player()
    {
        State = new Magician();
    }

    void Start()
    {
        State.Start(transform);
    }

    void Update()
    {
           //transform.position = new Vector3(0.0f, Mathf.Sin(Time.realtimeSinceStartup), 0.0f);
        State.Update();
    }
}
