using UnityEngine;

public class AnimatorBehabiour : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMovment(float speed, bool sprinting)
    {
        animator.SetFloat("Speed", speed);
        animator.SetBool("Sprinting", sprinting);
    }
}