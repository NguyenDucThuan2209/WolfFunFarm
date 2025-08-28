using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfFunFarm
{
    public class MachineView : MonoBehaviour
    {
        private int _level;
        public int Level => _level;

        public void Initialize(int level)
        {
            _level = level;
        }
    }
}