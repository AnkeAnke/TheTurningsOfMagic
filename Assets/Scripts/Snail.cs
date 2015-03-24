using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Snail : PlayerState
{
    const float MOVE_TOLERANCE = 0.25f;
    const float TURN_TOLERANCE = 0.4f;

    public override void Init(Player player, GameCamera mainCamera)
    {
        base.Init(player, mainCamera);
        StartCoroutine(MoveDecision());
    }

    private IEnumerator MoveDecision()
    {
        while(this.isActiveAndEnabled)
        {
            float orientation = Vector3.Dot(player.transform.up, mainCamera.DeviceUpInWorld);

            orientation = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began ? 0.5f : 1.0f;

            if (orientation > 0.0f && orientation < 1.0f - MOVE_TOLERANCE)
            {
                Int3 nextPos = player.GridPosition + new Int3(player.transform.forward);

                // Try to move forward
                if(World.Get().IsSolid(nextPos))
                {
                    // not upside down?
                  /*  if (Vector3.Dot(-player.transform.right, mainCamera.DeviceUpInWorld) > MOVE_TOLERANCE)
                    {
                        // Step up!

                        // TODO: Start run animation and wait for it.
                        //   player.transform.Rotate(mainCamera.DiscreteToPlayer.Vector, 90.0f * sign);
                        yield return new WaitForSeconds(1.0f);
                    }*/
                }
                else
                {
                    Int3 discreteUp = new Int3(player.transform.up);

                    // Nothing below?
                    if (!World.Get().IsSolid(nextPos - discreteUp))
                    {
                        // not upside down?
                       // if (Vector3.Dot(player.transform.right, mainCamera.DeviceUpInWorld) > TURN_TOLERANCE)
                        {
                            // Try to step down
                            // TODO: Start run animation and wait for it.
                            player.transform.rotation = Quaternion.LookRotation(-player.transform.up, player.transform.forward);
                            player.transform.position = (Vector3)(nextPos - discreteUp);
                            yield return new WaitForSeconds(1.0f);
                        }
                    }
                    else
                    {
                        // TODO: Start run animation and wait for it.
                        player.GridPosition = nextPos;
                        yield return new WaitForSeconds(1.0f);
                    }
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public override void Update()
    {
    }
}