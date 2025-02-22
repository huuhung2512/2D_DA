using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTestDummy : MonoBehaviour,IDamageable
{
    [SerializeField] private GameObject hitParticle;
    private Animator anim;

    public void Damage(float amount)
    {
        Debug.Log(amount + "damage taken");
        Instantiate(hitParticle,transform.position, Quaternion.Euler(0.0f,0.0f,Random.Range(0.0f,360f)));
        anim.SetTrigger("damage");
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
   
}
