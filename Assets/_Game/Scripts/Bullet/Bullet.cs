using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        //model.transform.rotation = Quaternion.Euler(new Vector3(0, model.transform.rotation.y + rotSpeed * Time.deltaTime, 0));
        model.transform.Rotate(Vector3.forward, rotSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        //TEST
        if (collision.gameObject.CompareTag("Bot") || collision.gameObject.CompareTag("Player"))
        {
            CollideCharacter(collision);
        }
       
    }

    private void CollideCharacter(Collision collision)
    {
        Character target = collision.gameObject.GetComponent<Character>();
        OnDespawn();
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

    public void OnDespawn()
    {
        Object.Destroy(this.gameObject);
    }

    public void OnInit(float scale, Character owner, float speed)
    {
        this.speed = speed;
        rotSpeed = config.rotSpeed;
        timeToExit = config.timeToExit;
        transform.localScale = Vector3.one * scale;
        SetOwner(owner);

        rb.velocity = transform.forward * speed;
        Invoke(nameof(OnDespawn), timeToExit);
    }

}
