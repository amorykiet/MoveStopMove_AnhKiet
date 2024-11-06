using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGroupMaterial : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer groupSkin;

    public void SetMat(Material mat)
    {
        groupSkin.material = mat;
    }
}
