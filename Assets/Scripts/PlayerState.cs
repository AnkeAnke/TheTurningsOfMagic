using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


abstract class PlayerState : MonoBehaviour
{
    /// <summary>
    /// Reference to the player this player state belongs to.
    /// </summary>
    protected Player player;

    /// <summary>
    /// Main camera script.
    /// </summary>
    protected GameCamera mainCamera;

    public virtual void Init(Player player, GameCamera mainCamera)
    {
        this.player = player;
        this.mainCamera = mainCamera;

        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public abstract void Update();

    //public virtual Vector3 GetPosition()
    //{
    //    return (Vector3)Position;
    //}
}