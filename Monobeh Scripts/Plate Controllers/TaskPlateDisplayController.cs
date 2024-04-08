using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FamilyOrganizer;
using Unity.VisualScripting;
using System.Text.RegularExpressions;
using FantomLib;
using System;
public class TaskPlateDisplayController : MonoBehaviour
{

    public GameObject PlatePrefab, PlatesLayout;
    public TaskGroup RepresentedGroup;
    public int DestroyUntil = 0;
    void Start()
    {
    }

    private void AssignTask(GameObject plate, Task task)
    {
        TaskPlateController taskPlateController = plate.GetComponent<TaskPlateController>();
        if (taskPlateController != null )
        {
            taskPlateController.RepresentedTask = task;
        }
        else throw new ArgumentNullException("task_controller", "У этого объекта нет компонента TaskPlateController.");
    }
    private void DestroyPlates()
    {
        for (int i = PlatesLayout.transform.childCount - 1 - DestroyUntil; i >= 0; i--)
        {
            Debug.Log($"PlateDisplay: Destroying {PlatesLayout.transform.GetChild(i).name}   index: {i}");
            Destroy(PlatesLayout.transform.GetChild(i).gameObject);
        }
    }


    public void UpdatePlates()
    {
        DestroyPlates();
        List<Task> allTasks = RepresentedGroup.GetTasks();
        foreach (Task task in allTasks)
        {
            GameObject newPlate = Instantiate(original: PlatePrefab, parent: PlatesLayout.transform);
            try
            {
                AssignTask(newPlate, task);
            }
            catch (ArgumentException e)
            {
                 Debug.LogException(e);
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(PlatesLayout.GetComponent<RectTransform>());
    }

}
