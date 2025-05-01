using UnityEngine;
using System.Collections;

public class AxeScript : MonoBehaviour
{
    public Transform accuracyBar;             
    public Transform accuracyMeterGreen;      

    public Quaternion startRotation = Quaternion.Euler(-6.18f, -50.66f, 5.63f);
    public Quaternion halfwayRotation = Quaternion.Euler(-56.9f, -38.2f, 5.63f);
    public Quaternion targetRotation = Quaternion.Euler(-70f, -99.6f, -343.7f);

    public float swingDuration = 0.5f;

    private bool isSwinging = false;

    public ChopCounter chopCounter; 

    public AudioManager audioManager;

    void Start()
    {
        // Set starting rotation
        transform.rotation = startRotation;
    }

    void Update()
    {
        if (!isSwinging && Input.GetKeyDown(KeyCode.Space))
        {
            float barY = accuracyBar.transform.position.y;
            float greenY = accuracyMeterGreen.position.y;

            float greenMinY = greenY - 0.35f;
            float greenMaxY = greenY + 0.35f;

            if (barY >= greenMinY && barY <= greenMaxY)
            {
                // ✅ Successful hit in green
                StartCoroutine(SwingAxe());
                chopCounter.RegisterHit();

                
                // Move green zone to new random Y position
                float newY = Random.Range(0.35f, 6.85f);
                accuracyMeterGreen.position = new Vector3(
                    accuracyMeterGreen.position.x,
                    newY,
                    accuracyMeterGreen.position.z
                );
            }
            else
            {
                // ❌ Missed input
                chopCounter.RegisterMiss();
                audioManager.playWhiff();
            }
        }
    }

    IEnumerator SwingAxe()
    {
        isSwinging = true;

        float elapsedTime = 0f;

        // Forward swing
        while (elapsedTime < swingDuration)
        {
            float t = elapsedTime / swingDuration;
            Quaternion firstLerp = Quaternion.Slerp(startRotation, halfwayRotation, t);
            Quaternion secondLerp = Quaternion.Slerp(halfwayRotation, targetRotation, t);
            transform.rotation = Quaternion.Slerp(firstLerp, secondLerp, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioManager.playAxe();

        transform.rotation = targetRotation;

        yield return new WaitForSeconds(0.1f);

        // Return to start rotation
        elapsedTime = 0f;
        while (elapsedTime < swingDuration)
        {
            float t = elapsedTime / swingDuration;
            transform.rotation = Quaternion.Slerp(targetRotation, startRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = startRotation;
        isSwinging = false;
    }
}
