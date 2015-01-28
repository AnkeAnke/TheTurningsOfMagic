using UnityEngine;

/// <summary>
/// 6 possible, discrete directions, with index (for array access) and 3d direction.
/// </summary>
struct Direction
{
    /// <summary>
    /// Direction in which this direction type points.
    /// </summary>
    public Vector3 Vector
    {
        get
        {
            return vectors[index];
        }
    }

    private static readonly Vector3[] vectors = new Vector3[6]
    {
        new Vector3( 1, 0, 0),
        new Vector3(-1, 0, 0),
        new Vector3( 0, 1, 0),
        new Vector3( 0,-1, 0),
        new Vector3( 0, 0, 1),
        new Vector3( 0, 0,-1),
    };


    static readonly Direction POS_X = new Direction(0);
    static readonly Direction NEG_X = new Direction(1);
    static readonly Direction POS_Y = new Direction(2);
    static readonly Direction NEG_Y = new Direction(3);
    static readonly Direction POS_Z = new Direction(4);
    static readonly Direction NEG_Z = new Direction(5);


    /// <summary>
    /// Internal index for array access.
    /// </summary>
    private readonly int index;

    /// <summary>
    /// Each direction has a unique index that can be used for array access.
    /// </summary>
    public static implicit operator int(Direction d)
    {
        return d.index;
    }


    private Direction(int index)
    {
        this.index = index;
    }
}