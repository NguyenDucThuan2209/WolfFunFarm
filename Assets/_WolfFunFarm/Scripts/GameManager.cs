using System.Collections.Generic;
using UnityEngine;

namespace WolfFunFarm
{
    public enum GameState
    {
        None,
        Standby,
        Breeding,
    }

    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance;

        [Header("Machine")]
        [SerializeField] MachineView _machinePrefab;
        [SerializeField] Vector3 _machineStartPos;
        [Header("Land")]
        [SerializeField] LandView _landPrefab;
        [SerializeField] Vector3 _landStartPos;
        [SerializeField] Vector3 _landSpawnOffset;
        [Header("Worker")]
        [SerializeField] WorkerView _workerPrefab;
        [SerializeField] Vector3 _workerStartPos;
        [SerializeField] Vector3 _workerSpawnOffset;
        [Header("Farm Entity")]
        [SerializeField] List<FarmEntity> _farmPrefab;

        public int Money => DataHandler.GetIngameAssetAmount("Gold");

        private FarmEntity _selectPrefab;
        private FarmEntityConfig _selectConfig;

        private GameState _currentState = GameState.None;

        public MachineHandler MachineHandler { get; private set; }
        public WorkerHandler WorkerHandler { get; private set; }
        public FarmHandler FarmHandler { get; private set; }
        public DataHandler DataHandler { get; private set; }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }

            ConfigHandler.InitializeConfigs();
        }
        private void Start()
        {
            DataHandler = new DataHandler(this);
            MachineHandler = new MachineHandler(this);
            WorkerHandler = new WorkerHandler(this);
            FarmHandler = new FarmHandler(this);

            InitializeScene();
            ChangeGameState(GameState.Standby);
        }
        private void Update()
        {
            WorkerHandler.OnUpdate();

            UpdateGameState();
        }

        private void UpdateGameState()
        {
            switch (_currentState)
            {
                case GameState.None:
                    {

                    }
                    break;
                case GameState.Standby:
                    {
                    }
                    break;
                case GameState.Breeding:
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            if (Physics.Raycast(ray, out var hitObject, Mathf.Infinity, LayerMask.GetMask("Selectable")))
                            {
                                if (hitObject.collider.CompareTag("Land"))
                                {
                                    var land = hitObject.collider.GetComponentInParent<LandView>();
                                    land.Breed(_selectPrefab, _selectConfig);
                                    ChangeGameState(GameState.Standby);
                                }
                            }
                        }
                    }
                    break;
            }
        }

        public void ChangeGameState(GameState newState)
        {
            _currentState = newState;
        }

        public void InitializeScene()
        {
            var spawnPos = _machineStartPos;
            var machineLevel = DataHandler.GetIngameAssetAmount("Machine");
            var machine = Instantiate(_machinePrefab, spawnPos, Quaternion.identity);
            MachineHandler.InitializeMachine(_machinePrefab, machineLevel);

            spawnPos = _landStartPos;
            var landAmount = DataHandler.GetIngameAssetAmount("Land");
            for (int i = 0; i < landAmount; i++)
            {
                var land = Instantiate(_landPrefab, spawnPos, Quaternion.identity);
                spawnPos += _landSpawnOffset;

                FarmHandler.Lands.Add(land);
            }

            spawnPos = _workerStartPos;
            var workerAmount = DataHandler.GetIngameAssetAmount("Worker");
            for (int i = 0; i < workerAmount; i++)
            {
                var worker = Instantiate(_workerPrefab, spawnPos, Quaternion.identity);
                spawnPos += _workerSpawnOffset;

                WorkerHandler.Workers.Add(worker);
            }
        }
        public void SelectBreed(string entityId)
        {
            _selectConfig = ConfigHandler.GetEntityConfig(entityId);
            _selectPrefab = _farmPrefab.Find(prefab => prefab.Id == entityId);
        }

        public FarmEntity GetFarmEntity(string entityId)
        {
            return _farmPrefab.Find(prefab => prefab.Id == entityId);
        }
    }
}