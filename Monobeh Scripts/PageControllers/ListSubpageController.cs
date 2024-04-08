using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FamilyOrganizer;
using UnityEngine.UI;
using System;

public class ListSubpageController : MonoBehaviour
{
    // Важно - edit page может быть включена не только для редактирования, но и для создания
    public GameObject ListSubpage, EditListPage;
    public TaskGroup RepresentedTaskgroup;
    public TaskPlateDisplayController PlateDisplayController;
    public GameObject EditButton;
    public Text NameHolder;
    public GameObject Layout;
    public GameObject DescriptionPrefab;
    void Start()
    {
    }

    // Update is called once per frame
    public void UpdatePage()
    {

        if (RepresentedTaskgroup == null)
        {
            Debug.Log("У страницы пока нет назначенной группы.");
            return;
        }

        NameHolder.text = RepresentedTaskgroup.GroupName;

        DestroyPlates();

        if (!String.IsNullOrEmpty(RepresentedTaskgroup.Description))
        {
            GameObject DescriptionHolder = Instantiate(original: DescriptionPrefab, parent: Layout.transform);
            DescriptionHolder.GetComponent<Text>().text = RepresentedTaskgroup.Description;
        }
        else
        {
            GameObject DescriptionHolder = Instantiate(original: new GameObject(), parent: Layout.transform);
        }

        if (RepresentedTaskgroup.IsBuiltin) EditButton.SetActive(false);
        else EditButton.SetActive(true);

        PlateDisplayController.RepresentedGroup = RepresentedTaskgroup;
        PlateDisplayController.UpdatePlates();
    }

    private void DestroyPlates()
    {
        for (int i = Layout.transform.childCount - 1; i >= 0; i--)
        {
            Debug.Log($"Subpage: Destroying {Layout.transform.GetChild(i).name}");
            Destroy(Layout.transform.GetChild(i).gameObject);
        }
    }

    public void OpenEditList()
    {
        if(RepresentedTaskgroup.IsBuiltin)
        {
            Debug.Log("Нельзя редактировать встроенные списки.");
            return;
        }
        PageController.OpenEditList(RepresentedTaskgroup);
    }

    public void AddTask()
    {
        Task newTask = new(name: "Новая задача", executor: Config.MainPerson);
        RepresentedTaskgroup.AddTask(newTask);
        PageController.OpenEditTask(newTask, RepresentedTaskgroup);
    }

}
