using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlockFaces
{
    interface BlockFace
    {
        /// <summary>
        /// Called when the player wants to enter this block.
        /// </summary>
        /// <returns>False if the operation is not possible, true otherwise.</returns>
        bool OnEnter(Player player);

        /// <summary>
        /// Player calls this function if she fully reached the face.
        /// </summary>
        void OnReached(Player player);
    }
}
