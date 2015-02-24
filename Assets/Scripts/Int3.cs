using System;
using UnityEngine;

/// <summary>
/// Vector of 3 integer values
/// 
/// author: Anke Friederici
/// </summary>
struct Int3
{
    // Could be done better...
    public static Int3 Zero
    {
        get { return new Int3(0, 0, 0); }
    }
    public static Int3 UnitX
    {
        get { return new Int3(1, 0, 0); }
    }
    public static Int3 UnitY
    {
        get { return new Int3(0, 1, 0); }
    }
    public static Int3 UnitZ
    {
        get { return new Int3(0, 0, 1); }
    }
    public static Int3 One
    {
        get { return new Int3(1, 1, 1); }
    }

    public int x;
    public int y;
    public int z;

    public Int3(int X, int Y, int Z)
    {
        this.x = X;
        this.y = Y;
        this.z = Z;
    }

    public Int3(Int3 vec)
    {
        this.x = vec.x;
        this.y = vec.y;
        this.z = vec.z;
    }

    /// <summary>
    /// Cast. Snapping to nearest integer value.
    /// </summary>
    /// <param name="vec"></param>
    public Int3(Vector3 vec)
    {
        this.x = (int)Math.Floor(vec.x + 0.5f);
        this.y = (int)Math.Floor(vec.y + 0.5f);
        this.z = (int)Math.Floor(vec.z + 0.5f);
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException("BananaHash!");
    }

    public override bool Equals(object obj)
    {
        throw new NotImplementedException("BananaEquals!");
    }

    public static Int3 operator +(Int3 vec1, Int3 vec2)
    {
        return new Int3(vec1.x + vec2.x, vec1.y + vec2.y, vec1.z + vec2.z);
    }

    public static Int3 operator -(Int3 vec1, Int3 vec2)
    {
        return new Int3(vec1.x - vec2.x, vec1.y - vec2.y, vec1.z - vec2.z);
    }

    public static Int3 operator *(Int3 vec1, Int3 vec2)
    {
        return new Int3(vec1.x * vec2.x, vec1.y * vec2.y, vec1.z * vec2.z);
    }

    public static Int3 operator /(Int3 vec1, Int3 vec2)
    {
        return new Int3(vec1.x / vec2.x, vec1.y / vec2.y, vec1.z / vec2.z);
    }

    public static Int3 operator *(float multiply, Int3 vec2)
    {
        return new Int3((int)(multiply * vec2.x), (int)(multiply * vec2.y), (int)(multiply*vec2.z));
    }

    public static Int3 operator *(Int3 vec2, float multiply)
    {
        return new Int3((int)(multiply * vec2.x), (int)(multiply * vec2.y), (int)(multiply * vec2.z));
    }

    public static Int3 operator *(int multiply, Int3 vec2)
    {
        return vec2 * multiply;
    }

    public static Int3 operator /(Int3 vec2, float f)
    {
        return new Int3((int)(vec2.x / f), (int)(vec2.y / f), (int)(vec2.z / f));
    }

    public static Int3 operator /(Int3 vec2, int f)
    {
        return new Int3((int)(vec2.x / f), (int)(vec2.y / f), (int)(vec2.z / f));
    }

    public static bool operator ==(Int3 vec1, Int3 vec2)
    {
        return !(vec1 != vec2);
    }

    public static bool operator !=(Int3 vec1, Int3 vec2)
    {
        return (vec1.x != vec2.x || vec1.y != vec2.y || vec1.z != vec2.z);
    }

    public static Int3 operator %(Int3 vec, int i)
    {
        return new Int3(vec.x % i, vec.y % i, vec.z % i);
    }

    public static Int3 operator %(Int3 vec, Int3 vec2)
    {
        return new Int3((vec.x + vec2.x) % vec2.x, (vec.y + vec2.y) % vec2.y, (vec.z + vec2.z) % vec2.z);
    }

    public Vector3 Lerp(Int3 vec, float t)
    {
        return (1 - t) * (Vector3)this + t * (Vector3)vec;
    }

    public static Vector3 Lerp(Int3 vec, Int3 vec2, float t)
    {
        return vec.Lerp(vec2, t);
    }

    /// <summary>
    /// Aka distance in 4 neighbourhood.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CityblockDistance(Int3 other)
    {
        return Math.Abs(other.x - this.x) + Math.Abs(other.y + this.y);
    }

    /// <summary>
    /// Aka distance in 8 neighbourhood.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int ChessboardDistance(Int3 other)
    {
        return Math.Max(Math.Abs(other.x - this.x), Math.Abs(other.y - this.y));
    }

    public override String ToString()
    {
        return "[X= " + this.x + "; Y= " + this.y + "; Z= " + this.z + "]";
    }

    public bool Equals(Int3 other)
    {
        return (this.x == other.x) && (this.y == other.y);
    }

    public static explicit operator Vector2(Int3 vec)
    {
        return new Vector2(vec.x, vec.y);
    }

    public static explicit operator Vector3(Int3 vec)
    {
        return new Vector3(vec.x, vec.y, vec.z);
    }

    public static Int3 Min(Int3 vec1, Int3 vec2)
    {
        return new Int3(Math.Min(vec1.x, vec2.x), Math.Min(vec1.y, vec2.y), Math.Min(vec1.z, vec2.z));
    }

    public static Int3 Max(Int3 vec1, Int3 vec2)
    {
        return new Int3(Math.Max(vec1.x, vec2.x), Math.Max(vec1.y, vec2.y), Math.Max(vec1.z, vec2.z));
    }
}