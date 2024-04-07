using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FamilyOrganizer;
using System;
using Unity.VisualScripting;
public class TaskEditPageController : MonoBehaviour
{
    // Start is called before the first frame update
    public Task RepresentedTask;
    public TaskGroup RepresentedGroup;
    public GameObject DeadlinePicker, LastDonePicker, IntervalPicker;
    public Text DeadlineHolder, LastDoneHolder;
    public InputField NameHolder, IntervalHolder;
    public GameObject NameWarning, IntervalWarning, DeadlineWarning, LastDoneWarning;
    public Image TypeHolder;
    public Sprite Simple, OneTime, Repeatable;
    void Start()
    {
    }


    // Update is called once per frame
    public void ChangeName()
    {
        NameWarning.SetActive(false);
        try
        {
            RepresentedTask.Name = NameHolder.text;
        }
        catch
        {
            NameWarning.SetActive(true);
        }
    }

    public void ChangeInterval()
    {
        IntervalWarning.SetActive(false);
        if (RepresentedTask is RepeatableTask repeatableTask)
        {
            try 
            { 
                int interval = Int32.Parse(IntervalHolder.text);
                repeatableTask.Interval = interval;
                Debug.Log("Мы сменили интервал");
            }
            catch (Exception e)
            {
                IntervalWarning.SetActive(true);
                IntervalWarning.GetComponent<Text>().text = e.Message;
            }
        }
    }

    public void ChangeType(string type)
    {
        if (type == "simple")
        {
            if (RepresentedTask is OneTimeTask || RepresentedTask is RepeatableTask)
            {
                Task newTask = new(name: RepresentedTask.Name, executor: RepresentedTask.Executor);
                if (RepresentedGroup != null)
                {
                    RepresentedGroup.RemoveTask(RepresentedTask.ID);
                    RepresentedGroup.AddTask(newTask);
                }
                RepresentedTask = newTask;
                Debug.Log("Мой тип сменился на простой");
            }
        }
        else if (type == "repeatable")
        {
            RepeatableTask newTask = new(name: RepresentedTask.Name, interval: 7, executor: RepresentedTask.Executor);
            if (RepresentedGroup != null)
            {
                RepresentedGroup.RemoveTask(RepresentedTask.ID);
                RepresentedGroup.AddTask(newTask);
            }
            RepresentedTask = newTask;
            Debug.Log("Мой тип сменился на повторяемый");
        }

        else if (type == "onetime")
        {
            OneTimeTask newTask = new(name: RepresentedTask.Name, executor: RepresentedTask.Executor, deadline: DateTime.Today);
            if (RepresentedGroup != null)
            {
                RepresentedGroup.RemoveTask(RepresentedTask.ID);
                RepresentedGroup.AddTask(newTask);
            }
            RepresentedTask = newTask;
            Debug.Log("Мой тип сменился на со сроком");
        }
        Debug.Log($"ChangeType: я ссылаюсь на задачу с айди {RepresentedTask.ID}  и группу с айди {RepresentedGroup.TaskgroupID}");
        UpdatePage();
    }

    public void ChangeDeadline(DateTime newDeadline)
    {
        DeadlineWarning.SetActive(false);
        DeadlineHolder.text = DateConverter.RepresentDate(newDeadline);
        if (RepresentedTask is OneTimeTask oneTimeTask)
        {
            try
            {
                oneTimeTask.Deadline = newDeadline;
            }
            catch (Exception e)
            {
                DeadlineWarning.SetActive(true);
                DeadlineWarning.GetComponent<Text>().text = e.Message;
            }
        }
    }

    public void ChangeLastdone(DateTime newLastDone)
    {
        LastDoneWarning.SetActive(false);
        LastDoneHolder.text = DateConverter.RepresentDate(newLastDone);
        if (RepresentedTask is RepeatableTask repeatableTask)
        {
            try
            {
                repeatableTask.LastDone = newLastDone;
            }
            catch (Exception e)
            {
                LastDoneWarning.SetActive(true);
                LastDoneWarning.GetComponent<Text>().text = e.Message;
            }
        }
    }

    public void UpdatePage()
    {
        Debug.Log($"TaskEdit: я ссылаюсь на задачу с айди {RepresentedTask.ID} и группу с айди {RepresentedGroup.TaskgroupID}");

        if (RepresentedTask == null)
        {
            Debug.Log("У этой страницы ещё нет назначенной задачи.");
            return;
        }
 
        MinimizeFields();
        NameHolder.text = RepresentedTask.Name;

        if (RepresentedTask is RepeatableTask repeatableTask)
        {
            TypeHolder.sprite = Repeatable;

            LastDonePicker.SetActive(true);
            LastDoneHolder.text = DateConverter.RepresentDate(repeatableTask.LastDone);

            IntervalPicker.SetActive(true);
            IntervalHolder.text = repeatableTask.Interval.ToString();


        }

        else if (RepresentedTask is OneTimeTask oneTimeTask)
        {
            TypeHolder.sprite = OneTime;

            DeadlinePicker.SetActive(true);
            DeadlineHolder.text = DateConverter.RepresentDate(oneTimeTask.Deadline);
        }

        else
        {
            TypeHolder.sprite = Simple;
        }
    }

    private void MinimizeFields()
    {
        DeadlinePicker.gameObject.SetActive(false);
        LastDonePicker.gameObject.SetActive(false);
        IntervalPicker.gameObject.SetActive(false);
    }

    public void GoBack()
    {
        string Ids = "";
        foreach (Task task in RepresentedGroup.GetTasks())
        {
            Ids += task.Name + " ";
        }
        Debug.Log($"Edited task id {RepresentedTask.ID}");
        Debug.Log($"Ids for current group: {Ids}");


        PageController.ListSubController.UpdatePage();
        PageController.HomeController.UpdatePage();
        this.gameObject.SetActive(false);
    }
}
