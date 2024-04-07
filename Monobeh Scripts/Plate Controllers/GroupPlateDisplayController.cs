using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FamilyOrganizer;
using Unity.VisualScripting;
using System.Text.RegularExpressions;
using System;
public class GroupPlateDisplayController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject PlatePrefab, PlatesLayout;
    void Start()
    {
    }

    private void AssignGroup(GameObject plate, TaskGroup group)
    {
        ListPlateController listPlateController = plate.GetComponent<ListPlateController>();
        if (listPlateController != null )
        {
            listPlateController.RepresentedGroup = group;
        }
        else throw new ArgumentNullException("list_controller", "У этого объекта нет компонента ListPlateController.");
    }

    private void DestroyPlates()
    {
        for (int i = PlatesLayout.transform.childCount - 1; i > 0; i--)
        {
            Destroy(PlatesLayout.transform.GetChild(i).gameObject);
        }
    }

    public void UpdatePlates()
    {
        DestroyPlates();
        List<TaskGroup> AllGroups = Config.OverallManager.GetAllGroups();
        foreach (TaskGroup group in AllGroups)
        {
            if (!(group.IsBuiltin))
            {
                GameObject newPlate = Instantiate(original: PlatePrefab, parent: PlatesLayout.transform);
                AssignGroup(newPlate, group);
        }
        }
    }

}
