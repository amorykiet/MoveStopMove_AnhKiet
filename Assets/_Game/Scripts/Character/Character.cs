using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public event Action OnCharacterDead;

    [SerializeField] protected CharacterConfig config;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Transform handPos;
    [SerializeField] protected Weapon weaponPref;
    [SerializeField] protected Transform bulletSpawnPos;
    [SerializeField] protected GameObject attackSphere;
    [SerializeField] protected Animator animator;

    protected Transform tf;
    protected Weapon currentWeapon;
    protected List<Character> charactersInRange = new();
    protected float radiusAttack;


    public Transform TF
    {
        get
        {
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }

    private void Start()
    {
        OnInit();

    }

    protected void SetupWeapon()
    {
        currentWeapon = Instantiate(weaponPref, handPos);
        currentWeapon.SetOwner(this);
        currentWeapon.SetBulletSpawnPos(bulletSpawnPos);
    }

    public virtual void AddCharacterInRange(Character chr)
    {
        charactersInRange.Add(chr);
    }

    public virtual void RemoveCharacterOutRange(Character chr)
    {
        charactersInRange.Remove(chr);
    }

    public virtual void LevelUp()
    {
        if (radiusAttack < 10.0f)
        {
            radiusAttack = radiusAttack + 0.5f;
        }
        attackSphere.transform.localScale = Vector3.one * radiusAttack;
    }

    public virtual void OnDead()
    {
        animator.SetBool("IsDead", true);
        rb.useGravity = false;
        rb.detectCollisions = false;
        Invoke(nameof(OnDespawn), 1.0f);
    }

    public virtual void OnDespawn()
    {
        GameObject.Destroy(gameObject);
    }

    public virtual void OnInit()
    {
        radiusAttack = config.radiusAttackStart;
    }
}
