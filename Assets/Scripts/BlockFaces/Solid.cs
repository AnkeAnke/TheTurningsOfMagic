
namespace BlockFaces
{
    class Solid : MonoBehaviour, BlockFace
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
