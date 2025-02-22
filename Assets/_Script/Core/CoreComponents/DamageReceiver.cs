using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageReceiver : CoreComponent, IDamageable
{
    [SerializeField] private GameObject damageParticle;
    [SerializeField] private GameObject textDamage;
    private Stats stats;
    private ParticleManager particleManager;
    private Animator anim;


    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + " Damage!!");
        stats.Health.Decrease(amount);
        particleManager.StartParticleWithRandomRotation(damageParticle);

        if (textDamage)
        {
            ShowFloatingText(amount);
        }
        //anim.SetTrigger("damage");
    }

    void ShowFloatingText(float amount)
    {
        GameObject go = Instantiate(textDamage, transform.position, Quaternion.identity);
        go.GetComponent<TextMeshPro>().text = "-" + amount.ToString();
    }

    protected override void Awake()
    {
        base.Awake();

        stats = core.GetCoreComponent<Stats>();
        particleManager = core.GetCoreComponent<ParticleManager>();
        anim = GetComponentInParent<Animator>();
    }
}
