using FamilyOrganizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuiltinPlateController : MonoBehaviour
{
    public Text CountHolder;
    private TaskGroup _representedGroup;
    public string Name;
    void Start()
    {
        _representedGroup = Config.BuiltinTasks[Name];
        UpdatePlate();
    }

    public void UpdatePlate()
    {
        CountHolder.text = _representedGroup.Count.ToString();
    }

    public void Open()
    {
        PageController.OpenSublist(_representedGroup);
    }
}
