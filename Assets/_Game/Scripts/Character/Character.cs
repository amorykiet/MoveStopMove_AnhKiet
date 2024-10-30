using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public abstract class Character : MonoBehaviour
{
    public static event Action<Character> OnCharacterDead;

    [SerializeField] protected CharacterConfig config;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Transform handPos;
    [SerializeField] protected Transform headPos;
    [SerializeField] protected CharacterPantsMaterial pant;
    [SerializeField] protected Transform bulletSpawnPos;
    [SerializeField] protected GameObject attackSphere;
    [SerializeField] protected GameObject model;
    [SerializeField] protected UICharacter charUI;

    protected Transform tf;
    protected float radiusAttack;
    protected float speed;
    

    public List<Character> charactersInRange = new();
    public Animator animator;
    public Weapon currentWeapon;
    public Hat currentHat;
    public Pant currentPant;
    public float modelScale;
    public float attackSpeed;
    public int score;

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

    protected virtual void SetupWeapon()
    {
        currentWeapon.SetOwner(this);
        currentWeapon.SetBulletSpawnPos(bulletSpawnPos);
    }

    protected virtual void SetupHat()
    { 
        Hat hatPref = UserDataManager.Ins.GetCurrentHat();
        if (hatPref.type == HatType.None) return;
        currentHat = Instantiate(hatPref, headPos);
    }
    protected virtual void SetupPant()
    {
        Pant _pant= UserDataManager.Ins.GetCurrentPant();
        if (_pant.type == PantsType.None) return;
        currentPant = _pant;
        pant.SetMat(currentPant.material);
    }

    public void AssignName(string name)
    {
        charUI.NameText.text = name;
    }

    public Character GetLatestTarget()
    {
        Character target = charactersInRange.OrderBy(o => Vector3.Distance(TF.position, o.TF.position)).First();
        return target;
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
            modelScale = modelScale + 0.1f;
        }
        attackSphere.transform.localScale = Vector3.one * radiusAttack;
        model.transform.localScale = Vector3.one * modelScale;

        score += 1;
        charUI.ScoreText.text = score.ToString();
    }

    public virtual void OnDead()
    {
        OnCharacterDead?.Invoke(this);
        animator.SetBool(Constants.IS_DEAD, true);
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
        modelScale = config.modelScaleStart;
        attackSpeed = config.attackSpeed;
        speed = config.speed;
        attackSphere.transform.localScale = Vector3.one * radiusAttack;
        model.transform.localScale = Vector3.one * modelScale;

        score = 0;
        charUI.OnInit();
    }


}
