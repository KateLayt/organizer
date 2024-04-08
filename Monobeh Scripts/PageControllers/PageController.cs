using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FamilyOrganizer;

public class PageController
{
    public static HomePageController HomeController;
    public static ListPageController ListController;
    public static ProfilePageController ProfileController;
    public static ListSubpageController ListSubController;
    public static EditListPageController ListEditController;
    public static TaskEditPageController TaskEditController;
    public static bool IsAvailable = true;

    private static void DisablePages()
    {
        HomeController.gameObject.SetActive(false);
        ListController.gameObject.SetActive(false);
        ProfileController.gameObject.SetActive(false);
        ListSubController.gameObject.SetActive(false);
        ListEditController.gameObject.SetActive(false);
        TaskEditController.gameObject.SetActive(false);
    }
    public static void OpenHome()
    {
        DisablePages();
        HomeController.gameObject.SetActive(true);
        HomeController.UpdatePage();
    }
    public static void OpenLists()
    {
        DisablePages();
        ListController.gameObject.SetActive(true);
        ListController.UpdatePage();
    }

    public static void OpenProfile()
    {
        DisablePages();
        ProfileController.gameObject.SetActive(true);
        ProfileController.UpdatePage();
    }

    public static void OpenSublist(TaskGroup taskgroup)
    {
        DisablePages();
        ListSubController.gameObject.SetActive(true);
        ListSubController.RepresentedTaskgroup = taskgroup;
        ListSubController.UpdatePage();
    }

    public static void OpenEditList(TaskGroup taskgroup)
    {
        DisablePages();
        ListEditController.gameObject.SetActive(true);
        ListEditController.RepresentedGroup = taskgroup;
        ListEditController.UpdatePage();
    }

    public static void OpenEditTask(Task task, TaskGroup taskgroup = null)
    {
        TaskEditController.gameObject.SetActive(true);
        Debug.Log($"PageController: я ссылаюсь на задачу с айди {task.ID}");
        TaskEditController.RepresentedTask = task;
        TaskEditController.RepresentedGroup = taskgroup;
        TaskEditController.UpdatePage();
    }
}
