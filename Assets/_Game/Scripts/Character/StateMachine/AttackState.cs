using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState<Bot>
{
    public override void OnEnter(Bot owner)
    {
        Character target = owner.GetLatestTarget();
        owner.PreAttack(target);
    }

    public override void OnExcute(Bot owner)
    {

    }

    public override void OnExit(Bot owner)
    {
    }


}
