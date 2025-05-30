using UnityEngine;
using System.Collections;

public class PlaneCrashPath : MonoBehaviour
{
    public Transform planeCamera;
    
    public ParticleSystem[] crashParticles;


    // Path positions
    private Vector3 startPoint = new Vector3(138f, 105f, -2602f);
    private Vector3 cruisePoint = new Vector3(0f, 105f, -260f);
    private Vector3 crashPoint = new Vector3(120f, 12f, 40.9f);

    private float timer = 0f;
    private bool particlesTriggered = false;

    public CanvasGroup crashCanvasGroup;
    private bool fadeCanvas = false;

    public SceneSwitcher SceneSwitcher;

    private float startShakeAt = 28f;
    private float shakeDuration = 3f;
    private float diveDuration = 5f;

    private float startParticlesAt = 35f;
    private float canvasFadeDuration = 2f;

    private float timeBeforeDive;
    private float totalTime;

    void Start()
    {
        playerData.newInstance();
        transform.position = startPoint;
        transform.rotation = Quaternion.LookRotation(cruisePoint - startPoint);
        crashCanvasGroup.alpha = 0f;
        playerData.curScene = "CutScene";
        timeBeforeDive = startShakeAt + shakeDuration;
        totalTime = startShakeAt + shakeDuration + diveDuration + canvasFadeDuration;

    }

    void Update()
    {
        timer += Time.deltaTime;

        Vector3 position;
        Quaternion rotation;
        float t;

        if (timer < timeBeforeDive)
        {
            t = Mathf.InverseLerp(0f, timeBeforeDive, timer);
            position = Vector3.Lerp(startPoint, cruisePoint, t);
            Vector3 direction = (cruisePoint - startPoint).normalized;
            rotation = Quaternion.LookRotation(direction);

            if (timer > startShakeAt)
            {
                float shakeZ = Mathf.Sin(timer * 40f) * 6f;
                rotation *= Quaternion.Euler(0f, 0f, shakeZ);
            }
        }
        else if (timer < 36f)
        {
            t = Mathf.InverseLerp(timeBeforeDive, (totalTime - canvasFadeDuration), timer);
            position = Vector3.Lerp(cruisePoint, crashPoint, t);
            Vector3 direction = (crashPoint - cruisePoint).normalized;
            rotation = Quaternion.LookRotation(direction);

            float rollZ = t * 1440f;
            rotation *= Quaternion.Euler(0f, 0f, rollZ);

            if (!particlesTriggered && timer >= startParticlesAt)
            {
                TriggerCrashParticles();
                particlesTriggered = true;
            }
        }
        else
        {
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
                particle.Play();
            }
        }
    }

    private IEnumerator FadeInCanvas()
    {
        float duration = canvasFadeDuration;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if (crashCanvasGroup != null)
            {
                crashCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / duration);
            }
            yield return null;
        }

        if (crashCanvasGroup != null)
        {
            crashCanvasGroup.alpha = 1f;
        }
        SceneSwitcher.changeScene();
    }
}
