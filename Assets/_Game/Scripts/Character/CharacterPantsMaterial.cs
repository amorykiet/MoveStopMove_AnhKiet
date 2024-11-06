using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPantsMaterial : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer pantSkin;

    public void SetMat(Material mat)
    {
        pantSkin.material = mat;
    }
}
