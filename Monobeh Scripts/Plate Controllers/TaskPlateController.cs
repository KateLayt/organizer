using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FamilyOrganizer;
using UnityEngine.UI;
using System;

public class TaskPlateController : MonoBehaviour
{
    // Start is called before the first frame update
    public Task RepresentedTask;
    public Text NameHolder, LastdoneHolder, DeadlineHolder;
    public Image StatusHolder;
    public Sprite BadStatus, NormalStatus, GoodStatus;
    void Start()
    {
        UpdatePlate();
    }

    public void UpdatePlate()
    {
        //Вообще хорошо бы тут ещё размеры плашек менять
        //Если в процессе поменяли тип задачи - то создали новую задачу, и для объекта плашки его тоже заменили, а потом обновили плашку
        SetAllActive();
        NameHolder.text = RepresentedTask.Name;
        Debug.Log($"Меня апдейтнули, имя моей задачи - {RepresentedTask.Name}");
        if (RepresentedTask is RepeatableTask repeatableTask)
        {
            LastdoneHolder.text = $"Последний раз выполнено {DateConverter.RepresentDate(repeatableTask.LastDone)}";
            DeadlineHolder.text = $"Повторяется каждые {DateConverter.RepresentDays(repeatableTask.Interval)}";
            switch(repeatableTask.TaskStatus) 
            {
                case "Всё хорошо":
                    StatusHolder.sprite = GoodStatus;
                    Debug.Log("Сменилось на хорошо");
                    break;
                case "Нормально":
                    StatusHolder.sprite = NormalStatus;
                    Debug.Log("Сменилось на норм");
                    break;
                case "За работу!":
                    StatusHolder.sprite = BadStatus;
                    Debug.Log("Сменилось на плохо"); 
                    break;
            }
        }
        else if (RepresentedTask is OneTimeTask oneTimeTask)
        {
            DeadlineHolder.gameObject.SetActive(false);
            StatusHolder.gameObject.SetActive(false);
            if (oneTimeTask.TaskStatus == "Не завершено")
            {
                LastdoneHolder.text = $"Осталось {DateConverter.RepresentDays(DateConverter.DifferenceInDays(oneTimeTask.Deadline, DateTime.Today))}";
            }
            else
            {
                LastdoneHolder.text = "Задача выполнена";
            }
        }
        else
        {
            DeadlineHolder.gameObject.SetActive(false);
            StatusHolder.gameObject.SetActive(false);
            if (RepresentedTask.TaskStatus == "Не завершено")
            {
                LastdoneHolder.gameObject.SetActive(false);
            }
            else
            {
                LastdoneHolder.text = "Задача выполнена";
            }
        }
    }

    private void SetAllActive()
    {
        NameHolder.gameObject.SetActive(true);
        LastdoneHolder.gameObject.SetActive(true);
        DeadlineHolder.gameObject.SetActive(true);
        StatusHolder.gameObject.SetActive(true);
    }

    public void Done()
    {

        if (RepresentedTask is RepeatableTask repeatable)
        {
            repeatable.Complete();
        }
        else
        {
            RepresentedTask.Complete();
        }
        UpdatePlate();
    }
    

    public void Edit()
    {
        PageController.OpenEditTask(RepresentedTask, PageController.ListSubController.RepresentedTaskgroup); // Эта че
        Debug.Log($"PlateController: я ссылаюсь на задачу с айди {RepresentedTask.ID}");
    }
}
