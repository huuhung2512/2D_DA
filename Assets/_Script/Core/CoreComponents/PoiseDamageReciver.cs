using Hung.Combat.PoiseDamage;
using Hung.ModifierSystem;
using UnityEngine;
namespace Hung.CoreSystem
{
    public class PoiseDamageReciver : CoreComponent, IPoiseDamageable
    {
        private Stats stats;

        public Modifiers<Modifier<PoiseDamageData>, PoiseDamageData> Modifiers { get; } = new();
        public void DamagePoise(PoiseDamageData data)
        {
            data = Modifiers.ApplyAllModifiers(data);

            stats.Poise.Decrease(data.Amount);
        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<Stats>();
        }
    }
}

