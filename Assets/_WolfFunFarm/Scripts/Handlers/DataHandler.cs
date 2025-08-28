using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace WolfFunFarm
{
    public class DataHandler
    {
        private readonly GameManager _gameManager;

        private Dictionary<string, int> _ingameAssets = new Dictionary<string, int>();
        private Dictionary<string, int> _breedSeedRemains = new Dictionary<string, int>();

        public DataHandler(GameManager manager)
        {
            _gameManager = manager;

            _ingameAssets = new Dictionary<string, int>()
            {
                { "Gold", 0 },
                { "Land", 3 },
                { "Worker", 1 },
                { "Machine", 1 }
            };
            _breedSeedRemains = new Dictionary<string, int>()
            {
                { "Cow", 2 },
                { "Tomato", 10 },
                { "Blueberry", 10 },
                { "Strawberry", 0 },
            };
        }

        public void AddBreedSeed(string breedId, int amount)
        {
            if (_breedSeedRemains.ContainsKey(breedId))
            {
                _breedSeedRemains[breedId] += amount;
            }
            else
            {
                _breedSeedRemains[breedId] = amount;
            }
        }
        public void AddIngameAsset(string assetId, int amount)
        {
            if (_ingameAssets.ContainsKey(assetId))
            {
                _ingameAssets[assetId] += amount;
            }
            else
            {
                _ingameAssets[assetId] = amount;
            }
        }

        public int GetBreedSeedAmount(string breedId)
        {
            if (_breedSeedRemains.ContainsKey(breedId))
            {
                return _breedSeedRemains[breedId];
            }

            return 0;
        }
        public int GetIngameAssetAmount(string assetId)
        {
            if (_ingameAssets.ContainsKey(assetId))
            {
                return _ingameAssets[assetId];
            }

            return 0;
        }
    }
}