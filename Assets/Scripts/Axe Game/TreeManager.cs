using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public GameObject leafPrefab; 
    public GameObject logPrefab;

    private int maxLogPerTree = 6;
    private int maxLeafPerTree = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        TreeID[] allTrees = GameObject.FindObjectsOfType<TreeID>();

        foreach (TreeID tree in allTrees)
        {
            if (playerData.brokenTrees.Contains(tree.treeID))
            {
                if ((playerData.treeChopped) && (tree.treeID == playerData.currentTreeID))
                {
                    Vector3 treePosition = tree.transform.position;

                    Destroy(tree.gameObject);
                    
                    for (int i = 0; i < Random.Range(3, maxLeafPerTree); i++)
                    {
                        float randomX = Random.Range(-3f, 3f);
                        float randomY = Random.Range(4f, 8f);
                        float randomZ = Random.Range(-3f, 3f);
                        Vector3 leafPosition = treePosition + new Vector3(randomX, randomY, randomZ);
                        Instantiate(leafPrefab, leafPosition, Quaternion.identity);
                    }

                    for (int i = 0; i < Random.Range(2, maxLogPerTree); i++)
                    {
                        float randomX = Random.Range(-3f, 3f);
                        float randomY = Random.Range(4f, 8f);
                        float randomZ = Random.Range(-3f, 3f);
                        Vector3 logPosition = treePosition + new Vector3(randomX, randomY, randomZ);
                        Instantiate(logPrefab, logPosition, Quaternion.identity);
                    }
                }
                else{
                    Destroy(tree.gameObject);
                }
            }

        }
        playerData.currentTreeID = null;
        playerData.treeChopped = false;
    }
}
