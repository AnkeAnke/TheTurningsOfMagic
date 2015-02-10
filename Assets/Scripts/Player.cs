using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private float persitent;
    public Camera TheCameraIAmResponsibleFor;

    void Start()
    {

    }

    void Update()
    {
           transform.position = new Vector3(0.0f, Mathf.Sin(Time.realtimeSinceStartup), 0.0f);
    }
}
