using UnityEngine;
using System.Collections;
using UnityEditor;

class Player : MonoBehaviour
{
    public GameCamera MainCamera;

    /// <summary>
    /// Prefab templates.
    /// </summary>
    private Object magicianPrefab, snailPrefab;

    private GameObject statePrefabInstance;
    private PlayerState state;

    const int PLAYER_LAYER = 1 << 8;
    
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
            state.Init(transform, MainCamera);
            state.transform.parent = transform;
            state.transform.localPosition = Vector3.zero;
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
            state.Init(transform, MainCamera);
            state.transform.parent = transform;
            state.transform.localPosition = Vector3.zero;
        }
    }

    void Update()
    {
        // Switch player?
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !MainCamera.CameraFollowingLastTouch)
        {
            Ray ray = MainCamera.camera.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, Mathf.Infinity, Player.PLAYER_LAYER))
            {
                if (state.GetType() == typeof(Magician))
                    SwitchToSnail();
                else
                    SwitchToMagician();
            }
        }
    }
}
