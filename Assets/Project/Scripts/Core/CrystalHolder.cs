using UnityEngine;

public class CyrstalHolder : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Crystal"))
        {
            Debug.Log("Got the Crystal");
        }
    }

}