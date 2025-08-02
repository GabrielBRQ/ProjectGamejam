using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public void AtivarAtackPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Animator animator = player.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("atack", true); // Troque "SeuParametro" pelo nome real do parâmetro
            }
            else
            {
                Debug.LogWarning("Animator não encontrado no Player.");
            }
        }
        else
        {
            Debug.LogWarning("Objeto com tag 'Player' não encontrado.");
        }
    }

    public void ResetAnimation()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Animator animator = player.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("def", false); // Troque "SeuParametro" pelo nome real do parâmetro
                animator.SetBool("atack", false); // Troque "SeuParametro" pelo nome real do parâmetro
                animator.SetBool("dmg", false); // Troque "SeuParametro" pelo nome real do parâmetro
                animator.SetBool("vulne", false);
            }
            else
            {
                Debug.LogWarning("Animator não encontrado no Player.");
            }
        }
        else
        {
            Debug.LogWarning("Objeto com tag 'Player' não encontrado.");
        }
    }

    public void AtivarDefensePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Animator animator = player.GetComponent<Animator>();
            if (animator != null)
            {
                // Verifica se "atack" está ativo
                if (!animator.GetBool("atack") && !animator.GetBool("vulne"))
                {
                    animator.SetBool("def", true);
                    PlayerControler playerControler = FindObjectOfType<PlayerControler>();
                    StartCoroutine(playerControler.CheckEnemyAttack());
                }
                else
                {
                    Debug.Log("Defesa não ativada porque 'atack' está ativo.");
                }
            }
            else
            {
                Debug.LogWarning("Animator não encontrado no Player.");
            }
        }
        else
        {
            Debug.LogWarning("Objeto com tag 'Player' não encontrado.");
        }
    }

    public void InverterSprite()
    {
        Debug.Log("Chamou a função, invertendo sprite...");
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
}
