using FamilyOrganizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public HomePageController HomeController;
    public ListPageController ListController;
    public ProfilePageController ProfileController;
    public ListSubpageController ListSubController;
    public EditListPageController ListEditController;
    public TaskEditPageController TaskEditController;

    private void Start()
    {
        PageController.HomeController = HomeController;
        PageController.ListController = ListController;
        PageController.ProfileController = ProfileController;
        PageController.ListSubController = ListSubController;
        PageController.ListEditController = ListEditController;
        PageController.TaskEditController = TaskEditController;
    }
    public void OpenHome()
    {
        PageController.OpenHome();
    }
    public void OpenLists()
    {
        PageController.OpenLists();
    }

    public void OpenProfile()
    {
        PageController.OpenProfile();
    }

    public void OpenSublist(TaskGroup taskgroup)
    {
        PageController.OpenSublist(taskgroup);
    }

    public void OpenEditList(TaskGroup taskgroup)
    {
        PageController.OpenEditList(taskgroup);
    }

    public void OpenEditTask(Task task)
    {
        PageController.OpenEditTask(task);
    }
}
