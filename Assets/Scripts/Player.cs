using UnityEngine;
using System.Collections;
using UnityEditor;

class Player : MonoBehaviour
{
    public GameCamera MainCamera = null;

    /// <summary>
    /// Prefab templates.
    /// </summary>
    private Object magicianPrefab, snailPrefab;

    private GameObject statePrefabInstance;
    private PlayerState state;

    public bool IsSnail { get { return state.GetType() == typeof(Snail); } }

    const int PLAYER_LAYER = 1 << 8;

    public enum LookDirection
    {
        RIGHT,
        LEFT
    };
    public LookDirection CurrentLookDirection
    {
        get { return currentLookDirection; }
        set { currentLookDirection = value; }
    }
    private LookDirection currentLookDirection = LookDirection.RIGHT;

    public Int3 GridPosition
    {
        get { return new Int3(transform.position); }
        set
        {
            transform.position = (Vector3)value;
        }
    }

    void Start()
    {
        // Load prefab templates.
        magicianPrefab = Resources.Load("Magician");
        if (magicianPrefab == null)
        {
            Debug.LogError("There is no prefab \"Magician\"!"); // Should never happen.
            return;
        }
        snailPrefab = Resources.Load("Snail");
        if (snailPrefab == null)
        {
            Debug.LogError("There is no prefab \"Snail\"!"); // Should never happen.
            return;
        }

        SwitchToMagician();
    }

    private void SwitchToMagician()
    {
        if (statePrefabInstance != null)
            DestroyImmediate(statePrefabInstance);

        statePrefabInstance = (GameObject)PrefabUtility.InstantiatePrefab(magicianPrefab);
        state = statePrefabInstance.GetComponent<Magician>();
        if (state == null)
        {
            Debug.LogError("There is no Magician script in Magician Prefab!"); // Should never happen.
        }
        else
        {
            state.Init(this, MainCamera);
        }
    }

    private void SwitchToSnail()
    {
        if (statePrefabInstance != null)
            DestroyImmediate(statePrefabInstance);

        statePrefabInstance = (GameObject)PrefabUtility.InstantiatePrefab(snailPrefab);
        state = statePrefabInstance.GetComponent<Snail>();
        if (state == null)
        {
            Debug.LogError("There is no Snail script in Snail Prefab!"); // Should never happen.
        }
        else
        {
            state.Init(this, MainCamera);
        }
    }

    void Update()
    {
        // Switch player?
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !MainCamera.CameraFollowingLastTouch)
        {
            Ray ray = MainCamera.GetComponent<Camera>().ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, Mathf.Infinity, Player.PLAYER_LAYER))
            {
                if (state.GetType() == typeof(Magician))
                    SwitchToSnail();
                else
                    SwitchToMagician();
            }
        }

        // Change orientation to match right/left setting
        if (Input.touchCount == 0)
        {
            Int3 sideVec = new Int3(Vector3.Cross(MainCamera.DiscreteToPlayer.Vector, transform.up)) * (CurrentLookDirection == LookDirection.RIGHT ? -1 : 1);
            if (new Int3(transform.forward) == -sideVec && MainCamera.CameraFollowingLastTouch)
            {
                CurrentLookDirection = CurrentLookDirection == LookDirection.RIGHT ? LookDirection.LEFT : LookDirection.RIGHT;
                sideVec = -sideVec;
            }

            //transform.forward = (Vector3)sideVec; 
            transform.rotation = Quaternion.LookRotation((Vector3)sideVec, transform.up);
        }
    }
}
