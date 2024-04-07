﻿using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;


namespace FamilyOrganizer
{
    public class TaskGroup
    {
        private static int _count = 0;
        public readonly int TaskgroupID = _count++;
        public readonly bool IsBuiltin;
        private string _groupName, _description;
        private Dictionary<int, Task> _tasks = new();


        public TaskGroup(string groupName, bool isBuiltin = false, string description = "")
        {
            GroupName = groupName;
            Description = description;
            IsBuiltin = isBuiltin;
        }

        public string GroupName
        {
            get { return _groupName; }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("groupname", "Название группы не может быть пустым.");
                if (value.Length < 5) throw new ArgumentException("groupname", "Название группы должно содержать не менее 5 символов.");
                if (value.Length > 20) throw new ArgumentException("groupname", "Название группы должно содержать не более 20 символов.");
                if (IsBuiltin) throw new Exception("Нельзя изменить название встроенной группы задач.");
                _groupName = value;
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                if (IsBuiltin) throw new Exception("Нельзя изменить описание встроенной группы задач.");
                if (value.Length > 40) throw new ArgumentException("description", "Описание не может быть длиннее 40 символов.");
                _description = value;
            }
        }


        public bool IsEmpty
        {
            get
            {
                return _tasks.Count == 0;
            }
        }

        public int Count
        {
            get
            {
                return _tasks.Count;
            }
        }

        //on add an remove calls UpdatePage method calls from above are needed.
        public void AddTask(Task task)
        {
            _tasks.Add(task.ID, task);
            UnityEngine.Debug.Log($"В группу с айди {TaskgroupID} добавилась задачи с айди {task.ID}");
        }

        public void RemoveTask(int id)
        {
            _tasks.Remove(id);
        }

        //Вот тут прикол с айди, аккуратно. И вообще проверку на наличие сюда бы прилепить
        public Task GetTask(int id)
        {
            return _tasks[id];
        }
        
        public List<Task> GetTasks()
        {
            List<Task> tasks = new List<Task>();
            foreach (Task task in _tasks.Values)
            {
                tasks.Add(task);
            }
            return tasks;
        }

        public List<int> GetGroupData()
        {
            List<int> data = new();
            foreach (Task t in _tasks.Values)
            {
                data.Add(t.ID);
            }
            return data;
        }

        public void ClearGroup()
        {
            _tasks.Clear();
        }
        

    }
}
