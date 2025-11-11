using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Referencias del Jugador")]
    public PlayerMovement playerMovement;
    public MouseLook mouseLook;

    [Header("Referencias del Mundo")]
    public string dialogueSceneName = "Scene_Dialogue";
    // ¡IMPORTANTE! Arrastra tu GameObject "MainWorldRoot" aquí
    public GameObject mainWorldRoot; 

    // Guardaremos la posición de la puerta para saber a dónde volver
    private Vector3 mainWorldDoorPosition;

    void Awake()
    {
        // (Singleton... sin cambios)
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }

    // --- FUNCIÓN MODIFICADA ---
    public void LoadDialogueScene(string interiorName, Transform doorTransform)
    {
        // Guardamos la posición de la puerta ANTES de entrar
        mainWorldDoorPosition = doorTransform.position; 
        StartCoroutine(LoadDialogueCoroutine(interiorName));
    }

    private IEnumerator LoadDialogueCoroutine(string interiorName)
    {
        // 1. Desactivar control (temporalmente)
        playerMovement.enabled = false;
        mouseLook.enabled = false;

        // 2. ¡Ocultar el mundo principal!
        if (mainWorldRoot != null) mainWorldRoot.SetActive(false);

        // 3. Cargar la escena de diálogo
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(dialogueSceneName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone) { yield return null; }

        // 4. Buscar el InteriorLoader y cargar el Prefab
        InteriorLoader loader = FindObjectOfType<InteriorLoader>();
        if (loader != null)
        {
            loader.LoadInterior(interiorName);

            // 5. ¡TELETRANSPORTAR AL JUGADOR!
            Transform playerSpawnPoint = loader.GetCurrentPlayerSpawnPoint();
            playerMovement.gameObject.transform.position = playerSpawnPoint.position;
            playerMovement.gameObject.transform.rotation = playerSpawnPoint.rotation;
        }

        // 6. ¡REACTIVAR el control del jugador!
        playerMovement.enabled = true;
        mouseLook.enabled = true;

        // 7. El cursor se queda BLOQUEADO para poder mover la cámara
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // --- FUNCIÓN MODIFICADA ---
    public void UnloadDialogueScene()
    {
        // 1. Desactivar control (temporalmente)
        playerMovement.enabled = false;
        mouseLook.enabled = false;
        
        // 2. Descargar la escena (esto destruye el prefab y el InteriorLoader)
        SceneManager.UnloadSceneAsync(dialogueSceneName);

        // 3. ¡Mostrar el mundo principal de nuevo!
        if (mainWorldRoot != null) mainWorldRoot.SetActive(true);

        // 4. ¡Teletransportar al jugador de VUELTA a la puerta!
        playerMovement.gameObject.transform.position = mainWorldDoorPosition;
        
        // 5. Reactivar control
        playerMovement.enabled = true;
        mouseLook.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}