using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    public DynamicJoystick joyStick;
    public bool isMouseUp;
    public bool stoped;
    public bool attacking;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private CharacterGroupMaterial groupMat;
    [SerializeField] private Transform backPos;
    [SerializeField] private Transform tailPos;
    [SerializeField] private Transform otherHandPos;

    private float eulerDirection;
    private RaycastHit standingHit;
    private bool grounded;
    private Vector3 direction;
    private Bot currentTarget;
    private bool dead;

    private List<GameObject> fullSetObjList = new();

    private void Update()
    {
        if (dead)
        {
            return;
        }
        ChangeDragOnGround();
        
        UpdateTarget();

        if (Input.GetMouseButtonUp(0))
        {
            OnMouseButtonUp();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            OnMouseButtonDown();
        }



        
    }

    private void FixedUpdate()
    {
        if (dead)
        {
            return;
        }
        if (!stoped)
        {
            Move();
        }

    }

    public override void OnDead()
    {
        SoundManager.Ins.OnDead();
        stoped = true;
        dead = true;
        rb.velocity = Vector3.zero;
        ChangeTarget(null);
        base.OnDead();
    }

    public override void OnInit()
    {
        base.OnInit();
        SetupWeapon();
        SetupHat();
        SetupPant();
        stoped = true;
        dead = false;
        eulerDirection = 0;
    }

    public override void AddCharacterInRange(Character chr)
    {
        base.AddCharacterInRange(chr);

        if (isMouseUp && charactersInRange.Count == 1 && !attacking)
        {
            stoped = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            ChangeTarget(chr as Bot);
            Attack(currentTarget);
        }
    }

    public override void LevelUp()
    {
        if (dead) return;
        SoundManager.Ins.OnLevelUp();
        base.LevelUp();
    }

    public override void SetupWeapon()
    {
        Weapon weaponPref = UserDataManager.Ins.GetCurrentWeapon();
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = Instantiate(weaponPref, handPos);
        base.SetupWeapon();
    }

    public override void SetupWeapon(Weapon weaponPref)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = Instantiate(weaponPref, handPos);
        base.SetupWeapon();
    }

    public void SetupFullSet(FullSet fullSet)
    {
        ClearFullSet();
        if (fullSet.groupMaterial != null)
        {
            groupMat.SetMat(fullSet.groupMaterial);
        }

        if (fullSet.pantMaterial != null)
        {
            pantMat.SetMat(fullSet.pantMaterial);
        }

        GameObject temp = null;

        if (fullSet.hatPref != null)
        {
            temp = Instantiate(fullSet.hatPref, headPos);
            fullSetObjList.Add(temp);
        }

        if (fullSet.weaponPref != null)
        {
            temp = Instantiate(fullSet.weaponPref, otherHandPos);
            fullSetObjList.Add(temp);
        }

        if (fullSet.wingPref != null)
        {
            temp = Instantiate(fullSet.wingPref, backPos);
            fullSetObjList.Add(temp);
        }

        if (fullSet.tailPref != null)
        {
            temp = Instantiate(fullSet.tailPref, tailPos);
            fullSetObjList.Add(temp);
        }
    }

    public void SetupFullSet()
    {
        SetupFullSet(UserDataManager.Ins.GetCurrentFullSet());
    }

    public void ClearFullSet()
    {
        FullSet none = ItemManager.Ins.GetFullSetByType(FullSetType.None);
        groupMat.SetMat(none.groupMaterial);
        pantMat.SetMat(none.pantMaterial);

        foreach (GameObject obj in fullSetObjList)
        {
            Destroy(obj.gameObject);
        }
        fullSetObjList.Clear();
    }

    public void AttachCam(CameraFollow cam)
    {
        charUI.cam = cam;
    }

    public void ResetAttack()
    {
        currentWeapon.Show();
        animator.SetBool(Constants.IS_ATTACK, false);
        stoped = false;
    }

    public void Wining()
    {
        dead = true;
        stoped = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.detectCollisions = false;
        animator.SetBool(Constants.IS_WIN, true);
    }

    public void ConfigAttackSphereOnPreviewing()
    {
        attackSphere.transform.localScale = Vector3.one * 100;
    }

    public void ConfigAttackSphereOnPlaying()
    {
        attackSphere.transform.localScale = Vector3.one * radiusAttack;
    }

    private void UpdateTarget()
    {
        if (charactersInRange.Count > 0)
        {
            Bot lastTarget = GetLatestTarget() as Bot;

            if (currentTarget != lastTarget)
            {
                ChangeTarget(lastTarget);
            }

        }
        else
        {
            if (currentTarget != null)
            {
                ChangeTarget(null);
            }
        }
    }

    private void ChangeTarget(Bot lastTarget)
    {
        if (currentTarget != null)
        {
            currentTarget.SetCircleTarget(false);
        }

        currentTarget = lastTarget;

        if (currentTarget != null)
        {
            currentTarget.SetCircleTarget(true);
        }
    }

    private void ChangeDragOnGround()
    {
        grounded = Physics.Raycast(TF.position, Vector3.down, 0.3f, groundMask);

        if (grounded)
        {
            rb.drag = 5f;
        }
        else
        {
            rb.drag = 0f;
        }

    }

    private void OnMouseButtonUp()
    {
        isMouseUp = true;
        if (attacking)
        {
            return;
        }
        stoped = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (currentTarget != null)
        {
            Attack(currentTarget);
        }
        else
        {
            animator.SetBool(Constants.IS_IDLE, true);
        }

    }

    private void OnMouseButtonDown()
    {
        isMouseUp = false;
        if (attacking)
        {
            return;
        }
        ResetAttack();

    }

    private bool IsOnSlope()
    {
        if (Physics.Raycast(TF.position, Vector3.down, out standingHit, 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, standingHit.normal);
            if (angle > 0.0001f && angle < 50.0f)
            {
                return true;
            }
        }
        return false;
    }

    private void Attack(Bot target)
    {
        transform.forward = Vector3.ProjectOnPlane((target.TF.position - transform.position), Vector3.up).normalized;
        animator.SetBool(Constants.IS_ATTACK, true);
    }

    private void Move()
    {
        if (joyStick == null) return;
        //Movement
        direction = new Vector3(joyStick.Direction.x, 0, joyStick.Direction.y).normalized;
        if (grounded)
        {
            if (IsOnSlope())
            {
                direction = Vector3.ProjectOnPlane(direction, standingHit.normal).normalized;
            }
            rb.AddForce(direction * speed, ForceMode.Force);

        }

        //Rotation
        if (direction.magnitude > 0.001f)
        {
            animator.SetBool(Constants.IS_IDLE, false);
            eulerDirection = Vector2.SignedAngle(joyStick.Direction, Vector2.up);
        }
        else
        {
            animator.SetBool(Constants.IS_IDLE, true);
        }

        TF.rotation = Quaternion.Euler(0, eulerDirection, 0);

    }

}
