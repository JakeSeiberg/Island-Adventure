using UnityEngine;

public class AxeController : MonoBehaviour
{
    public AccuracyBarMover accuracyBar;             
    public Transform accuracyMeterGreen;             
    public float swingAngle = 45f;
    public float swingSpeed = 5f;

    private bool isSwinging = false;
    private Quaternion originalRotation;
    private Quaternion targetRotation;
    private bool swingingForward = true;

    void Start()
    {
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (!isSwinging && Input.GetKeyDown(KeyCode.Space))
        {
            float barY = accuracyBar.transform.position.y;
            float greenY = accuracyMeterGreen.position.y;

            float greenMinY = greenY - 0.25f;
            float greenMaxY = greenY + 0.25f;

            if (barY >= greenMinY && barY <= greenMaxY)
            {
                targetRotation = Quaternion.Euler(0, 0, -swingAngle);
                isSwinging = true;
                swingingForward = true;

                float newY = Random.Range(-3f, 3f);
                accuracyMeterGreen.position = new Vector3(
                    accuracyMeterGreen.position.x,
                    newY,
                    accuracyMeterGreen.position.z
                );
            }
        }

        if (isSwinging)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, swingSpeed);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                if (swingingForward)
                {
                    swingingForward = false;
                    targetRotation = originalRotation;
                }
                else
                {
                    isSwinging = false;
                }
            }
        }
    }
}
