using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    [SerializeField] Player owner;

    public void Attack()
    {
        owner.currentWeapon.Fire(owner.modelScale, owner.attackSpeed);
        owner.currentWeapon.Hide();
    }

    public void ResetAttack()
    {
        owner.animator.SetBool(Constants.IS_ATTACK, false);
        owner.currentWeapon.Show();
        owner.stoped = false;
        //attacking = false;
    }

}
