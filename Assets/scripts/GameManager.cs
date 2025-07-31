using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Lista de Prefabs")]
    public GameObject[] prefabs;
    public GameObject gameOverPanel;
    void Start()
    {
        SpawnRandomPrefab();
        PlayerPrefs.SetInt("Life", 3);
    }

    public void SpawnRandomPrefab()
    {
        if (prefabs.Length == 0)
        {
            Debug.LogWarning("Nenhum prefab atribuído no GameManager.");
            return;
        }

        int randomIndex = Random.Range(0, prefabs.Length);
        GameObject selectedPrefab = prefabs[randomIndex];

        Vector3 spawnPosition = new Vector3(0f, 2.69f, 0f);
        Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }

    public void ActivateGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}
