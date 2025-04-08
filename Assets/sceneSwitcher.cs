using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class sceneSwitcher : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //when escape is pressed, switch scene to "TYLER NEW SCENE"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainWorld"); 
        }
    }
}
