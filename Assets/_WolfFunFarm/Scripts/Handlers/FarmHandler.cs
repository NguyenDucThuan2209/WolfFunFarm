using System.Collections.Generic;
using UnityEngine;

namespace WolfFunFarm
{
    public class FarmHandler
    {
        private readonly GameManager _gameManager;

        private List<LandView> _lands;
        public List<LandView> Lands => _lands;

        public List<LandView> EmptyLands
        {
            get
            {
                var emptyLands = new List<LandView>();
                
                foreach (var land in _lands)
                {
                    if (land.IsEmpty) emptyLands.Add(land);
                }

                return emptyLands;
            }
        }
        public List<LandView> HarvestLands
        {
            get
            {
                var harvestLands = new List<LandView>();
                
                foreach (var land in _lands)
                {
                    if (land.IsHarvestable) harvestLands.Add(land);
                }

                return harvestLands;
            }
        }

        public FarmHandler(GameManager manager)
        {
            _gameManager = manager;

            _lands = new List<LandView>();
        }
    }
}