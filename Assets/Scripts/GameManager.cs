using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<int> OnBlocksCountUpdated;

    private List<Block> _blocksCount;

    public void SetBlocks(List<Block> blocks)
    {
        _blocksCount = blocks;
        StartCoroutine(CheckBlocksCount());
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

        }
        yield return null;
        Debug.Log("END After Coroutine");
        Debug.Log("END After Coroutine   X2");
        Debug.Log("END After While");
        if (_blocksCount.Count == 0)
        {
            Debug.Log(_blocksCount.Count);
            Debug.Log("END");

        }
    }
}
