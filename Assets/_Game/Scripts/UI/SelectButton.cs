using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : MonoBehaviour
{
    [SerializeField] private GameObject SelectedButton;
    [SerializeField] private GameObject UnSelectedButton;

    public void Select()
    {
        SelectedButton.SetActive(true);
        UnSelectedButton.SetActive(false);
    }

    public void UnSelect()
    {
        SelectedButton.SetActive(false);
        UnSelectedButton.SetActive(true);
    }

}
