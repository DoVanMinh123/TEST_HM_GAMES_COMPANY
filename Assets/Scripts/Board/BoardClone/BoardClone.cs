using System;
using UnityEngine;

[Serializable]
public class BoardClone
{
    public int SizeX;
    public int SizeY;
    public NormalItem.eNormalType[] NormalTypes;

    public BoardClone(int sizeX, int sizeY)
    {
        SizeX = sizeX;
        SizeY = sizeY;
        NormalTypes = new NormalItem.eNormalType[sizeX * sizeY];
    }

    public void Set(int x, int y, NormalItem.eNormalType type)
    {
        NormalTypes[y * SizeX + x] = type;
    }

    public NormalItem.eNormalType Get(int x, int y)
    {
        return NormalTypes[y * SizeX + x];
    }
}
