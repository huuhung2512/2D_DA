using Hung.Combat.KnockBack;
using Hung.ModifierSystem;
using UnityEngine;
namespace Hung.CoreSystem
{
    public class KnockBackReceiver : CoreComponent, IKnockBackable
    {
        public Modifiers<Modifier<KnockBackData>, KnockBackData> Modifiers { get; } = new();
        [SerializeField] private float maxKnockBackTime = 0.2f;

        public bool isKnockBackActive;
        private float knockBackStartTime;

        private Movement movement;
        private CollisionSenses collisionSenses;

        public override void LogicUpdate()
        {
            CheckKnockBack();
        }

        public void KnockBack(KnockBackData data)
        {
            data = Modifiers.ApplyAllModifiers(data);

            movement.SetVelocity(data.Strength, data.Angle, data.Direction);
            movement.CanSetVelocity = false;
            isKnockBackActive = true;
            knockBackStartTime = Time.time;
        }
        private void CheckKnockBack()
        {
            if (isKnockBackActive && ((movement.CurrentVelocity.y <= 0.01f && collisionSenses.Grounded) || Time.time >= knockBackStartTime + maxKnockBackTime))
            {
                isKnockBackActive = false;
                movement.CanSetVelocity = true;
            }
        }
        protected override void Awake()
        {
            base.Awake();

            movement = core.GetCoreComponent<Movement>();
            collisionSenses = core.GetCoreComponent<CollisionSenses>();
        }
    }
}

