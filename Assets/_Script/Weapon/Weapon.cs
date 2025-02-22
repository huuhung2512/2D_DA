using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public event Action<bool> OnCurrentInputChange;

    [SerializeField] private float attackCounterResetCooldown;
    public WeaponDataSO Data { get; private set; }
    public int CurrentAttackCounter
    {
        get => currentAttackCounter;
        private set
        {
            if (value >= Data.NumberOfAttacks)
            {
                currentAttackCounter = 0;
            }
            else
            {
                currentAttackCounter = value;
            }
        }
    }

    public bool CurrentInput
    {
        get => currentIput;
        set
        {
            if (currentIput != value)
            {
                currentIput = value;
                OnCurrentInputChange?.Invoke(currentIput);
            }
        }
    }

    public event Action OnEnter;

    public event Action OnExit;

    private Animator anim;
    public GameObject BaseGameObject { get; private set; }
    public GameObject WeaponSpriteGameObject { get; private set; }
    public AnimationEventHandler EventHandler { get; private set; }
    public Core Core { get; private set; }

    private int currentAttackCounter;

    private Timer attackCounterResetTimer;

    private bool currentIput;
    public void Enter()
    {
        print($"{transform.name} enter");
        anim.SetBool("active", true);

        attackCounterResetTimer.StopTimer();

        anim.SetInteger("counter", CurrentAttackCounter);

        OnEnter?.Invoke();
    }

    public void SetCore(Core core)
    {
        Core = core;
    }

    public void SetData(WeaponDataSO data)
    {
        Data = data;
    }

    private void Exit()
    {
        anim.SetBool("active", false);

        CurrentAttackCounter++;

        attackCounterResetTimer.StartTimer();

        OnExit?.Invoke();
    }
    private void Awake()
    {
        BaseGameObject = transform.Find("Base").gameObject;
        WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;

        anim = BaseGameObject.GetComponent<Animator>();

        EventHandler = BaseGameObject.AddComponent<AnimationEventHandler>();

        attackCounterResetTimer = new Timer(attackCounterResetCooldown);
    }

    private void Update()
    {
        attackCounterResetTimer.Tick();
    }
    private void ResetAttackCounter() => CurrentAttackCounter = 0;

    private void OnEnable()
    {
        EventHandler.OnFinish += Exit;
        attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
    }

    private void OnDisable()
    {
        EventHandler.OnFinish -= Exit;
        attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;
    }
}
