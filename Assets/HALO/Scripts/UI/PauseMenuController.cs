using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    public void OnMainMenu(){
        SceneManager.LoadScene(0);
    }

}
