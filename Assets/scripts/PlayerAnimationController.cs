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
                animator.SetBool("def", true); // Troque "SeuParametro" pelo nome real do parâmetro
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
}
