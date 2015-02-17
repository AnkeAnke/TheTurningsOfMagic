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
            minPos = Int3.Min(minPos, new Int3(block.transform.position));
            maxPos = Int3.Max(maxPos, new Int3(block.transform.position));
        }

        Origin = minPos;
        Size = maxPos - minPos;

        // Fill the array.
        Blocks = new Block[Size.x, Size.y, Size.z];

        foreach (Block block in blocks)
        {
            Int3 pos = new Int3(block.transform.position);
            Blocks[pos.x, pos.y, pos.z] = block;
        }
    }

    public Block GetBlock(Int3 pos)
    {
        return Blocks[pos.x, pos.y, pos.z];
    }

    public bool IsSolid(Int3 pos)
    {
        return GetBlock(pos) != null;
    }
}

