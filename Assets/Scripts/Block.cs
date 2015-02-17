using System.Collections;
using System.Reflection;
using System.Linq;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Block))]
class BlockFacesEditor : Editor
{
    private System.Type[] blockFaceTypes;
    private string[] blockFaceOptions;

    public BlockFacesEditor()
    {
        blockFaceTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Namespace == "BlockFaces" && !x.IsAbstract).ToArray();
        blockFaceOptions = new string[blockFaceTypes.Length + 1];
        blockFaceOptions[0] = "none";
        for (int i = 0; i < blockFaceTypes.Length; ++i)
            blockFaceOptions[i + 1] = blockFaceTypes[i].Name;
    }

    public void OnSceneGUI()
    {
        // Snap on left mouse button released.
        if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
        {
            Block targetBlock = (Block)target;
            targetBlock.transform.position = new Vector3(Mathf.Round(targetBlock.transform.position.x),
                                                         Mathf.Round(targetBlock.transform.position.y),
                                                         Mathf.Round(targetBlock.transform.position.z));
        }
    }

    private int GetPopupTypeIndex(System.Type type)
    {
        if (type == null)
            return 0;
        else
        {
            for (int i = 0; i < blockFaceTypes.Length; ++i)
            {
                if (blockFaceTypes[i] == type)
                    return i+1;
            }
            Debug.LogError("Unknown BlockFace type (BlockFacesEditor)!");
            return -1;
        }
    }

    public override void OnInspectorGUI()
    {
        Block targetBlock = (Block)target;

        int[] typePerDir = new int[6];
        typePerDir[(int)Direction.POS_X] = EditorGUILayout.Popup("PosX", GetPopupTypeIndex(targetBlock.GetFaceType(Direction.POS_X)), blockFaceOptions);
        typePerDir[(int)Direction.NEG_X] = EditorGUILayout.Popup("NegX", GetPopupTypeIndex(targetBlock.GetFaceType(Direction.NEG_X)), blockFaceOptions);
        typePerDir[(int)Direction.POS_Y] = EditorGUILayout.Popup("PosY", GetPopupTypeIndex(targetBlock.GetFaceType(Direction.POS_Y)), blockFaceOptions);
        typePerDir[(int)Direction.NEG_Y] = EditorGUILayout.Popup("NegY", GetPopupTypeIndex(targetBlock.GetFaceType(Direction.NEG_Y)), blockFaceOptions);
        typePerDir[(int)Direction.POS_Z] = EditorGUILayout.Popup("PosZ", GetPopupTypeIndex(targetBlock.GetFaceType(Direction.POS_Z)), blockFaceOptions);
        typePerDir[(int)Direction.NEG_Z] = EditorGUILayout.Popup("NegZ", GetPopupTypeIndex(targetBlock.GetFaceType(Direction.NEG_Z)), blockFaceOptions);

        // Optional all box.
        if (typePerDir.All(x => typePerDir[0] == x))
        {
            int allSides = EditorGUILayout.Popup("AllSides", typePerDir[(int)Direction.POS_X], blockFaceOptions);
            for (int i = 0; i < 6; ++i)
                typePerDir[i] = allSides;
        }

        if (GUI.changed)
        {
            for (int i = 0; i < 6; ++i)
            {
                targetBlock.SetFace(Direction.DIRS[i], typePerDir[i] == 0 ? null : blockFaceTypes[typePerDir[i] - 1]);
            }
            EditorUtility.SetDirty(target);
        }
    }
}

[ExecuteInEditMode]
public class Block : MonoBehaviour
{
    public void SetFace(Direction direction, System.Type faceType)
    {
        BlockFaces.BlockFace ownFace = GetFace(direction);

        if (faceType == null)
        {
            if (ownFace != null)
            {
                DestroyImmediate(ownFace.gameObject);
            }
        }
        else if (ownFace == null || ownFace.GetType() != faceType)
        {
            // Get prefab.
            string prefabPath = "BlockFaces/" + faceType.Name;
            Object prefab = Resources.Load(prefabPath);
            if (prefab == null)
            {
                Debug.LogError("There is no prefab \"" + prefabPath + "\"! Please add a prefab for this type in Resources/BlockFaces");
                return;
            }
            GameObject prefabInstance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

            // Get and check for logic component (a component that derives from BlockFaces.BlockFace).
            Component[] blockFace = prefabInstance.GetComponents(typeof(BlockFaces.BlockFace));
            if (blockFace == null || blockFace.Length == 0)
            {
                Debug.LogError("The prefab \"" + prefabPath + "\" does not contain a BlockFace component!");
                return;
            }
            else if (blockFace.Length != 1)
            {
                Debug.LogError("The prefab \"" + prefabPath + "\" contains multiple BlockFace components!");
                return;
            }
            ((BlockFaces.BlockFace)blockFace[0]).DirectionIndex = (int)direction;

            // Delete old face
            if (ownFace != null)
            {
                DestroyImmediate(ownFace.gameObject);
            }

            // Orient the new face.
            prefabInstance.transform.parent = transform;
            prefabInstance.transform.position = transform.position + direction.Vector * 0.5f;
            prefabInstance.transform.rotation = Quaternion.FromToRotation(Direction.POS_Y.Vector, direction.Vector);
        }
    }

    BlockFaces.BlockFace GetFace(Direction direction)
    {
        //return transform.GetComponentsInChildren<BlockFaces.BlockFace>().SingleOrDefault(x => x.Direction == direction);
        var a = transform.GetComponentsInChildren<BlockFaces.BlockFace>();
        for (int i = 0; i < a.Length; ++i)
        {
            if (a[i].Direction == direction)
                return a[i];
        }
        return null;
    }

    public System.Type GetFaceType(Direction direction)
    {
        var face = GetFace(direction);
        if (face == null)
            return null;
        else
            return face.GetType();
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
