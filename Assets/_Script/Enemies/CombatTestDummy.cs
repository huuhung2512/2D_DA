using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hung.Combat.Damage;
public class CombatTestDummy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject hitParticle;
    private Animator anim;

    public void Damage(DamageData data)
    {
        Debug.Log(data.Amount + " Damage taken");

        Instantiate(hitParticle, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        anim.SetTrigger("damage");
        Destroy(gameObject);
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
   
}
