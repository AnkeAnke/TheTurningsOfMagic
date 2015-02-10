using UnityEngine;

namespace BlockFaces
{
    abstract class BlockFace : MonoBehaviour
    {
        public Direction Direction { get { return Direction.DIRS[DirectionIndex]; } }

        /// <summary>
        /// For internal use - since Unity can not serialize the Direction struct by default.
        /// </summary>
        [HideInInspector]
        public int DirectionIndex;

        /// <summary>
        /// Called when the player wants to enter this block.
        /// </summary>
        /// <returns>False if the operation is not possible, true otherwise.</returns>
        public abstract bool OnEnter(Player player);

        /// <summary>
        /// Player calls this function if she fully reached the face.
        /// </summary>
        public abstract void OnReached(Player player);
    }
}
