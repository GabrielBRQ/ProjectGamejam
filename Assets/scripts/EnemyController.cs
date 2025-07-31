using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Chances (0 a 1)")]
    [Range(0f, 1f)] public float chanceDeAtaque = 0.5f;
    [Range(0f, 1f)] public float chanceDeVulnerabilidade = 0.3f;

    [Header("Intervalo entre ações")]
    public float tempoEntreAcoes = 2f; // tempo em segundos

    private Animator animator;
    private float tempoProximaAcao;
    public int teste = 1;
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator não encontrado no EnemyController.");
        }

        tempoProximaAcao = Time.time + tempoEntreAcoes;
    }

    void Update()
    {
        if (Time.time >= tempoProximaAcao)
        {
            RealizarAcao();
            tempoProximaAcao = Time.time + tempoEntreAcoes;
        }
    }

    void RealizarAcao()
    {
        float random = Random.value;

        if (random < chanceDeAtaque)
        {
            animator.SetBool("atack", true);
            animator.SetBool("vulne", false);

            // Verifica o estado de defesa do Player

        }
        else if (random < chanceDeAtaque + chanceDeVulnerabilidade)
        {
            animator.SetBool("vulne", true);
            animator.SetBool("atack", false);
        }
        else
        {
            animator.SetBool("atack", false);
            animator.SetBool("vulne", false);
        }
    }

    public void ResetEnemyAnimation()
    {
        animator.SetBool("atack", false);
        animator.SetBool("vulne", false);
    }

    public void RealizarAtack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Animator playerAnimator = player.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                bool estaDefendendo = playerAnimator.GetBool("def");

                if (!estaDefendendo)
                {
                    playerAnimator.SetBool("dmg", true);

                    int vidaAtual = PlayerPrefs.GetInt("Life", 3);
                    vidaAtual--;
                    PlayerPrefs.SetInt("Life", vidaAtual);

                    if (vidaAtual <= 0)
                    {
                        GameManager managerScript = GameObject.FindObjectOfType<GameManager>();
                        if (managerScript != null)
                        {
                            managerScript.ActivateGameOverPanel();
                        }
                        else
                        {
                            Debug.LogWarning("Script GameManager não encontrado na cena.");
                        }
                    }
                }

            }
            else
            {
                Debug.LogWarning("Animator não encontrado no Player.");
            }
        }
        else
        {
            Debug.LogWarning("Player não encontrado na cena.");
        }
    }
}