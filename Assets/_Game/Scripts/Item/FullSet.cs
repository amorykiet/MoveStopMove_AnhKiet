using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FullSet", menuName = "ScriptableObjects/FullSet", order = 1)]
public class FullSet : ScriptableObject
{
    public FullSetType type;
    public Material groupMaterial;
    public Material pantMaterial;
    public GameObject hatPref;
    public GameObject weaponPref;
    public GameObject wingPref;
    public GameObject tailPref;

}
