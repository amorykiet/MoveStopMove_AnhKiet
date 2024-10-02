using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereXRay : MonoBehaviour
{
    [SerializeField] private Obstacle owner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            owner.HideInXRay();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            owner.Show();
        }
    }
}
