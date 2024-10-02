using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "ScriptableObjects/CharacterConfig", order = 1)]
public class CharacterConfig : ScriptableObject
{
    public float radiusAttackStart;
    public float modelScaleStart;
    public float speed;
    public float attackSpeed;

}
