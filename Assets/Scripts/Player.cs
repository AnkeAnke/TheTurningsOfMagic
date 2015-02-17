using UnityEngine;
using System.Collections;

class Player : MonoBehaviour
{
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
        State.Update();
    }
}
