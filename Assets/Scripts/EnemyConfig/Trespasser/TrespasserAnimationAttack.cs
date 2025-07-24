using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrespasserAnimationAttack : MonoBehaviour
{
    List<Animator> animators = new List<Animator>();

    [SerializeField] float delayBetweenAttacks = 3f;

    void Start()
    {
        foreach (Transform child in transform)
        {
            Animator animator = child.GetComponent<Animator>();
            if (animator != null)
            {
                animators.Add(animator);
            }
        }

        // Bắt đầu Coroutine thực hiện random liên tục
        StartCoroutine(RandomAttackLoop());
    }

    IEnumerator RandomAttackLoop()
    {
        while (true)
        {

            int randomIndex = Random.Range(0, animators.Count);
            Animator chosenAnimator = animators[randomIndex];
            string[] attackParams = { "AttackPlayer1", "AttackPlayer2" };
            string chosenParam = attackParams[Random.Range(0, attackParams.Length)];
            // Bật animation
            chosenAnimator.SetBool(chosenParam, true);

            yield return new WaitForSeconds(1f);

            chosenAnimator.SetBool(chosenParam, false);
            yield return new WaitForSeconds(delayBetweenAttacks);
        }
    }


}
