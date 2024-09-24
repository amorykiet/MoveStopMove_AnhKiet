using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : BaseState<Bot>
{

    public override void OnEnter(Bot owner)
    {
        owner.animator.SetBool(Constants.IS_IDLE, false);
        owner.RandomGo();
    }

    public override void OnExcute(Bot owner)
    {
        if (owner.agent.remainingDistance < 0.001f)
        {
            owner.ChangeState(new IdleState());
        }
    }

    public override void OnExit(Bot owner)
    {
        owner.animator.SetBool(Constants.IS_IDLE, true);
    }
}
