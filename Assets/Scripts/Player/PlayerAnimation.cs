using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator myAnimator;
    Animator swordAnimation;
    private void Awake()
    {
        myAnimator = GetComponentInChildren<Animator>();
        swordAnimation = transform.GetChild(1).GetComponent<Animator>();
    }
    public void Move(float move)
    {
        myAnimator.SetFloat("Move", Mathf.Abs(move));
        
    }
    public void Jump(bool value)
    {
        myAnimator.SetBool("Jumping", value);
    }
    public void Attack()
    {
        myAnimator.SetTrigger("Attack");
        swordAnimation.SetTrigger("SwordAnimation");
    }
    public void Die()
    {
        myAnimator.SetTrigger("Death");
    }

}
