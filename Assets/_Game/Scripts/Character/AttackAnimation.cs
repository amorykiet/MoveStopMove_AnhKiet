using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    [SerializeField] private Player owner;

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
        SoundManager.Ins.OnThrowWeapon();
        owner.currentWeapon.Hide();
    }

    public void ResetAttack()
    {
        owner.ResetAttack();
        owner.attacking = false;
        owner.stoped = false;
    }

}
