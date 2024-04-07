using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace FamilyOrganizer
{
    public class TaskgroupManager
    {
        private Dictionary<int, TaskGroup> _taskGroup = new();
        private Person _owner;
        public TaskgroupManager(Person owner) 
        {
            Owner = owner;
        }

        public Person Owner
        {
            get { return _owner; }
            private set
            {
                if (value == null) throw new ArgumentNullException("owner", "У групп задач должен быть владелец.");
                _owner = value;
            }
        }

        public void AddTaskGroup(TaskGroup taskgroup)
        {
            _taskGroup.Add(taskgroup.TaskgroupID, taskgroup);
        }

        //тут ещё предупреждения о наличии такого айди вообще. аналогично в группе задач при поиске/удалении задач
        public void RemoveTaskGroup(int taskgroupID)
        {
            _taskGroup.Remove(taskgroupID);
        }

        public TaskGroup GetTaskGroup(int taskgroupID)
        {
            return _taskGroup[taskgroupID];
        }

        public List<TaskGroup> GetAllGroups()
        {
            return _taskGroup.Values.ToList();
        }

    }
}
