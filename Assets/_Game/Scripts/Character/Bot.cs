using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] private GameObject targetCircle;
    [SerializeField] private float walkRadius = 10;
    
    private BaseState<Bot> currentState;

    public NavMeshAgent agent;

    public void SetCircleTarget(bool targeted)
    {
        targetCircle.SetActive(targeted);
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExcute(this);
        }
    }

    protected override void SetupWeapon()
    {
        Weapon weapon = ItemManager.Ins.GetWeaponPrefRandom();
        currentWeapon = Instantiate(weapon, handPos);
        base.SetupWeapon();
    }

    override public void OnInit()
    {
        base.OnInit();
        agent.enabled = true;
        ChangeState(new IdleState());
        SetupWeapon();
    }

    public override void OnDead()
    {
        agent.isStopped = true;
        base.OnDead();
    }

    public void ChangeState(BaseState<Bot> newState)
    {
        if (currentState != null) currentState.OnExit(this);
        currentState = newState;
        currentState.OnEnter(this);
    }

    public void PreAttack(Character target)
    {
        transform.forward = target.TF.position - transform.position;
        animator.SetBool(Constants.IS_ATTACK, true);
        animator.SetBool(Constants.IS_IDLE, true);

        Invoke(nameof(Attack), 0.2f);
    }

    public void Attack()
    {
        currentWeapon.Fire(modelScale, attackSpeed);
        currentWeapon.Hide();
        Invoke(nameof(ResetAttack), 0.65f);

    }

    public void ResetAttack()
    {
        animator.SetBool(Constants.IS_ATTACK, false);
        currentWeapon.Show();
        ChangeState(new IdleState());
    }
    

    public void RandomGo()
    {
        Vector3 randomPoint = Random.insideUnitSphere * walkRadius;
        randomPoint += transform.position;

        NavMeshHit hit;
        if( NavMesh.SamplePosition(randomPoint, out hit, walkRadius/2.0f, 1))
        {
            agent.SetDestination(hit.position);
        }
        else
        {
            ChangeState(new IdleState());
        }
    }
}
