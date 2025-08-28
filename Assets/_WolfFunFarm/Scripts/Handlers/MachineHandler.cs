using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfFunFarm
{
    public class MachineHandler
    {
        private readonly GameManager _gameManager;

        private MachineView _machine;
        public MachineView Machine => _machine;

        public MachineHandler(GameManager manager)
        {
            _gameManager = manager;
        }

        public void InitializeMachine(MachineView machine, int level)
        {
            _machine = machine;
            _machine.Initialize(level);
        }
    }
}