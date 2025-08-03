using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Lista de Prefabs")]
    public GameObject[] prefabs;
    public GameObject gameOverPanel;
    public GameObject[] hearts;
    public float baseValue = 0.1f;

    public void StartGame()
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

        string defeatedHero = PlayerPrefs.GetString("DefeatedHero", "");
        string newHero = defeatedHero;
        GameObject selectedPrefab = null;

        while (defeatedHero == newHero)
        {
            int randomIndex = Random.Range(0, prefabs.Length);
            selectedPrefab = prefabs[randomIndex];
            newHero = selectedPrefab.name;
        }

        PlayerPrefs.SetString("DefeatedHero", newHero);

        Vector3 spawnPosition = new Vector3(17f, -2.51f, 0f);
        GameObject instance = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

        StartCoroutine(MoverParaCentro(instance, new Vector3(-0.86f, -2.51f, 0f), 0.4f));
    }

    private IEnumerator MoverParaCentro(GameObject obj, Vector3 destino, float duracao)
    {
        Animator animator = obj.GetComponent<Animator>();

        float tempo = 0f;
        Vector3 posInicial = obj.transform.position;

        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            float t = tempo / duracao;
            obj.transform.position = Vector3.Lerp(posInicial, destino, t);
            yield return null;
        }

        obj.transform.position = destino; // Garante que fique exatamente no destino

        if (animator != null)
        {
            animator.enabled = true; // Reativa o Animator depois da movimentação
        }
    }

    public void AumentarTimeScale()
    {
        // Aumenta o timeScale com o valor atual
        Time.timeScale += baseValue;

        // Debug para visualizar
        Debug.Log("Novo Time.timeScale: " + Time.timeScale);

        // Reduz o valor base em 10% para a próxima vez
        baseValue *= 0.9f;

        Debug.Log("Próximo incremento será: " + baseValue);
    }

    public void ActivateGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        GameOverManager gameOverManager = GameObject.FindObjectOfType<GameOverManager>();
        gameOverManager.CallGameOver();
    }

    public void RemoveHearts()
    {
        int life = PlayerPrefs.GetInt("Life", 0); // Obtém o valor de "Life" dos PlayerPrefs, default 0

        if (life >= 0 && life < hearts.Length && hearts[life] != null)
        {
            Animator anim = hearts[life].GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetBool("Hit", true);
            }
        }
        else
        {
            Debug.LogWarning("Índice de Life inválido ou GameObject nulo.");
        }
    }

    public void FecharJogo()
    {
        #if UNITY_EDITOR
            // Se estiver no editor, para a execução
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Se for build final, fecha o jogo
            Application.Quit();
        #endif
    }

}
