using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState<Bot>
{
    private const float MAX_TIME_ATTACK = 2;
    private float timeToAttack;
    private float timeCouter;

    public override void OnEnter(Bot owner)
    {
        owner.animator.SetBool(Constants.IS_IDLE, true);
        timeCouter = 0;
        timeToAttack = Random.Range(0, MAX_TIME_ATTACK);
    }

    public override void OnExcute(Bot owner)
    {
        if(timeCouter < timeToAttack)
        {
            timeCouter += Time.deltaTime;
        }
        else
        {
            if (owner.charactersInRange.Count > 0)
            {
                owner.ChangeState(new AttackState());
            }
            else
            {
                owner.ChangeState(new PatrolState());
            }
        }
    }

    public override void OnExit(Bot owner)
    {
        
    }
}
