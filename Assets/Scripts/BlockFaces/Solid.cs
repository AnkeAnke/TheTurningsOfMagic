﻿using UnityEngine;
using System.Collections;

namespace BlockFaces
{
    class Solid : MonoBehaviour, BlockFace
    {
        public bool OnEnter(Player player)
        {
            return true;
        }

        public void OnReached(Player player)
        {
        }
    }
}
