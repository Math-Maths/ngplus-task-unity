using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float lowerDistance = 5f; // Quanto a porta desce
    [SerializeField] private float speed = 2f; // Velocidade da descida

    private bool isOpen = false;
    private int objectives = 0;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition - new Vector3(0, lowerDistance, 0);
    }

    public void OpenDoor()
    {
        objectives++;

        if (isOpen) return;

        if (objectives >= 2)
        {
            isOpen = true;
            StartCoroutine(LowerDoor());
        }
    }

    private System.Collections.IEnumerator LowerDoor()
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        Debug.Log("Porta totalmente aberta.");
    }
}
