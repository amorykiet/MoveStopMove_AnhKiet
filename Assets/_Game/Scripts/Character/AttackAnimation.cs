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
        owner.attacking = true;
    }

    public void ResetAttack()
    {
        owner.ResetAttack();
        owner.attacking = false;
    }

}
