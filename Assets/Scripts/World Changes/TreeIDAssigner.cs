using UnityEngine;
using UnityEditor;

public class TreeIDAssigner
{
    [MenuItem("Tools/Assign Unique Tree IDs")]
    public static void AssignTreeIDs()
    {
        TreeID[] trees = GameObject.FindObjectsOfType<TreeID>();
        int count = 0;

        foreach (TreeID tree in trees)
        {
            tree.treeID = "Tree_" + count;
            tree.name = tree.treeID; // Optional: rename GameObject in hierarchy
            EditorUtility.SetDirty(tree); // Mark dirty so Unity saves it
            count++;
        }

        Debug.Log("Assigned unique IDs to " + count + " trees.");
    }
}

