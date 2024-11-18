using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    [SerializeField] private float posX;
    [SerializeField] private float posY;
    [SerializeField] private float posZ;

    public PlayerData(Vector3 position)
    {
        posX = position.x;
        posY = position.y;
        posZ = position.z;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(posX, posY, posZ);
    }
}
