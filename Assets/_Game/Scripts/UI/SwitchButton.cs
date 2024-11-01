using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwitchButton : MonoBehaviour
{
    [SerializeField] private GameObject onImage;
    [SerializeField] private GameObject offImage;

    public void SetOn()
    {
        onImage.SetActive(true);
        offImage.SetActive(false);
    }

    public void SetOff()
    {
        onImage.SetActive(false);
        offImage.SetActive(true);

    }
}
