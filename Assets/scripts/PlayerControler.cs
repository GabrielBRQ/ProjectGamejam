using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerControler : MonoBehaviour
{
    public Button defenseButton; // arraste o botão da UI aqui pelo Inspector
    public Button atackButton; // arraste o botão da UI aqui pelo Inspector

    public TMP_Text score;

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
                    // Inicia o efeito antes de destruir
                    StartCoroutine(EfeitoAntesDeDestruir(inimigo));
                }
                else
                {
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
            int newscore = int.Parse(score.text);
            newscore += 100;
            score.text = newscore.ToString();

            Vector3 posicaoInicial = inimigo.transform.position;

            // Escolhe uma direção aleatória em X
            float direcaoX = Random.Range(5f, 10f);
            direcaoX *= Random.value < 0.5f ? -1f : 1f;

            Vector3 posicaoFinal = posicaoInicial + new Vector3(direcaoX, 22f, 0f);

            float duracao = 0.5f;
            float tempo = 0f;

            while (tempo < duracao)
            {
                tempo += Time.deltaTime;
                float t = tempo / duracao;

                inimigo.transform.position = Vector3.Lerp(posicaoInicial, posicaoFinal, t);

                yield return null;
            }

            Destroy(inimigo);
            Debug.Log("Inimigo destruído após movimento suave.");
            GameManager gameManager = FindObjectOfType<GameManager>();

            gameManager.SpawnRandomPrefab();
        }
    }
}

