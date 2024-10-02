using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material deactiveMaterial;

    private List<GameObject> list = new List<GameObject>();
    

    private void OnTriggerEnter(Collider other)
    {
        list.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        list.Remove(other.gameObject);
    }

    private void Update()
    {
        if (list.Count == 0)
        {
            if (mesh.material != activeMaterial)
            {
                mesh.material = activeMaterial;
            }
        }
        else
        {
            if (mesh.material != deactiveMaterial)
            {
                mesh.material = deactiveMaterial;
            }

        }
    }

}
