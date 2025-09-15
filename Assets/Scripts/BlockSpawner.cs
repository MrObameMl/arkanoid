using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [Header(" ----------Grid Spawner----------")]
    public Block BlockPrefab1;
    public int Colomns;
    public int Rows;
    public Vector2 StartSpawnPoint;
    [Range(1, 10)]public int OffsetY;
    [Range(1, 10)]public int OffsetX;
    [Space(10)]
    [Header(" ----------Spiral Spawner----------")]
    public Block BlockPrefab2;
    public float AngleStep;
    public float StartRadius;
    public float NumberOfSpiralBlocks;
    public float RadiusStep;
    public Vector2 StartSpiralPosition;
    [Space(10)]
    [Header("----------Circle Spawner----------")]
    public Block BlockPrefab3;
    public int NumberOfCircleBlocks;
    public float CircleRadius;
    public Vector2 StartCirclePosition;

    

    public void SpawnSpiralBlocks()
    {
        float currentAngle = 0;
        float currentRadius = StartRadius;
        for (int block = 0; block < NumberOfSpiralBlocks; block++)
        {
            float angleRad = currentAngle * Mathf.Deg2Rad;
            float x = currentRadius * Mathf.Cos(angleRad);
            float y = currentRadius * Mathf.Sin(angleRad);
            Vector2 spawnPosition = StartSpiralPosition + new Vector2(x, y);
            Instantiate(BlockPrefab2, spawnPosition, Quaternion.identity);
            currentAngle += AngleStep;
            currentRadius += RadiusStep;
        }
    }

    public void SpawnGridBlocks()
    {
        for (int colomn = 0; colomn < Colomns; colomn++)
        {
            for (int row = 0; row < Rows; row++)
            {
                Vector3 spawnPosition = StartSpawnPoint + new Vector2(colomn * OffsetX, row * OffsetY); 
                Instantiate(BlockPrefab1, spawnPosition, Quaternion.identity);
            }
        }
    }    
    
    public void SpawnCircleBlocks()
    {
        for (int block = 0; block <NumberOfCircleBlocks; block++)
        {
            float angle = 360 / NumberOfCircleBlocks;
            float angleRad = angle * block * Mathf.Deg2Rad;
            float x = CircleRadius * Mathf.Cos(angleRad);
            float y = CircleRadius * Mathf.Sin(angleRad);
            Vector2 spawnPosition = StartCirclePosition + new Vector2(x, y);
            Instantiate(BlockPrefab3, spawnPosition, Quaternion.identity);
        }

    }
}
