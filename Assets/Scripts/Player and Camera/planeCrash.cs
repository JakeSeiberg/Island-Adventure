using UnityEngine;
using System.Collections;

public class PlaneCrashPath : MonoBehaviour
{
    public Transform planeCamera;
    
    public ParticleSystem[] crashParticles;


    // Path positions
    private Vector3 startPoint = new Vector3(138f, 105f, -812f);
    private Vector3 cruisePoint = new Vector3(0f, 105f, -160f);
    private Vector3 crashPoint = new Vector3(120f, 9.2f, 40.9f);

    private float timer = 0f;
    private bool particlesTriggered = false;

    public CanvasGroup crashCanvasGroup;
    private bool fadeCanvas = false;

    public sceneSwitcher sceneSwitcher;

    void Start()
    {
        transform.position = startPoint;
        transform.rotation = Quaternion.LookRotation(cruisePoint - startPoint);
        crashCanvasGroup.alpha = 0f;
        playerData.curScene = "cutscene";
    }

    void Update()
    {
        timer += Time.deltaTime;

        Vector3 position;
        Quaternion rotation;
        float t;

        if (timer < 8f)
        {
            // Stage 1: Cruise phase, slight engine shake starts after 6s
            t = Mathf.InverseLerp(0f, 8f, timer);
            position = Vector3.Lerp(startPoint, cruisePoint, t);
            Vector3 direction = (cruisePoint - startPoint).normalized;
            rotation = Quaternion.LookRotation(direction);

            if (timer > 6f)
            {
                float shakeZ = Mathf.Sin(timer * 40f) * 6f;
                rotation *= Quaternion.Euler(0f, 0f, shakeZ);
            }
        }
        else if (timer < 13f)
        {
            // Stage 2: Dive + barrel roll (5 seconds of fast descent with spin)
            t = Mathf.InverseLerp(8f, 13f, timer);
            position = Vector3.Lerp(cruisePoint, crashPoint, t);
            Vector3 direction = (crashPoint - cruisePoint).normalized;
            rotation = Quaternion.LookRotation(direction);

            float rollZ = t * 1440f; // 4 full rolls in 5 seconds
            rotation *= Quaternion.Euler(0f, 0f, rollZ);

            if (!particlesTriggered && timer >= 12.5f)
            {
                TriggerCrashParticles();
                particlesTriggered = true;
            }
        }
        else
        {
            // Stage 3: Final crash
            position = crashPoint;
            rotation = Quaternion.Euler(2.31f, 57f, 17.83f);

            if (!fadeCanvas)
            {
                fadeCanvas = true;
                StartCoroutine(FadeInCanvas());
            }
        }

        transform.position = position;
        transform.rotation = rotation;

        // Lock camera to plane
        if (planeCamera != null)
        {
            planeCamera.position = position;
            planeCamera.rotation = rotation;
        }
    }

    private void TriggerCrashParticles()
    {
        foreach (ParticleSystem particle in crashParticles)
        {
            if (particle != null)
            {
                particle.Play(); // Start the particle system
            }
        }
    }

    private IEnumerator FadeInCanvas()
    {
        float duration = 2f; // Duration of the fade-in
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if (crashCanvasGroup != null)
            {
                crashCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / duration); // Gradually increase alpha
            }
            yield return null;
        }

        // Ensure the canvas is fully visible at the end
        if (crashCanvasGroup != null)
        {
            crashCanvasGroup.alpha = 1f;
        }

        sceneSwitcher.changeScene();
    }
}
