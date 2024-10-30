using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    //In Player
    public DynamicJoystick joyStick;

    [SerializeField] private LayerMask groundMask;

    private float eulerDirection;
    private RaycastHit standingHit;
    private bool grounded;
    private Vector3 direction;
    private Bot currentTarget;
    private bool dead;

    public bool isMouseUp;
    public bool stoped;
    public bool attacking;



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

    override public void OnDead()
    {
        stoped = true;
        dead = true;
        rb.velocity = Vector3.zero;
        ChangeTarget(null);
        base.OnDead();
    }

    override public void OnInit()
    {
        base.OnInit();
        SetupWeapon();
        SetupHat();
        SetupPant();
        stoped = true;
        dead = false;
        eulerDirection = 0;
    }

    override public void AddCharacterInRange(Character chr)
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
        if(dead) return;
        base.LevelUp();
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

    override public void SetupWeapon()
    {
        Weapon weaponPref = UserDataManager.Ins.GetCurrentWeapon();
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = Instantiate(weaponPref, handPos);
        base.SetupWeapon();
    }

    
}
