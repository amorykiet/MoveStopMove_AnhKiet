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
        if (other.CompareTag(Constants.BOT_TAG))
        {
            Character chr = other.gameObject.GetComponent<Bot>();
            owner.RemoveCharacterOutRange(chr);
        }
        else if (other.CompareTag(Constants.PLAYER_TAG))
        {
            Character chr = other.GetComponent<Player>();
            owner.RemoveCharacterOutRange(chr);
        }
    }

    private void HideObstacle(Collider other)
    {
        other.gameObject.GetComponent<Obstacle>().HideInXRay();
    }
    private void ShowObstacle(Collider other)
    {
        other.gameObject.GetComponent<Obstacle>().Show();
    }


    private void OnTriggerEnter(Collider other)
    {
        AddCharacter(other);

        if (owner.CompareTag(Constants.PLAYER_TAG) && other.CompareTag(Constants.OBSTACLE_TAG))
        {
            HideObstacle(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveCharacter(other);
        if (owner.CompareTag(Constants.PLAYER_TAG) && other.CompareTag(Constants.OBSTACLE_TAG))
        {
            ShowObstacle(other);
        }
    }

}
