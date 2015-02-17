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
    public abstract void Start();
    public abstract void Update();
}

class Magician : PlayerState
{
    public override void Start()
    {
        // TODO: snap position
    }

    public override void Update()
    {
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
        }
        
        // TODO: Move
        if (Input.GetTouch(0).phase == TouchPhase.Began)
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
            }
        }
    }

    public bool IsMoveable()
    {
        return true;
        // TODO
    }
}
