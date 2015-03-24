using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GameCamera : MonoBehaviour
{
    public Player TrackedPlayer = null;
    public float LOOK_HEIGHT = 2.0f;
    public float LOOK_DISTANCE = 2.0f;

    private const float TAP_MOVE_DEADZONE = 0.1f;
    private const float ROTATION_SNAP_SPEED = 10.0f;
    private const float ROTATION_MOVE_SPEED = 70.0f;

    public Direction DiscreteToPlayer { get; private set; }
    private Vector3 continousToPlayer;

    public Vector3 DeviceUpInWorld { get; private set; }
    public Vector3 DeviceUp { get; private set; }

    /// <summary>
    /// The camera does actually never change its general up direction. It is fixed at startup using the player's orientation
    /// </summary>
    private Vector3 generalWorldUp;

    /// <summary>
    /// Last touch position in pixel coordinates. Only non-negative if there is an ongoing touch.
    /// </summary>
    private Vector2 lastTouchPosition = -Vector2.one;


    /// <summary>
    /// True if the camera followed the camera since the last touch begin event.
    /// </summary>
    public bool CameraFollowingLastTouch { get { return cameraFollowingLastTouch; } }
    bool cameraFollowingCurrentTouch = false;
    bool cameraFollowingLastTouch = false;

    /// <summary>
    /// Scale factor to account for screen size and pixel scaling.
    /// </summary>
    private float screenScale;


    private void Start()
    {
        if (Screen.dpi > 0.0f)
            screenScale = Screen.dpi;
        else
        {
            screenScale = Math.Min(Screen.width, Screen.height) / 3; // probably bad heuristic
            Debug.Log("No Screen.dpi value available, using " + screenScale + " for scaling instead!");
        }

        DiscreteToPlayer = Direction.GetDirection(TrackedPlayer.transform.right);
        continousToPlayer = DiscreteToPlayer.Vector;

        generalWorldUp = TrackedPlayer.transform.up;
    }

    private void Update()
    {
        // Touch events.
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                lastTouchPosition = Input.touches[0].position;
                cameraFollowingLastTouch = false;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                lastTouchPosition = -Vector2.one;
                cameraFollowingCurrentTouch = false;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && !cameraFollowingCurrentTouch)
            {
                cameraFollowingCurrentTouch = Vector2.Distance(lastTouchPosition, Input.GetTouch(0).position) / screenScale > TAP_MOVE_DEADZONE;
                cameraFollowingLastTouch = cameraFollowingCurrentTouch;
            }
        }

        // Follow touch or snap to orientation.
        if (cameraFollowingCurrentTouch)
        {
            //float x = Vector3.Dot(TrackedPlayer.transform.up, generalWorldUp);
            //float y = Vector3.Dot(TrackedPlayer.transform.up, Vector3.Cross(continousToPlayer, generalWorldUp));

            float rotationAmount = (Input.GetTouch(0).position.x - lastTouchPosition.x) / screenScale * ROTATION_MOVE_SPEED; // TODO: Make dependent on the orientation
            continousToPlayer = (Quaternion.AngleAxis(rotationAmount, TrackedPlayer.transform.up) * continousToPlayer).normalized;
            DiscreteToPlayer = Direction.GetDirection(continousToPlayer);
            lastTouchPosition = Input.GetTouch(0).position;
        }
        else
        {
            continousToPlayer = Vector3.Slerp(DiscreteToPlayer.Vector, continousToPlayer, (float)Math.Exp(-Time.deltaTime * ROTATION_SNAP_SPEED));
        }


        Vector3 upPositionOffsetDir = TrackedPlayer.transform.up;

        // Rotate in snail mode.
     /*   if (TrackedPlayer.IsSnail)
        {
            // TODO: Smoothing
            DeviceUp = new Vector3(-Input.acceleration.x, -Input.acceleration.y, 0.0f);
            DeviceUp.Normalize();
            DeviceUpInWorld = Quaternion.LookRotation(continousToPlayer, TrackedPlayer.transform.up) * DeviceUp;

            upPositionOffsetDir = DeviceUpInWorld;
        } */

        transform.position = TrackedPlayer.transform.position + upPositionOffsetDir * LOOK_HEIGHT - continousToPlayer * LOOK_DISTANCE;
        transform.LookAt(TrackedPlayer.transform, generalWorldUp);
        //transform.Rotate(transform.forward, Vector3.Angle(up, TrackedPlayer.transform.up) * Mathf.Sign(-Input.acceleration.x), Space.World);
    }

    private void End()
    {

    }
}