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
    [SerializeField] private float speed;


    private float eulerDirection = 0;
    private RaycastHit standingHit;
    private bool grounded;
    private Vector3 direction;
    private bool stoped = false;
    private bool attacking = false;
    private bool readyToAttack = false;
    private Bot currentTarget;

    //TEST HERE
    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        ChangeDragOnGround();
        if (charactersInRange.Count > 0)
        {
            Bot lastTarget = GetNewTarget();

            if (currentTarget != lastTarget)
            {
                ChangeTargetTo(lastTarget);
            }

        }
        else
        {
            if (currentTarget != null)
            {
                ChangeTargetTo(null);
            }
        }
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
        direction = Vector3.zero;
        if (!stoped)
        {
            Move();
        }

    }

    private Bot GetNewTarget()
    {
        Bot target = charactersInRange.OrderBy(o => Vector3.Distance(TF.position, o.TF.position)).First() as Bot;
        return target;
    }

    private void ChangeTargetTo(Bot lastTarget)
    {
        if(lastTarget == null)
        {
            currentTarget?.IsTargeted(false);
            currentTarget = null;
            return;

        }

        if(currentTarget != null)
        {
            currentTarget.IsTargeted(false);
        }

        currentTarget = lastTarget;
        currentTarget.IsTargeted(true);
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
        readyToAttack = true;
        stoped = true;
        rb.velocity = Vector3.zero;
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
        readyToAttack = false;
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
        transform.forward = target.TF.position - transform.position;
        animator.SetBool(Constants.IS_ATTACK, true);
        animator.SetBool(Constants.IS_IDLE, true);
        Invoke(nameof(Attack), 0.2f);
    }

    private void Attack()
    {
        if (!readyToAttack) { return;}
        if (attacking) { return; }
        attacking = true;
        currentWeapon.FireOnScale(modelScale);
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

    override public void OnInit()
    {
        base.OnInit();
        SetupWeapon();
    }
}
