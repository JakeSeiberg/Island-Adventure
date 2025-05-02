using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionController : MonoBehaviour
{
    public void OnContinue(){
        SceneManager.LoadScene(3);
    }
}
