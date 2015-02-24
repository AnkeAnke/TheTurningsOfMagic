
using UnityEngine;
namespace BlockFaces
{
    class Solid : BlockFace
    {
        public override bool OnEnter(Player player)
        {
            return true;
        }

        public override void OnReached(Player player)
        {
        }
    }
}
