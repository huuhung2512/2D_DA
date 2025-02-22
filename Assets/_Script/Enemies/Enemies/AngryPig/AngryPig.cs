using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class AngryPig : Entity
{
    public AG_IdleState idleState {  get; private set; }    
    public AG_MoveState moveState { get; private set; }
    public AG_ChargeState chargeState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_MoveState moveStateData;

    public override void Awake()
    {
        base.Awake();
        moveState = new AG_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new AG_IdleState(this, stateMachine, "idle", idleStateData, this);
        chargeState = new AG_ChargeState(this, stateMachine, "charge", chargeStateData, this);
    }
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Kiểm tra nếu Player chạm vào hitbox
        {
            Debug.Log("cham vao player");
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            IKnockBackable knockback = collision.gameObject.GetComponent<IKnockBackable>();
            if (damageable != null)
            {
                knockback.KnockBack(new Vector2(1, 2), 20, Movement.FacingDirection);
                damageable.Damage(20);
                Destroy(gameObject);
            }
        }
        
    }
    private void Start()
    {
        stateMachine.Initialize(moveState);

    }
}
