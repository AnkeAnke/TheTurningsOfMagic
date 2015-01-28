using System.Collections;
using System.Reflection;
using System.Linq;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Block))]
class BlockFacesEditor : Editor
{
    System.Type[] blockFaceTypes;
    string[] blockFaceOptions;

    public BlockFacesEditor()
    {
        blockFaceTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Namespace == "BlockFaces" && x.IsClass).ToArray();
        blockFaceOptions = new string[blockFaceTypes.Length + 1];
        blockFaceOptions[0] = "none";
        for (int i = 0; i < blockFaceTypes.Length; ++i)
            blockFaceOptions[i + 1] = blockFaceTypes[i].Name;
    }

    public override void OnInspectorGUI()
    {
        int typePosX = EditorGUILayout.Popup("PosX", 0, blockFaceOptions);
        int typeNegX = EditorGUILayout.Popup("NegX", 0, blockFaceOptions);
        int typePosY = EditorGUILayout.Popup("PosY", 0, blockFaceOptions);
        int typeNegY = EditorGUILayout.Popup("NegY", 0, blockFaceOptions);
        int typePosZ = EditorGUILayout.Popup("PosZ", 0, blockFaceOptions);
        int typeNegZ = EditorGUILayout.Popup("NegZ", 0, blockFaceOptions);

        if (GUI.changed)
            EditorUtility.SetDirty(target);
    }
}


public class Block : MonoBehaviour
{
    private BlockFaces.BlockFace[] faces = new BlockFaces.BlockFace[6];

    void Start()
    {

    }

    void Update()
    {

    }
}
