using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Pant", menuName = "ScriptableObjects/Pant", order = 1)]
public class Pant : ScriptableObject
{
    public PantsType type;
    public Material material;
}
