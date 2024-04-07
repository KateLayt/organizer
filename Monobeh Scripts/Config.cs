using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FamilyOrganizer;
using System;

public static class Config
{
    public static Person MainPerson;
    public static TaskgroupManager OverallManager;
    public static TaskGroup OtherList1, OtherList2;
    public static Task plainTask;
    public static OneTimeTask onetimeTask;
    public static RepeatableTask repeatableTask;
    public static Dictionary<string, TaskGroup> BuiltinTasks;

    static Config()
    {
        MainPerson = new Person();
        OverallManager = new(MainPerson);

        BuiltinTasks = new Dictionary<string, TaskGroup>();
        BuiltinTasks.Add("Уборка", new TaskGroup(groupName: "Уборка", isBuiltin: true));
        BuiltinTasks.Add("Продукты", new TaskGroup(groupName: "Продукты", isBuiltin: true));
        BuiltinTasks.Add("Прочее", new TaskGroup(groupName: "Прочее", isBuiltin: true));
        BuiltinTasks.Add("Список дел", new TaskGroup(groupName: "Список дел", isBuiltin: true));
        foreach (TaskGroup taskgroup in BuiltinTasks.Values)
        {
            OverallManager.AddTaskGroup(taskgroup);
        }


        OtherList1 = new(groupName: "Доп список 1", isBuiltin: false, description: "У меня есть описание!");
        OtherList2 = new(groupName: "Доп список 2", isBuiltin: false, description: "У меня есть описание!");
        UnityEngine.Debug.Log($"Назначенные айди: {OtherList1.TaskgroupID}, {OtherList2.TaskgroupID}");
        OverallManager.AddTaskGroup(OtherList1);
        OverallManager.AddTaskGroup(OtherList2);
        try
        {
            plainTask = new(name: "Простая задача", executor: MainPerson);
            onetimeTask = new(name: "Задача со сроком", executor: MainPerson, deadline: new DateTime(2024, 5, 13));
            repeatableTask = new(name: "Повторяемая задача", executor: MainPerson, interval: 21);
        }
        catch 
        {
            Debug.Log("Инициализация задач выкинула исключение");
        }
        try
        {
            OtherList1.AddTask(plainTask);
            OtherList1.AddTask(onetimeTask);
            OtherList2.AddTask(repeatableTask);
        }
        catch
        {
            Debug.Log("Добавление задач выкинуло исключение");
        }
    }

    public static TaskGroup GetUnfinishedTasks()
    {
        TaskGroup result = new("Незавершенные задачи");
        result.ClearGroup();
        foreach (TaskGroup tg in OverallManager.GetAllGroups())
        {
            foreach(Task task in tg.GetTasks())
            {
                if (task is RepeatableTask || task.TaskStatus == "Не завершено")
                {
                    result.AddTask(task);
                }
            }
        }
        return result;
    }

    public static TaskGroup GetFinishedTasks()
    {
        TaskGroup result = new("Завершенные задачи");
        result.ClearGroup();
        foreach (TaskGroup tg in OverallManager.GetAllGroups())
        {
            foreach (Task task in tg.GetTasks())
            {
                if (task.TaskStatus == "Завершено")
                {
                    result.AddTask(task);
                }
            }
        }
        return result;
    }
}
