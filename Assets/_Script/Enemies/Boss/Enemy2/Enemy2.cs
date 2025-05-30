using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_MoveState moveState { get; private set; }
    public E2_IdleState idleState { get; private set; }
    public E2_PlayerDetectedState playerDetectedState { get; private set; }
    public E2_MeleeAttackState meleeAttackState { get; private set; }
    public E2_LookForPlayerState lookForPlayerState { get; private set; } 
    public E2_StunState stunState { get; private set; }
    public E2_DeadState deadState { get; private set; }
    public E2_DodgeState dodgeState { get; private set; }
    public E2_RangedAttackState rangedAttackState { get; private set; }

    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_MeleeAttackState meleeAttackStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    public D_DodgeState dodgeStateData;
    [SerializeField]
    public D_RangedAttackState rangedAttackStateData;
    [SerializeField]
    private Transform meleeAttackPossition;
    [SerializeField]
    private Transform rangedAttackPosition;
    public override void Awake()
    {
        base.Awake();
        moveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E2_IdleState(this,stateMachine,"idle",idleStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(this,stateMachine,"playerDetected",playerDetectedData, this);
        meleeAttackState = new E2_MeleeAttackState(this,stateMachine,"meleeAttack",meleeAttackPossition,meleeAttackStateData, this);
        lookForPlayerState = new E2_LookForPlayerState(this,stateMachine,"lookForPlayer",lookForPlayerStateData, this);
        stunState = new E2_StunState(this,stateMachine,"stun",stunStateData, this);
        deadState = new E2_DeadState(this,stateMachine,"dead",deadStateData, this);
        dodgeState = new E2_DodgeState(this,stateMachine,"dodge",dodgeStateData, this);
        rangedAttackState = new E2_RangedAttackState(this,stateMachine,"rangedAttack",rangedAttackPosition,rangedAttackStateData, this);
        
        stats.Poise.OnCurrentValueZero += HandlePoiseZero;
    }

    //tao stun cho enemy
    private void HandlePoiseZero()
    {
        stateMachine.ChangeState(stunState);
    }

    private void OnDestroy()
    {
        stats.Poise.OnCurrentValueZero -= HandlePoiseZero;
    }
    private void Start()
    {
        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPossition.position, meleeAttackStateData.attackRadius);
    }
}
