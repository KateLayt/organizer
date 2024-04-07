using FamilyOrganizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListPlateController : MonoBehaviour
{
    public Text NameHolder, CountHolder;
    public TaskGroup RepresentedGroup;
    void Start()
    {
        UpdatePlate();
    }

    public void UpdatePlate()
    {
        NameHolder.text = RepresentedGroup.GroupName;
        CountHolder.text = RepresentedGroup.Count.ToString();
    }

    public void Open()
    {
        PageController.OpenSublist(RepresentedGroup);
    }

}
