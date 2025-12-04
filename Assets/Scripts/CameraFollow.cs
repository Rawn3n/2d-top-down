using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Mål")]
    public Transform target;

    [Header("Smoothing")]
    [Tooltip("Hvor hurtigt kameraet glider mod spilleren (højere = hurtigere)")]
    public float smoothSpeed = 8f;

    [Header("Offset")]
    [Tooltip("Hvis du vil have kameraet til at være lidt forskudt (fx 0, 0, -10)")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    [Header("Begrænsning (valgfri)")]
    public bool useBounds = false;
    public Vector2 minBounds; // fx (-20, -10)
    public Vector2 maxBounds; // fx ( 20,  10)

    void LateUpdate()
    {
        if (target == null) return;

        // Ønsket position (spilleren + offset)
        Vector3 desiredPosition = target.position + offset;

        // Hvis vi bruger bounds, klamper vi X/Y
        if (useBounds)
        {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);
        }

        // Smooth bevægelse mod målet
        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Flyt kameraet
        transform.position = smoothed;
    }
}
