using UnityEngine;

public class StatusBarKeepAwake : MonoBehaviour
{
    private static StatusBarKeepAwake instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
