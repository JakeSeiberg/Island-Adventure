using UnityEngine;
using System.Collections;

public class AxeScript : MonoBehaviour
{
    void Start()
    {
        // Define start, halfway, and target rotations
        Quaternion startRotation = Quaternion.Euler(-6.18f, -50.66f, 5.63f);
        Quaternion halfwayRotation = Quaternion.Euler(-56.9f, -38.2f, 5.63f);
        Quaternion targetRotation = Quaternion.Euler(-70f, -99.6f, -343.7f);

        // Start the smooth rotation coroutine
        StartCoroutine(RotateSmoothly(startRotation, halfwayRotation, targetRotation, 0.5f));
    }

    IEnumerator RotateSmoothly(Quaternion from, Quaternion control, Quaternion to, float duration)
    {
        while (true)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                // Smoothly interpolate between the rotations
                Quaternion firstLerp = Quaternion.Slerp(from, control, t);
                Quaternion secondLerp = Quaternion.Slerp(control, to, t);
                transform.rotation = Quaternion.Slerp(firstLerp, secondLerp, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure final rotation is set precisely
            transform.rotation = to;
        }
    }
}
