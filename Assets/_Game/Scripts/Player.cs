using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Player : Character
{
    //In Player
    public DynamicJoystick joyStick;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;


    private float eulerDirection = 0;
    private RaycastHit standingHit;
    private bool grounded;
    private Vector3 direction;


    private void Update()
    {

        Debug.DrawLine(TF.position, TF.position + Vector3.down * 0.3f, Color.red);
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

    private void FixedUpdate()
    {
        Move();
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

    override protected void Move()
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
            animator.SetBool("IsIdle", false);
            eulerDirection = Vector2.SignedAngle(joyStick.Direction, Vector2.up);
        }
        else
        {
            animator.SetBool("IsIdle", true);
        }

        TF.rotation = Quaternion.Euler(0, eulerDirection, 0);

    }



}
