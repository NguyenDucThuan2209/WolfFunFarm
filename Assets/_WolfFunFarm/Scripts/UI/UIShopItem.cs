using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WolfFunFarm
{
    public class UIShopItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _nameText;
        [SerializeField] TextMeshProUGUI _priceText;
        [SerializeField] Button _buyButton;

        private FarmEntityConfig _config;

        private void Update()
        {
            _buyButton.enabled = GameManager.Instance.Money >= _config.SeedPrice;
        }

        public void BuyItem()
        {
            // Todo: Update money and seed amount in DataHandler
        }
        public void Initialize(FarmEntityConfig config)
        {
            _config = config;

            _nameText.text = $"Name: {config.Name}";
            _priceText.text = $"Price: {config.SeedPrice}";
        }
    }
}