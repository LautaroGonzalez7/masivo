using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string interiorName = "Pizzeria";
    public GameObject pressEPrompt; 
    private bool playerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (pressEPrompt != null) pressEPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (pressEPrompt != null) pressEPrompt.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // --- LÍNEA MODIFICADA ---
            // Ahora también pasamos la posición de esta puerta
            GameManager.Instance.LoadDialogueScene(interiorName, transform);
        }
    }
}