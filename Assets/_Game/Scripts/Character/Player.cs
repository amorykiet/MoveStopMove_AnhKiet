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
    private bool stoped;
    private bool attacking;
    private bool isMouseUp;
    private Bot currentTarget;

    //TEST HERE
    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        ChangeDragOnGround();
        
        UpdateTarget();

        if (Input.GetMouseButtonUp(0))
        {
            OnMouseButtonUp();
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnMouseButtonDown();
        }



        
    }

    private void FixedUpdate()
    {
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
        stoped = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (currentTarget != null)
        {
            PreAttack(currentTarget);
        }
        else
        {
            animator.SetBool(Constants.IS_IDLE, true);
        }

    }

    private void OnMouseButtonDown()
    {
        isMouseUp = false;
        if (!attacking)
        {
            ResetAttack();
        }

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

    private void PreAttack(Bot target)
    {
        transform.forward = Vector3.ProjectOnPlane((target.TF.position - transform.position), Vector3.up).normalized;
        animator.SetBool(Constants.IS_ATTACK, true);
        animator.SetBool(Constants.IS_IDLE, true);
        Invoke(nameof(Attack), 0.2f);
    }

    private void Attack()
    {
        if (!isMouseUp) { return;}
        if (attacking) { return; }
        attacking = true;
        currentWeapon.Fire(modelScale, attackSpeed);
        currentWeapon.Hide();
        Invoke(nameof(ResetAttack), 0.65f);

    }

    private void ResetAttack()
    {
        animator.SetBool(Constants.IS_ATTACK, false);
        currentWeapon.Show();
        stoped = false;
        attacking = false;
    }

    private void Move()
    {
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

    public override void OnDead()
    {
        stoped = true;
        rb.velocity = Vector3.zero;
        base.OnDead();
    }

    public override void OnInit()
    {
        base.OnInit();
        SetupWeapon();
        stoped = false;
        attacking = false;
        isMouseUp = true;
        eulerDirection = 0;
}

    public override void AddCharacterInRange(Character chr)
    {
        base.AddCharacterInRange(chr); 

        if (!attacking && isMouseUp && charactersInRange.Count == 1)
        {
            stoped = true;
            ChangeTarget(chr as Bot);
            PreAttack(currentTarget);
        }
    }
}
