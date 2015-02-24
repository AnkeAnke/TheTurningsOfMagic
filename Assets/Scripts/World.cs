using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class World
{
    private static World instance;

    public Int3 Origin, Size;
    public Block[, ,] Blocks { get; private set; }

    public static World Get()
    {
        if (instance == null)
            instance = new World();
        return instance;
    }
    private World()
    {
        // Get the extents of the world.
        var blocks = Component.FindObjectsOfType<Block>();
        Int3 minPos = new Int3(500, 500, 500);
        Int3 maxPos = new Int3(-500, -500, -500);
        foreach(Block block in blocks)
        {
            Debug.Log("Block at: " + block.transform.position);
            minPos = Int3.Min(minPos, new Int3(block.transform.position));
            maxPos = Int3.Max(maxPos, new Int3(block.transform.position));
        }

        Origin = minPos;
        Size = maxPos - minPos + Int3.One;
        Debug.Log("Origin: " + minPos);
        Debug.Log("Size: " + Size);
        Debug.Log("Max Pos: " + maxPos);
        // Fill the array.
        Blocks = new Block[Size.x, Size.y, Size.z];
        //Debug.Log("Blocks dimension: " + Blocks.GetLength(0) + ", ")
        foreach (Block block in blocks)
        {
            Int3 pos = new Int3(block.transform.position) - Origin;
            Blocks[pos.x, pos.y, pos.z] = block;
        }
    }

    /// <summary>
    /// Returns the block at this position, in Unity space.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Block GetBlock(Int3 pos)
    {
        Int3 rPos = pos - Origin;
        return Blocks[rPos.x, rPos.y, rPos.z];
    }

    public bool IsSolid(Int3 pos)
    {
        Int3 relativePos = pos - Origin;
        if (relativePos % Size != relativePos)
        {
            //Debug.Log("Outside at " + pos + ", pos % Size = " + pos%Size);
            return false;
        }
        return GetBlock(pos) != null;
    }
}

