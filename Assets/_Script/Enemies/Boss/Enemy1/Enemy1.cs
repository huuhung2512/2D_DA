using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set; }
    public E1_ChargeState chargeState { get; private set; }
    public E1_LookForPlayerState lookForPlayerState { get; private set; }
    public E1_MeleeAttackState meleeAttackState { get; private set; }
    public E1_StunState stunState { get; private set; }
    public E1_DeadState deadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttackState meleeAttackStateData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private Transform meleeAttackPossition;
    public override void Awake()
    {
        base.Awake();
        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new E1_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new E1_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new E1_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPossition, meleeAttackStateData, this);
        stunState = new E1_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new E1_DeadState(this, stateMachine, "dead", deadStateData, this);

        stats.Poise.OnCurrentValueZero += HandlePoiseZero;
    }

    private void HandlePoiseZero()
    {
        stateMachine.ChangeState(stunState);
    }

    private void Start()
    {
        stateMachine.Initialize(moveState);
    }

    private void OnDestroy()
    {
        stats.Poise.OnCurrentValueZero -= HandlePoiseZero;
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPossition.position, meleeAttackStateData.attackRadius);
    }
}
