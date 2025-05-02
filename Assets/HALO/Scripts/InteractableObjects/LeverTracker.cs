using UnityEngine;

public class LeverTracker : MonoBehaviour
{
    public static bool[] leversSwitched;
    public int numberOfRooms;
    private bool yawnCheck;
    private float time;

    void Start()
    {
        yawnCheck = false;
        time = Time.time;

        leversSwitched = new bool[numberOfRooms];
        for (int i = 0; i < numberOfRooms; i++)
        {
            leversSwitched[i] = false;
        }
    }

    void Update()
    {
        for (int i = 0; i < numberOfRooms; i++){
            if (!leversSwitched[i]){
                yawnCheck = false;
            }
            else{
                yawnCheck = true;
            }
        }

        if(yawnCheck && Time.time - time > 10){
            _AudioManager.Instance.playYawnSound();
            time = Time.time;
        }

    }

    public static void resetLevers(){
        for (int i = 0; i < leversSwitched.Length; i++)
        {
            leversSwitched[i] = false;
        }
    }
}
