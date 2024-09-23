using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereAttackRange : MonoBehaviour
{
    [SerializeField] private Character owner;

    private void AddCharacter(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
            Character chr = other.gameObject.GetComponent<Bot>();
            owner.AddCharacterInRange(chr);
        }
        else if (other.CompareTag("Player"))
        {
            Character chr = other.GetComponent<Player>();
            owner.AddCharacterInRange(chr);
        }
    }

    private void RemoveCharacter(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
            Character chr = other.gameObject.GetComponent<Bot>();
            owner.RemoveCharacterOutRange(chr);
        }
        else if (other.CompareTag("Player"))
        {
            Character chr = other.GetComponent<Player>();
            owner.RemoveCharacterOutRange(chr);
        }
    }

    private void RemoveBullet(Collider other)
    {

        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();

            if (bullet.owner == owner)
            {
                bullet.OnDespawn();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        AddCharacter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveCharacter(other);
    }

}
