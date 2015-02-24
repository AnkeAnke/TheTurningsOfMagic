using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


abstract class PlayerState
{
    /// <summary>
    /// The position the player is coming from.
    /// </summary>
    protected Int3 Position;
    /// <summary>
    /// Reference to the holding players transformation.
    /// </summary>
    protected Transform PlayerTransform;
    public abstract void Start(Transform playerTransform);
    public abstract void Update();

    //public virtual Vector3 GetPosition()
    //{
    //    return (Vector3)Position;
    //}
}

class Magician : PlayerState
{
    protected Int3 up;
    protected bool moveable = true;
    protected Vector3 startPosition, startRight, startUp;
    public override void Start(Transform playerTransform)
    {
        Position = new Int3(playerTransform.position);
        this.PlayerTransform = playerTransform;

        // Save initial player position.
        startPosition = (Vector3)Position;
        Debug.Log("Position: " + Position);
        Debug.Log("StartPosition: " + startPosition);
        startRight = playerTransform.right;
        startUp = playerTransform.up;
    }

    public void Respawn()
    {
        PlayerTransform.position = startPosition;
        PlayerTransform.right = startRight;
        PlayerTransform.up = startUp;
        Position = new Int3(startPosition);
        Debug.Log(Position);
    }

    public override void Update()
    {
        if (!IsMoveable())
            return;

        // Falling.
        if(!World.Get().IsSolid(Position - Int3.UnitY))
        {
            //moveable = false;
            Position -= Int3.UnitY;

            if (Position.y < World.Get().Size.y * 2)
            {
                Debug.Log("Died");
                Respawn();
            }
        }

        Int3 nextPos;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                nextPos = Position + Int3.UnitX;

            }
            else
            {
                nextPos = Position - Int3.UnitX;
            }

            if (!World.Get().IsSolid(nextPos))
            {
                // TODO: move
                Position = nextPos;
            }
            Debug.Log("Player position: " + Position);
        }
        
        // TODO: Move
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Input.GetTouch(0).position.x > Screen.width / 2)
            {
                nextPos = Position + Int3.UnitX;
            }
            else
            {
                nextPos = Position - Int3.UnitX;
            }
            
            if(!World.Get().IsSolid(nextPos))
            {
                // TODO: move
                Position = nextPos;
            }
        }

        PlayerTransform.position = (Vector3)Position;
    }

    public bool IsMoveable()
    {
        return moveable;
    }
}
