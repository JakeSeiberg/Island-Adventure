using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public GameObject leafPrefab; 
    public GameObject logPrefab;
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
                    
                    for (int i = 0; i < Random.Range(1, 5); i++)
                    {
                        float randomX = Random.Range(-3f, 3f);
                        float randomY = Random.Range(4f, 8f);
                        float randomZ = Random.Range(-3f, 3f);
                        Vector3 leafPosition = treePosition + new Vector3(randomX, randomY, randomZ);
                        Instantiate(leafPrefab, leafPosition, Quaternion.identity);
                    }

                    for (int i = 0; i < Random.Range(1, 4); i++)
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

            // Clear state
        }
        playerData.currentTreeID = null;
        playerData.treeChopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
