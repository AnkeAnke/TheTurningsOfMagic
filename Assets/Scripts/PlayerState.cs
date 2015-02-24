using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


abstract class PlayerState : MonoBehaviour
{
    /// <summary>
    /// The position the player is coming from.
    /// </summary>
    protected Int3 Position;
    /// <summary>
    /// Reference to the holding players transformation.
    /// </summary>
    protected Transform PlayerTransform;
    /// <summary>
    /// Main camera script.
    /// </summary>
    protected GameCamera MainCamera;

    public virtual void Init(Transform playerTransform, GameCamera mainCamera)
    {
        Position = new Int3(playerTransform.position);
        this.PlayerTransform = playerTransform;
        this.MainCamera = mainCamera;
    }

    public abstract void Update();

    //public virtual Vector3 GetPosition()
    //{
    //    return (Vector3)Position;
    //}
}