using UnityEngine;
using UnityEngine.UI;

public class DialogueUIManager : MonoBehaviour
{
    // Arrastra tu botón "Salir" aquí en el Inspector
    public Button exitButton;

    void Start()
    {
        // 1. Le decimos al botón que, cuando se haga clic, llame a nuestra función
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(OnExitClicked);
        }
    }

    void OnExitClicked()
    {
        // 2. Llama al GameManager para que descargue esta escena
        // Usamos "Instance" porque el GameManager está vivo en la otra escena
        GameManager.Instance.UnloadDialogueScene();
    }
}