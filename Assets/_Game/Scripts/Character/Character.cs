using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public abstract class Character : MonoBehaviour
{
    public static event Action<Character> OnCharacterDead;

    public UICharacter charUI;
    public List<Character> charactersInRange = new();
    public Animator animator;
    public Weapon currentWeapon;
    public Hat currentHat;
    public Pant currentPant;
    public float modelScale;
    public float attackSpeed;
    public int score;

    [SerializeField] protected CharacterConfig config;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Transform handPos;
    [SerializeField] protected Transform headPos;
    [SerializeField] protected CharacterPantsMaterial pantMat;
    [SerializeField] protected Transform bulletSpawnPos;
    [SerializeField] protected GameObject attackSphere;
    [SerializeField] protected GameObject model;

    protected Transform tf;
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
    protected float radiusAttack;
    protected float speed;


    public virtual void SetupWeapon()
    {
        currentWeapon.SetOwner(this);
        currentWeapon.SetBulletSpawnPos(bulletSpawnPos);
    }

    public virtual void SetupHat()
    { 
        Hat hatPref = UserDataManager.Ins.GetCurrentHat();
        if (currentHat != null)
        {
            Destroy(currentHat.gameObject);
        }
        if (hatPref.type == HatType.None) return;
        currentHat = Instantiate(hatPref, headPos);
    }

    public virtual void SetupPant()
    {
        Pant _pant= UserDataManager.Ins.GetCurrentPant();
        if (_pant.type == PantsType.None)
        {
            currentPant = ItemManager.Ins.GetPantMatByType(PantsType.None);
        }
        currentPant = _pant;
        pantMat.SetMat(currentPant.material);
    }

    public virtual void SetupWeapon(Weapon weaponPref)
    {
        currentWeapon.SetOwner(this);
        currentWeapon.SetBulletSpawnPos(bulletSpawnPos);
    }

    public virtual void SetupHat(Hat hatPref)
    {
        if (hatPref.type == HatType.None) return;
        if (currentHat != null)
        {
            Destroy(currentHat.gameObject);
        }
        currentHat = Instantiate(hatPref, headPos);
    }

    public virtual void SetupPant(Pant _pant)
    {
        if (_pant.type == PantsType.None) return;

        currentPant = _pant;
        pantMat.SetMat(currentPant.material);
    }

    public virtual void ClearHat()
    {
        if (currentHat != null)
        {
            Destroy(currentHat.gameObject);
        }
    }

    public virtual void ClearPant()
    {
        Pant none = ItemManager.Ins.GetPantMatByType(PantsType.None);
        pantMat.SetMat(none.material);
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

    public void AssignName(string name)
    {
        charUI.NameText.text = name;
    }

    public Character GetLatestTarget()
    {
        Character target = charactersInRange.OrderBy(o => Vector3.Distance(TF.position, o.TF.position)).First();
        return target;
    }


}
