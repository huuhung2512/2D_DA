using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnockBackReceiver : CoreComponent, IKnockBackable
{
    
    [SerializeField] private float maxKnockBackTime = 0.2f;

    public bool isKnockBackActive;
    private float knockBackStartTime;

    private CoreComp<Movement> movement;
    private CoreComp<CollisionSenses> collisionSeses;

    public override void LogicUpdate()
    {
        CheckKnockBack();
    }
    
    public void KnockBack(Vector2 angle, float strength, int direction)
    {
        movement.Comp?.SetVelocity(strength, angle, direction);
        movement.Comp.CanSetVelocity = false;
        isKnockBackActive = true;
        knockBackStartTime = Time.time;
    }
    private void CheckKnockBack()
    {
        if (isKnockBackActive && ((movement.Comp?.CurrentVelocity.y <= 0.01f && collisionSeses.Comp.Grounded) || Time.time >= knockBackStartTime + maxKnockBackTime ))
        {
            isKnockBackActive =false;
            movement.Comp.CanSetVelocity = true;
        }
    }
    protected override void Awake()
    {
        base.Awake();

        movement = new CoreComp<Movement>(core);
        collisionSeses = new CoreComp<CollisionSenses>(core);   
    }
}
