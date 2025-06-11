using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    [Header("Float Settings")]
    [SerializeField] float floatAmplitude = 0.25f;
    [SerializeField] float floatFrequency = 1f;

    [Header("Rotation Settings")]
    [SerializeField] float rotationSpeed = 45f;

    private Vector3 startPosition;
    private bool isAnimating = true;
    private float floatTimer = 0f;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (!isAnimating) return;

        floatTimer += Time.deltaTime;

        // Floating
        float offsetY = Mathf.Sin(floatTimer * floatFrequency) * floatAmplitude;
        Vector3 newPosition = startPosition + new Vector3(0, offsetY, 0);
        transform.position = newPosition;

        // Rotation
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }

    public void PauseRotationAndFloat()
    {
        isAnimating = false;
        startPosition = transform.position;
    }

    public void ResumeRotationAndFloat()
    {
        isAnimating = true;
        floatTimer = 0f;
        startPosition = new Vector3(transform.position.x, 0.8f, transform.position.z);
        transform.rotation = Quaternion.Euler(Vector3.up);
    }
}
