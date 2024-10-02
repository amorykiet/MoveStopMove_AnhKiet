using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material deactiveMaterial;

    public void Show()
    {
        mesh.material = activeMaterial;
    }

    public void HideInXRay()
    {
        mesh.material = deactiveMaterial;
    }
 
}
