using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FamilyOrganizer;

public class ListPageController : MonoBehaviour
{
    // Start is called before the first frame update
    GroupPlateDisplayController GroupPlateDisplay;
    public BuiltinPlateController[] BuiltinGroupsControllers;
    void Start()
    {
        GroupPlateDisplay = GetComponent<GroupPlateDisplayController>();
        UpdatePage();
    }

    // Update is called once per frame
    public void UpdatePage()
    {
        GroupPlateDisplay.UpdatePlates();
        foreach (BuiltinPlateController controller in BuiltinGroupsControllers)
        {
            controller.UpdatePlate();
        }
    }

    public void AddGroup()
    {
        TaskGroup newGroup = new("Новая группа");
        Config.OverallManager.AddTaskGroup(newGroup);
        PageController.OpenEditList(newGroup);
    }
}
