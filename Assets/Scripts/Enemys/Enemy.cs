using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    float currentHealth;
    Animator animator;
    
    void Start()
    {
        currentHealth=maxHealth;
        animator=GetComponent<Animator>();
    }

    public void TakeDamage(float Damage){
        Debug.Log(currentHealth);
        currentHealth-=Damage;
        animator.SetTrigger("Damaged");
        if(currentHealth<=0) Die();
    }

    void Die (){
        animator.SetBool("Dead",true);
    }
}
