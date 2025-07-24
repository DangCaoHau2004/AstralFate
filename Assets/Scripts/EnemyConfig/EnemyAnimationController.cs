using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    Animator animator;


    void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();

    }


    public void SetMoveAnimation(bool value)
    {
        animator.SetBool("Move", value);

    }

    public void SetAttackAnimation(bool value)
    {
        animator.SetBool("Attack", value);
    }

    public void SetDeathAnimation(bool value)
    {
        animator.SetBool("Death", value);

    }
    public void SetIdleAnimation(bool value)
    {
        animator.SetBool("Death", value);

    }
}
