using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WolfFunFarm
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;
        public static UIManager Instance => _instance;

        [Header("Shop")]
        [SerializeField] GameObject _shopDetail;
        [SerializeField] GameObject _shopInfoHolder;
        [SerializeField] GameObject _shopInfoPrefab;
        private bool _isShowShopDetail = false;

        [Header("Bag Menu")]
        [SerializeField] GameObject _bagMenu;
        [SerializeField] Button _bagButton;
        private bool _isShowBagMenu = false;

        [Header("Breed Menu")]
        [SerializeField] GameObject _breedMenu;
        [SerializeField] Button _breedButton;
        [SerializeField] Button[] _breedButtons;
        private bool _isShowBreedMenu = false;

        [Header("Gold")]
        [SerializeField] TextMeshProUGUI _goldText;

        [Header("Land")]
        [SerializeField] Button _landButton;
        [SerializeField] GameObject _landDetail;
        [SerializeField] GameObject _landInfoHolder;
        [SerializeField] GameObject _landInfoPrefab;
        private bool _isShowLandDetail = false;

        [Header("Machine")]
        [SerializeField] TextMeshProUGUI _machineText;

        [Header("Worker")]
        [SerializeField] TextMeshProUGUI _workerText;

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
        }
        private void Update()
        {
            UpdateLandInfo();
            UpdateWorkerInfo();
            UpdateGoldAmount();
            UpdateMachineInfo();
            UpdateBagMenuStatus();
            UpdateBreedMenuStatus();
            UpdateShopMenuStatus(); 
        }

        private void UpdateLandInfo()
        { 
            _landDetail.SetActive(_isShowLandDetail);

            _landButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Land\n({GameManager.Instance.DataHandler.GetIngameAssetAmount("Land")})";
        }
        public void UpdateWorkerInfo()
        {
            var currentWorkers = GameManager.Instance.WorkerHandler.Workers;
            var isWorking = currentWorkers.Count(currentWorkers => currentWorkers.IsWorking);

            _workerText.text = $"Worker:\n" +
                               $"-Working: {isWorking}\n" +
                               $"-Relaxing: {currentWorkers.Count - isWorking}";
        }
        private void UpdateGoldAmount()
        {
            var currentGold = GameManager.Instance.Money;
            _goldText.text = $"Gold:\n{currentGold}";
        }
        private void UpdateMachineInfo()
        {
            if (GameManager.Instance.MachineHandler.Machine != null 
                && ConfigHandler.GetMachineConfig() != null)
            {
                _machineText.text = $"Machine:\n" +
                                $"{GameManager.Instance.MachineHandler.Machine.Level} " +
                                $"(Boost {ConfigHandler.GetMachineConfig().Boost}%)";
            }
        }
        private void UpdateBagMenuStatus()
        {
            _bagMenu.SetActive(_isShowBagMenu);
            if (_isShowBagMenu)
            {
            }
            else
            {
            }
        }
        private void UpdateBreedMenuStatus()
        {
            _breedMenu.SetActive(_isShowBreedMenu);

            if (_isShowBreedMenu)
            {
                foreach (var button in _breedButtons)
                {
                    if (button.name.Contains("Cow"))
                    {
                        var currentCow = GameManager.Instance.DataHandler.GetBreedSeedAmount("Cow");

                        button.enabled = currentCow > 0;
                        button.GetComponentInChildren<TextMeshProUGUI>().text = $"Cow\n({currentCow})";
                    }
                    if (button.name.Contains("Tomato"))
                    {
                        var currentTomato = GameManager.Instance.DataHandler.GetBreedSeedAmount("Tomato");

                        button.enabled = currentTomato > 0;
                        button.GetComponentInChildren<TextMeshProUGUI>().text = $"Tomato\n({currentTomato})";
                    }
                    if (button.name.Contains("Blueberry"))
                    {
                        var currentBlueberry = GameManager.Instance.DataHandler.GetBreedSeedAmount("Blueberry");

                        button.enabled = currentBlueberry > 0;
                        button.GetComponentInChildren<TextMeshProUGUI>().text = $"Blueberry\n({currentBlueberry})";
                    }
                    if (button.name.Contains("Strawberry"))
                    {
                        var currentStrawberry = GameManager.Instance.DataHandler.GetBreedSeedAmount("Strawberry");

                        button.enabled = currentStrawberry > 0;
                        button.GetComponentInChildren<TextMeshProUGUI>().text = $"Strawberry\n({currentStrawberry})";
                    }
                }
            }
        }
        private void UpdateShopMenuStatus()
        { 
            _shopDetail.SetActive(_isShowShopDetail);
        }

        public void OnSelectBreed(string cropId)
        {
            GameManager.Instance.SelectBreed(cropId);
            GameManager.Instance.ChangeGameState(GameState.Breeding);
        }
        public void OnLandButtonPressed()
        {
            _isShowLandDetail = !_isShowLandDetail;
        }
        public void OnBagButtonPressed()
        {
            _isShowBagMenu = !_isShowBagMenu;

            if (_isShowBagMenu)
            {
                _isShowBreedMenu = false;
            }
        }
        public void OnBreedButtonPressed()
        {
            _isShowBreedMenu = !_isShowBreedMenu;

            if (_isShowBreedMenu)
            {
                _isShowBagMenu = false;
            }
        }
        public void OnShopButtonPressed()
        {
            _isShowShopDetail = !_isShowShopDetail;
        }
    }
}