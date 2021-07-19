using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelLogic : MonoBehaviour
{
    public Action OnBrickDestroyed;
    public Action OnBricksEnded;
    [SerializeField] private List<BreakableBrick> _levelBricks;
    private void Start()
    {
        _levelBricks = FindBricks<BreakableBrick>();
    }

    private List<T> FindBricks<T>() where T : BaseBrick
    {
        return FindObjectsOfType<T>().ToList();
    }

    private void OnBrickDestroy(BaseBrick obj)
    {
        _levelBricks.Remove(obj.GetComponent<BreakableBrick>());
        OnBrickDestroyed?.Invoke();
        if(_levelBricks.Count >0) return;
        OnBricksEnded?.Invoke();
    }
    
    private void OnEnable()
    {
        BaseBrick.BeforeDestroy += OnBrickDestroy;
    }

    private void OnDisable()
    {
        BaseBrick.BeforeDestroy -= OnBrickDestroy;
    }
}
