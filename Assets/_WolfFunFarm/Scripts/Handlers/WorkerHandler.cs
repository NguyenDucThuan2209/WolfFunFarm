using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfFunFarm
{
    [System.Serializable]
    public class Task
    {
        public string Id;
        public LandView Land;
    }

    public class WorkerHandler
    {
        private readonly GameManager _gameManager;

        private List<WorkerView> _workers;
        public List<WorkerView> Workers => _workers;

        private Stack<Task> _availableTasks = new Stack<Task>();

        public WorkerHandler(GameManager manager)
        {
            _gameManager = manager;

            _workers = new List<WorkerView>();
        }

        public void OnUpdate()
        {
            UpdateAvailableTasks();
            UpdateAvailableWorker();
        }

        private void UpdateAvailableTasks()
        {
            _availableTasks = new Stack<Task>();

            var emptyLands = _gameManager.FarmHandler.EmptyLands;
            while (emptyLands.Count > 0)
            {
                var land = emptyLands[0];

                var hasCowSeed = _gameManager.DataHandler.GetBreedSeedAmount("Cow") > 0;
                var hasTomatoSeed = _gameManager.DataHandler.GetBreedSeedAmount("Tomato") > 0;
                var hasBlueberrySeed = _gameManager.DataHandler.GetBreedSeedAmount("Blueberry") > 0;
                var hasStrawberrySeed = _gameManager.DataHandler.GetBreedSeedAmount("Strawberry") > 0;

                if (hasCowSeed)
                {
                    _availableTasks.Push(new Task() { Id = "Breed_Cow", Land = land });
                    emptyLands.Remove(land);
                }
                else if (hasTomatoSeed)
                {
                    _availableTasks.Push(new Task() { Id = "Breed_Tomato", Land = land });
                    emptyLands.Remove(land);
                }
                else if (hasBlueberrySeed)
                {
                    _availableTasks.Push(new Task() { Id = "Breed_Blueberry", Land = land });
                    emptyLands.Remove(land);
                }
                else if (hasStrawberrySeed)
                {
                    _availableTasks.Push(new Task() { Id = "Breed_Strawberry", Land = land });
                    emptyLands.Remove(land);
                }
                else
                {
                    break;
                }
            }

            var harvestLands = _gameManager.FarmHandler.HarvestLands;
            while (harvestLands.Count > 0)
            {
                _availableTasks.Push(new Task() { Id = "Harvest", Land = harvestLands[0] });
                harvestLands.RemoveAt(0);
            }
        }
        private void UpdateAvailableWorker()
        {
            foreach (var worker in _workers)
            {
                if (!worker.IsWorking && _availableTasks.Count > 0)
                {
                    var task = _availableTasks.Pop();
                    worker.AssignTask(task);
                }
            }
        }
    }
}