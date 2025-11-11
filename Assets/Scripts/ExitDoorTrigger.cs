using UnityEngine;

public class ExitDoorTrigger : MonoBehaviour
{
    // Este script es mucho más simple
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Le dice al GameManager que descargue la escena actual
            GameManager.Instance.UnloadDialogueScene();
        }
    }
}