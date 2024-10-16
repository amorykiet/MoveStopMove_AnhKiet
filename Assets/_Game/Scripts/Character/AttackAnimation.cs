using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    [SerializeField] Player owner;

    public void Attack()
    {
        if (!owner.isMouseUp)
        {
            owner.ResetAttack();
            return;
        }
        owner.stoped = true;
        owner.attacking = true;
        owner.currentWeapon.Fire(owner.modelScale, owner.attackSpeed);
        owner.currentWeapon.Hide();
    }

    public void ResetAttack()
    {
        owner.ResetAttack();
        owner.attacking = false;
        owner.stoped = false;
    }

}
