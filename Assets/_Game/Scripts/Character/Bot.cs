using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{
    [SerializeField] private GameObject targetCircle;


    public void IsTargeted(bool targeted)
    {
        targetCircle.SetActive(targeted);
    }

}
