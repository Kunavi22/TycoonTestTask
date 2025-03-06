using UnityEngine;

public class RandomizeAnimation : MonoBehaviour
{
    void Start()
    {
        Animator animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, UnityEngine.Random.value);
        }
    }
}
