using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material deactiveMaterial;

    private bool showing = true;

    public void Show()
    {
        if (!showing)
        {
            mesh.material = activeMaterial;
            showing = true;
        }
    }

    public void HideInXRay()
    {
        if (showing)
        {
            mesh.material = deactiveMaterial;
            showing = false;
        }
    }
 
}
