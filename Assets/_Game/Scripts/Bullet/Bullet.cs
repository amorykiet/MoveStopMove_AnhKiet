using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : GameUnit
{
    public Character owner;

    [SerializeField] private BulletConfig config;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject model;

    private float speed;
    private float rotSpeed;
    private float timeToExit;
    private float timeCount;


    private void Update()
    {

        if (timeCount < timeToExit)
        {
            timeCount += Time.deltaTime;
        }
        else
        {
            OnDespawn(0);
        }
    }

    private void FixedUpdate()
    {
        model.transform.Rotate(Vector3.forward * (rotSpeed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constants.BOT_TAG) || collision.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            CollideCharacter(collision);
        }
        else
        {
            OnDespawn(0);

        }
       
    }

    public void SetOwner(Character owner)
    {
        this.owner = owner;
    }

    public void OnInit(float scale, Character owner, float speed)
    {
        rotSpeed = config.rotSpeed;
        timeToExit = config.timeToExit;

        this.speed = speed;
        SetOwner(owner);

        transform.localScale = Vector3.one * scale;
        rb.velocity = transform.forward * speed;

        timeCount = 0;
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

}
