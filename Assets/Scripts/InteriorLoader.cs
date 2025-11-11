using UnityEngine;
using System.Collections.Generic;

public class InteriorLoader : MonoBehaviour
{
    [System.Serializable]
    public class InteriorMapping
    {
        public string interiorName; 
        public GameObject prefab;   
    }

    [Header("Configuración")]
    public List<InteriorMapping> interiorMappings = new List<InteriorMapping>();
    public Transform spawnPoint; // El (0,0,0) donde se crea el prefab

    // --- Variables privadas ---
    private GameObject currentInteriorInstance;
    private Transform currentPlayerSpawnPoint; // El spawn del jugador DENTRO del prefab

    public void LoadInterior(string interiorName)
    {
        if (currentInteriorInstance != null)
        {
            Destroy(currentInteriorInstance);
        }

        InteriorMapping mapping = interiorMappings.Find(m => m.interiorName == interiorName);

        if (mapping != null && mapping.prefab != null)
        {
            currentInteriorInstance = Instantiate(mapping.prefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);
            
            // --- LÍNEA NUEVA ---
            // Buscamos el punto de spawn del jugador que creamos DENTRO del prefab
            currentPlayerSpawnPoint = currentInteriorInstance.transform.Find("PlayerSpawnPoint_Interior");

            if (currentPlayerSpawnPoint == null)
            {
                Debug.LogWarning($"El prefab {interiorName} no tiene un 'PlayerSpawnPoint_Interior'. El jugador aparecerá en (0,0,0).");
            }
        }
        else
        {
            Debug.LogError($"[InteriorLoader] No se encontró un Prefab para: {interiorName}");
        }
    }

    // --- FUNCIÓN NUEVA ---
    // El GameManager usará esto para saber dónde poner al jugador
    public Transform GetCurrentPlayerSpawnPoint()
    {
        // Si no encontramos un spawn, devolvemos el spawn (0,0,0) como fallback
        return currentPlayerSpawnPoint != null ? currentPlayerSpawnPoint : spawnPoint;
    }
}