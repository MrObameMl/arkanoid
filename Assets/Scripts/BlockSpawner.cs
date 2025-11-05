using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public Block[] BlockPrefabs;

    [Header(" ----------Grid Spawner----------")]
    public int Colomns;
    public int Rows;
    public Vector2 StartSpawnPoint;
    [Range(1, 10)]public int OffsetY;
    [Range(1, 10)]public int OffsetX;
    [Space(10)]
    [Header(" ----------Spiral Spawner----------")]
    public float AngleStep;
    public float StartRadiusX;
    public float StartRadiusY;
    public float NumberOfSpiralBlocks;
    public float RadiusStepX;
    public float RadiusStepY;
    public Vector2 StartSpiralPosition;
    [Space(10)]
    [Header("----------Circle Spawner----------")]
    public int NumberOfCircleBlocks;
    public float CircleRadius;
    public Vector2 StartCirclePosition1;
    public Vector2 StartCirclePosition2;
    public Vector2 StartCirclePosition3;

    private List<Block> _blocksCount = new List<Block>();
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
    }

    public void SpawnSpiralBlocks()
    {
        float currentAngle = 0;
        float currentRadiusX = StartRadiusX;
        float currentRadiusY = StartRadiusY;
        Block blockPrefab = BlockPrefabs[Random.Range(0, BlockPrefabs.Length)];
        for (int block = 0; block < NumberOfSpiralBlocks; block++)
        {
            float angleRad = currentAngle * Mathf.Deg2Rad;
            float x = currentRadiusX * Mathf.Cos(angleRad);
            float y = currentRadiusY * Mathf.Sin(angleRad);
            Vector2 spawnPosition = StartSpiralPosition + new Vector2(x, y);
            var blockInstance = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
            currentAngle += AngleStep;
            currentRadiusX += RadiusStepX;
            currentRadiusY += RadiusStepY;

            _blocksCount.Add(blockInstance);
            _gameManager.SetBlocks(_blocksCount);
        }
    }

    public void SpawnGridBlocks()
    {
        Block blockPrefab = BlockPrefabs[Random.Range(0, BlockPrefabs.Length)];
        for (int colomn = 0; colomn < Colomns; colomn++)
        {
            for (int row = 0; row < Rows; row++)
            {
                Vector3 spawnPosition = StartSpawnPoint + new Vector2(colomn * OffsetX, row * OffsetY); 
                var blockInstance = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);

                _blocksCount.Add(blockInstance);
                _gameManager.SetBlocks(_blocksCount);
            }
        }
    }    
    
    public void SpawnCircleBlocks()
    {
        Block blockPrefab = BlockPrefabs[Random.Range(0, BlockPrefabs.Length)];
        void Spawn(Vector3 position)
        {            
            for (int block = 0; block < NumberOfCircleBlocks; block++)
            {
                float angle = 360 / NumberOfCircleBlocks;
                float angleRad = angle * block * Mathf.Deg2Rad;
                float x = CircleRadius * Mathf.Cos(angleRad);
                float y = CircleRadius * Mathf.Sin(angleRad);
                Vector2 spawnPosition = position + new Vector3(x, y);
                var blockInstance = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);

                _blocksCount.Add(blockInstance);
                _gameManager.SetBlocks(_blocksCount);
            }
        }

        Spawn(StartCirclePosition1);
        Spawn(StartCirclePosition2);
        Spawn(StartCirclePosition3);
    }
}
