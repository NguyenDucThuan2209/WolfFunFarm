using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace WolfFunFarm
{
    public class UILandInfo : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _nameText;
        [SerializeField] TextMeshProUGUI _statusText;

        private LandView _currentLand;

        private void Update()
        {
            if (_currentLand.IsEmpty)
            {
                _statusText.text = "Status: Empty";
            }
            else
            {
                _statusText.text = $"Status: " +
                                   $"{(_currentLand.IsHarvestable ? "Harvestable" : "Growing")} " +
                                   $"({_currentLand.ProducedProducts}/{_currentLand.CurrentConfig.LifetimeProducts})";
            }
        }

        public void Initialize(int index, LandView land)
        { 
            _nameText.text = $"Land {index + 1}";
            _currentLand = land;
        }
    }
}