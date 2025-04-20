using UnityEngine;

public class TargetScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    public void showTarget(Vector3 targetPos)
    {
        print("AHH");
        targetPos.y = 25;
        //print out x y and z of targetPos
        Debug.Log("Target Position: " + targetPos.x + ", " + targetPos.y + ", " + targetPos.z);
        transform.position = targetPos;
        GetComponent<Renderer>().enabled = true;
    }

    public void hideTarget()
    {
        GetComponent<Renderer>().enabled = false;
    }


}