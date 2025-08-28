using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfFunFarm
{
    public class WorkerView : MonoBehaviour
    {
        private Task _currentTask;
        public Task CurrentTask => _currentTask;

        public bool IsWorking => _currentTask != null;

        private void Update()
        {
            if (_currentTask == null) return;

            if (transform.position == _currentTask.Land.Zone.position)
            {
                if (_currentTask.Id.Contains("Breed"))
                {
                    if (_currentTask.Id.Contains("Cow"))
                    {
                        var config = ConfigHandler.GetEntityConfig("Cow");
                        var prefab = GameManager.Instance.GetFarmEntity("Cow");
                        _currentTask.Land.Breed(prefab, config);
                    }
                    if (_currentTask.Id.Contains("Tomato"))
                    {
                        var config = ConfigHandler.GetEntityConfig("Tomato");
                        var prefab = GameManager.Instance.GetFarmEntity("Tomato");
                        _currentTask.Land.Breed(prefab, config);
                    }
                    if (_currentTask.Id.Contains("Blueberry"))
                    {
                        var config = ConfigHandler.GetEntityConfig("Blueberry");
                        var prefab = GameManager.Instance.GetFarmEntity("Blueberry");
                        _currentTask.Land.Breed(prefab, config);
                    }
                    if (_currentTask.Id.Contains("Strawberry"))
                    {
                        var config = ConfigHandler.GetEntityConfig("Strawberry");
                        var prefab = GameManager.Instance.GetFarmEntity("Strawberry");
                        _currentTask.Land.Breed(prefab, config);
                    }
                }
                if (_currentTask.Id.Contains("Harvest"))
                {
                    _currentTask.Land.Harvest();
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _currentTask.Land.Zone.position, 2f * Time.deltaTime);
            }
        }

        public void AssignTask(Task task)
        {
            _currentTask = task;
        }
    }
}