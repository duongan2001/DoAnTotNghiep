using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public int spawnCase;
    public GameObject enemy;
    public int enemyNumber;
    public float[] enemySpawnPosX;
    public float[] enemySpawnPosY;
    public float[] enemyStopPosX;
    public float[] enemyStopPosY;

}
