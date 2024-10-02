using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereXRay : MonoBehaviour
{
    [SerializeField] private Obstacle owner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            owner.HideInXRay();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            owner.Show();
        }
    }
}
