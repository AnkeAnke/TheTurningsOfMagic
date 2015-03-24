using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Magician : PlayerState
{
    //protected Int3 up;
    private bool moveable = true;
    private Int3 startPosition;
    private Vector3 startRight, startUp;

    public override void Init(Player player, GameCamera mainCamera)
    {
        base.Init(player, mainCamera);

        // Save initial player position.
        startPosition = player.GridPosition;
        startRight = player.transform.right;
        startUp = player.transform.up;
    }

    public void Respawn()
    {
        player.GridPosition = startPosition;
        player.transform.right = startRight;
        player.transform.up = startUp;
    }

    public override void Update()
    {
        if (!IsMoveable())
            return;

        // Falling.
        if(!World.Get().IsSolid(player.GridPosition - Int3.UnitY))
        {
            //moveable = false;
            player.GridPosition -= Int3.UnitY;

            if (player.GridPosition.y < World.Get().Size.y * 2)
            {
                Respawn();
            }
        }

        // Fallback moving via keyboard.
        Int3 nextPos;
      /*  if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
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
        }*/
        
        // Move (touch)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !mainCamera.CameraFollowingLastTouch)
        {
            // Ray cast for transform to 
            if (Input.GetTouch(0).position.x > Screen.width / 2)
            {
                nextPos = player.GridPosition + new Int3(Vector3.Cross(player.transform.up, mainCamera.DiscreteToPlayer.Vector));
                player.CurrentLookDirection = Player.LookDirection.RIGHT;
            }
            else
            {
                nextPos = player.GridPosition - new Int3(Vector3.Cross(player.transform.up, mainCamera.DiscreteToPlayer.Vector));
                player.CurrentLookDirection = Player.LookDirection.LEFT;
            }

            if (!World.Get().IsSolid(nextPos))
            {
                // TODO: move
                player.GridPosition = nextPos;
            }
        }
    }

    public bool IsMoveable()
    {
        return moveable;
    }
}