using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Block Block;
    public BlockSpawner BlockSpawner;

    private List<(int, int)> _usedCombinations = new List<(int, int)>();
    private delegate void BlockBehaviour();
    private delegate void SpawnBehaviour();   

    private void Start()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        SetUpLevel(levelIndex);
    }

    private void SetUpLevel(int levelIndex)
    {
        if (levelIndex > 3)
        {
            return;
        }

        BlockBehaviour[] blockBehaviour = new BlockBehaviour[]
        {
            Block.Behaviour_1, Block.Behaviour_2, Block.Behaviour_3
        };

        SpawnBehaviour[] spawnBehaviour = new SpawnBehaviour[]
        {
            BlockSpawner.SpawnCircleBlocks, BlockSpawner.SpawnGridBlocks, BlockSpawner.SpawnSpiralBlocks
        };

        int blockBehaviourIndex, spawnerBehaviourIndex;

        do
        {
            blockBehaviourIndex = Random.Range(0, 3);
            spawnerBehaviourIndex = Random.Range(0, 3);
        }
        while (_usedCombinations.Contains((blockBehaviourIndex, spawnerBehaviourIndex)));
        _usedCombinations.Add((blockBehaviourIndex, spawnerBehaviourIndex));

        //blockBehaviour[blockBehaviourIndex]();
        spawnBehaviour[spawnerBehaviourIndex]();
    }
}
