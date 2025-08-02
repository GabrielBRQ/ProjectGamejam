using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Chances (0 a 1)")]
    [Range(0f, 1f)] public float chanceDeAtaque = 0.5f;
    [Range(0f, 1f)] public float chanceDeVulnerabilidade = 0.3f;

    [Header("Intervalo entre ações")]
    public float tempoEntreAcoes = 2f; // tempo em segundos
    public int life = 3;
    public float chanceToStun = 0.4f;

    private Animator animator;
    private float tempoProximaAcao;

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
        animator.SetBool("dmg", false);
    }

    public void RealizarAtack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            GameManager managerScript = GameObject.FindObjectOfType<GameManager>();
            SFXManager sfxManager = GameObject.FindObjectOfType<SFXManager>();
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
                    managerScript.RemoveHearts();
                    sfxManager.PlayAhhSound();

                    if (vidaAtual <= 0)
                    {
                        if (managerScript != null)
                        {
                            managerScript.ActivateGameOverPanel();
                            chanceDeAtaque = 0;
                            chanceDeVulnerabilidade = 0;
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

    public void EnemyCallPlayPunchSound()
    {
        SFXManager sfxManager = FindObjectOfType<SFXManager>();
        GameObject inimigo = GameObject.FindGameObjectWithTag("Player");

        Animator animator = inimigo.GetComponent<Animator>();
        if (animator != null)
        {
            bool def = animator.GetBool("def");

            if (!def)
            {
                sfxManager.PlayPunchSound();
                StartCoroutine(WaitAndCallOutchSound());
            }
            else
            {
                sfxManager.PlayPunchDefendSound();
            }
        }
    }

    public IEnumerator WaitAndCallOutchSound()
    {
        SFXManager sfxManager = FindObjectOfType<SFXManager>();
        yield return new WaitForSeconds(0.2f);
        sfxManager.PlayOutchSound();
    }

    public void ActivateVulne()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("vulne", true);
        }
        else
        {
            Debug.LogWarning("Animator não encontrado no GameObject.");
        }
    }

    public void CallChargeSound()
    {
        SFXManager sfxManager = FindObjectOfType<SFXManager>();
        sfxManager.PlayChargeSound();
    }

    public void CallConfusedSound()
    {
        SFXManager sfxManager = FindObjectOfType<SFXManager>();
        sfxManager.PlayConfusedSound();
    }
}