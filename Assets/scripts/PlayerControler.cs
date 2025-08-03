using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerControler : MonoBehaviour
{
    public Button defenseButton; // arraste o botão da UI aqui pelo Inspector
    public Button atackButton; // arraste o botão da UI aqui pelo Inspector
    public int scoreToImproveSpeed = 500;
    public TMP_Text score;
    public SFXManager sfxManager;

    bool punched = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (defenseButton != null)
            {
                defenseButton.onClick.Invoke(); // Simula o clique
            }
            else
            {
                Debug.LogWarning("Botão de defesa não atribuído no Inspector.");
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (atackButton != null)
            {
                atackButton.onClick.Invoke(); // Simula o clique
            }
            else
            {
                Debug.LogWarning("Botão de defesa não atribuído no Inspector.");
            }
        }

    }

    public void VerificarEEliminarInimigo()
    {
        GameObject inimigo = GameObject.FindGameObjectWithTag("enemy");

        if (inimigo != null)
        {
            Animator animator = inimigo.GetComponent<Animator>();

            if (animator != null)
            {
                bool vulneravel = animator.GetBool("vulne");

                if (vulneravel)
                {
                    ApplyDamage();
                }
                else
                {
                    animator.SetBool("def", true);
                    Debug.Log("Inimigo não está vulnerável.");
                }
            }
            else
            {
                Debug.LogWarning("Animator não encontrado no inimigo.");
            }
        }
        else
        {
            Debug.LogWarning("Nenhum inimigo com a tag 'enemy' encontrado.");
        }
    }

    private System.Collections.IEnumerator EfeitoAntesDeDestruir(GameObject inimigo)
    {
        if (inimigo != null)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            int newscore = int.Parse(score.text);
            newscore += 100;
            if (newscore >= scoreToImproveSpeed)
            {
                scoreToImproveSpeed += 500;
                if (gameManager != null)
                {
                    gameManager.AumentarTimeScale();
                }
            }
            score.text = newscore.ToString();
            sfxManager.PlayHypeSound();

            Vector3 posicaoInicial = inimigo.transform.position;

            // Desativa o Animator para evitar conflito de posição
            Animator anim = inimigo.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetBool("vulne", false);
                anim.SetBool("dmg", false);
                anim.enabled = false;
            }

            // Escolhe uma direção aleatória em X
            float direcaoX = Random.Range(5f, 15f);
            direcaoX *= Random.value < 0.5f ? -1f : 1f;

            Vector3 posicaoFinal = posicaoInicial + new Vector3(direcaoX, 35f, 0f);

            float duracao = 0.5f;
            float tempo = 0f;

            // Inicializa a escala em 0.6
            float scaleInicial = 0.4f;
            float scaleFinal = 0.1f;

            // Inicializa a rotação
            float anguloFinal = 720f; // O inimigo irá girar 360 graus

            while (tempo < duracao)
            {
                tempo += Time.deltaTime;
                float t = tempo / duracao;

                if (inimigo != null)
                {
                    // Movimento suave da posição
                    inimigo.transform.position = Vector3.Lerp(posicaoInicial, posicaoFinal, t);

                    // Diminuição da escala de 0.6 para 0.1 durante o movimento
                    float scale = Mathf.Lerp(scaleInicial, scaleFinal, t);
                    inimigo.transform.localScale = new Vector3(scale, scale, scale);

                    // Rotação suave de 0 a 360 graus durante o movimento (agora no eixo Z)
                    float anguloAtual = Mathf.Lerp(0f, anguloFinal, t);
                    inimigo.transform.rotation = Quaternion.Euler(0f, 0f, anguloAtual); // Rota no eixo Z
                }

                yield return null;
            }

            Destroy(inimigo);
            Debug.Log("Inimigo destruído após movimento suave.");

            gameManager.SpawnRandomPrefab();
        }
    }



    public IEnumerator CheckEnemyAttack()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("enemy");

        if (enemy == null)
        {
            Debug.LogWarning("Nenhum inimigo encontrado na cena.");
            yield break;
        }

        Animator animator = enemy.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Enemy não possui um componente Animator.");
            yield break;
        }

        float tempoTotal = 0f;
        float duracao = 0.5f;
        bool atacou = false;

        while (tempoTotal < duracao)
        {
            if (animator.GetBool("atack"))
            {
                atacou = true;
                break;
            }

            tempoTotal += Time.deltaTime;
            yield return null; // espera um frame
        }
        Animator playerAnimator = GetComponent<Animator>();
        if (atacou)
        {
            // Faça algo aqui
            playerAnimator.SetBool("vulne", false);
        }
        else
        {
            playerAnimator.SetBool("def", false);
            playerAnimator.SetBool("vulne", true);
        }
    }

    public void CallPlayPunchSound()
    {
        GameObject inimigo = GameObject.FindGameObjectWithTag("enemy");
        if (inimigo != null)
        {
            Animator animator = inimigo.GetComponent<Animator>();
            if (animator != null)
            {
                bool vulneravel = animator.GetBool("vulne");

                if (vulneravel)
                {
                    if (punched)
                    {
                        sfxManager.PlayPunchSound();
                    }
                    else
                    {
                        sfxManager.PlayPunchSound2();
                    }
                    punched = !punched;
                }
                else
                {
                    sfxManager.PlayPunchDefendSound();
                }
            }
        }
    }

    public void CallVulneSound()
    {
        sfxManager.PlayVulneSound();
    }

    public void ApplyDamage()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("enemy");

        if (enemy != null)
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();

            if (enemyController != null)
            {
                enemyController.life -= 1;

                if (enemyController.life < 1)
                {
                    StartCoroutine(EfeitoAntesDeDestruir(enemy));
                }
                else
                {
                    float randomValue = Random.value; // Retorna um valor entre 0 e 1
                    Debug.Log("randomValue é igual á: " + randomValue);
                    if (randomValue < enemyController.chanceToStun)
                    {
                        Debug.Log("Passou no if");
                        Animator animator = enemy.GetComponent<Animator>();
                        if (animator != null)
                        {
                            Debug.Log("Colocou dmg como true");
                            animator.SetBool("dmg", true);
                            enemyController.chanceToStun -= 0.1f;
                        }
                        else
                        {
                            Debug.LogWarning("Animator não encontrado no inimigo.");
                        }
                    }
                    else
                    {
                        enemyController.ResetEnemyAnimation();
                    }
                }
            }
            else
            {
                Debug.LogWarning("EnemyController não encontrado no inimigo.");
            }
        }
        else
        {
            Debug.LogWarning("Inimigo com tag 'enemy' não encontrado.");
        }
    }
}

