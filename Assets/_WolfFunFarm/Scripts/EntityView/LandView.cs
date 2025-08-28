using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WolfFunFarm
{
    public class LandView : MonoBehaviour
    {
        [SerializeField] Image _fillImage;
        [SerializeField] Image _durationImage;
        [SerializeField] RectTransform _harvestButton;
        [Space]
        [SerializeField] Transform _zone;
        [SerializeField] Transform[] _slots;

        private int _remainProducts;
        private int _producedProducts;
        private DateTime _plantedTime;

        private GameObject _productPrefabs;
        private FarmEntityConfig _currentConfig;

        private List<FarmEntity> _liveEntities = new List<FarmEntity>();

        public int RemainProducts => _remainProducts;
        public int ProducedProducts => _producedProducts;

        public bool IsEmpty => _currentConfig == null;
        public bool IsHarvestable => _remainProducts > 0;
       
        public Transform Zone => _zone;
        public FarmEntityConfig CurrentConfig => _currentConfig;

        private void Update()
        {
            if (_currentConfig == null) return;

            var secondPassed = (DateTime.UtcNow - _plantedTime).TotalSeconds;
            var durationPassed = (int)secondPassed % _currentConfig.CycleDuration;
            var currentProducedAmount = (int)secondPassed / _currentConfig.CycleDuration;

            if (secondPassed > _currentConfig.CycleDuration)
            {
                _harvestButton.gameObject.SetActive(true);
                if (currentProducedAmount > _producedProducts)
                {
                    _remainProducts += currentProducedAmount - _producedProducts;
                    _producedProducts = currentProducedAmount;
                }
            }

            _fillImage.fillAmount = ((float)durationPassed / (float)_currentConfig.CycleDuration);

            if (_producedProducts >= _currentConfig.LifetimeProducts)
            {
                ResetLand();
            }
        }

        private void ResetLand()
        {
            foreach (var crop in _liveEntities)
            {
                Destroy(crop.gameObject);
            }

            _liveEntities = new List<FarmEntity>();
            _currentConfig = null;
            _producedProducts = 0;
        }

        public void Breed(FarmEntity entityPrefab, FarmEntityConfig config)
        {
            ResetLand();

            _plantedTime = DateTime.UtcNow;
            _currentConfig = config;
            _productPrefabs = entityPrefab.ProductPrefab;

            _fillImage.fillAmount = 0;

            _durationImage.gameObject.SetActive(true);
            _harvestButton.gameObject.SetActive(false);

            if (entityPrefab is CropView)
            {
                foreach (var slot in _slots)
                {
                    var entity = Instantiate(entityPrefab, slot);
                    _liveEntities.Add(entity);

                    var effect = slot.GetComponentInChildren<ParticleSystem>();
                    effect.Play();
                }
            }
            
            if (entityPrefab is AnimalView)
            {
                var entity = Instantiate(entityPrefab, _slots[4]);
                _liveEntities.Add(entity);

                var effect = _slots[4].GetComponentInChildren<ParticleSystem>();
                effect.Play();
            }
        }

        public void Harvest()
        {
            Debug.Log($"Harvest: {_remainProducts}");

            _remainProducts = 0;
        }
    }
}