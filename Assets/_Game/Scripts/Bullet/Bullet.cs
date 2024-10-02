using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField] BulletConfig config;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject model;
    private float speed = 2;
    private float rotSpeed = 100;
    private float timeToExit = 2.0f;


    public Character owner;

    private void FixedUpdate()
    {
        //model.transform.Rotate(Vector3.forward, rotSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * (rotSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bot") || collision.gameObject.CompareTag("Player"))
        {
            CollideCharacter(collision);
        }
        else
        {
            OnDespawn(0);

        }
       
    }

    private void CollideCharacter(Collision collision)
    {
        Character target = collision.gameObject.GetComponent<Character>();
        OnDespawn(0);
        if (owner  != null)
        {
            owner.LevelUp();
        }
        target.OnDead();

    }

    public void SetOwner(Character owner)
    {
        this.owner = owner;
    }

    public void OnInit(float scale, Character owner, float speed)
    {
        this.speed = speed;
        this.model.transform.rotation = config.rotation;
        rotSpeed = config.rotSpeed;
        timeToExit = config.timeToExit;
        transform.localScale = Vector3.one * scale;
        SetOwner(owner);

        rb.velocity = transform.forward * speed;
        OnDespawn(timeToExit);
    }

}
