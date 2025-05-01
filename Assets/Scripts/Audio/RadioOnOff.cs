using UnityEngine;
using UnityEngine.SceneManagement;

public class RadioOnOff : MonoBehaviour
{
    public GameObject radio;
    void Update()
    {
        if (playerData.radioOn)
        {
            radio.SetActive(true);
        }
        else
        {
            radio.SetActive(false);
        }
    }
}
