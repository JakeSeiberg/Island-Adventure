using UnityEngine;

public class TreeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playerData.treeChopped && !string.IsNullOrEmpty(playerData.currentTreeID))
        {
            TreeID[] allTrees = GameObject.FindObjectsOfType<TreeID>();

            foreach (TreeID tree in allTrees)
            {
                if (tree.treeID == playerData.currentTreeID)
                {
                    Debug.Log("Destroying tree with ID: " + tree.treeID);
                    Destroy(tree.gameObject);
                    break;
                }
            }

            // Clear state
            playerData.currentTreeID = null;
            playerData.treeChopped = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
