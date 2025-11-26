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

    private static int _blockBehaviourIndex;

    private void OnEnable()
    {
        float min = 20f / 255f;
        float max = 50f / 255f;
        Color newColor = new Color(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
        Camera.main.backgroundColor = newColor;

        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        SetUpLevel(levelIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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

        int spawnerBehaviourIndex;

        do
        {
            _blockBehaviourIndex = Random.Range(0, 3);
            spawnerBehaviourIndex = Random.Range(0, 3);
        }
        while (_usedCombinations.Contains((_blockBehaviourIndex, spawnerBehaviourIndex)));
        _usedCombinations.Add((_blockBehaviourIndex, spawnerBehaviourIndex));

        //blockBehaviour[_blockBehaviourIndex]();
        spawnBehaviour[spawnerBehaviourIndex]();
    }

    public static int ReturnBehaviourIndex()
    {
        return _blockBehaviourIndex;
    }
}
