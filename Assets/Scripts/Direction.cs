using UnityEngine;

/// <summary>
/// 6 possible, discrete directions, with index (for array access) and 3d direction.
/// </summary>
public struct Direction
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


    public static readonly Direction POS_X = new Direction(0);
    public static readonly Direction NEG_X = new Direction(1);
    public static readonly Direction POS_Y = new Direction(2);
    public static readonly Direction NEG_Y = new Direction(3);
    public static readonly Direction POS_Z = new Direction(4);
    public static readonly Direction NEG_Z = new Direction(5);

    public static readonly Direction[] DIRS = new Direction[]{ POS_X, NEG_X, POS_Y, NEG_Y, POS_Z, NEG_Z };


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

    static private Direction GetDirection(Vector3 vectorDir)
    {
        int maxDim = 0;
        if (Mathf.Abs(vectorDir[0]) < Mathf.Abs(vectorDir[1]))
            maxDim = Mathf.Abs(vectorDir[1]) < Mathf.Abs(vectorDir[2]) ? 2 : 1;
        else
            maxDim = Mathf.Abs(vectorDir[2]) < Mathf.Abs(vectorDir[0]) ? 2 : 0;

        return DIRS[maxDim * 2 + Mathf.Sign(vectorDir[maxDim]) < 0 ? 0 : 1];
    }

    private Direction(int index)
    {
        this.index = index;
    }
}