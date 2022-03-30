using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public float pitchValue = 0;
    private float[] playerPos = new float[3];

    public void SetPlayerPos(Vector3 pos)
    {
        playerPos[0] = pos.x;
        playerPos[1] = pos.y;
        playerPos[2] = pos.z;
    }

    public Vector3 GetPlayerPos()
    {
        return new Vector3(playerPos[0], playerPos[1], playerPos[2]);
    }
}
