using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<int> OnBlocksCountUpdated;

    private List<Block> _blocksCount;

    private void Start()
    {
        StartCoroutine(CheckBlocksCount());
    }

    public void SetBlocks(List<Block> blocks)
    {
        _blocksCount = blocks;
        OnBlocksCountUpdated?.Invoke(_blocksCount.Count);
    }

    private IEnumerator CheckBlocksCount()
    {
        while (_blocksCount.Count > 0)
        {
            for (int i = 0; i < _blocksCount.Count; i++)
            {
                if (_blocksCount[i] == null)
                {
                    _blocksCount.RemoveAt(i);
                    OnBlocksCountUpdated?.Invoke(_blocksCount.Count);
                }
            }
            yield return null;

        }

        if (_blocksCount.Count == 0)
        {

        }
    }
}
