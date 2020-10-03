using System.Collections;
using UnityEngine;

public class TutorialViking : MonoBehaviour
{

    private Animator animator;
    private bool isRunning;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isRunning)
        {
            StartCoroutine(TakeAction());
        }
    }

    private IEnumerator TakeAction()
    {
        isRunning = true;
        int chance = Random.Range(0, 100);
        if (chance > 25 && chance < 50)
        {
            animator.SetTrigger("Melee");
        }
        else if (chance > 0 && chance < 25)
        {
            animator.SetTrigger("Range");
        }
        yield return new WaitForSeconds(2f);
        isRunning = false;
    }
}
