using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class BaseBrick: MonoBehaviour
{
    public static Action<BaseBrick> BeforeDestroy;
    public BrickType brickType;
    private SpriteRenderer _sprite;
    private BrickStats _brickStats;
    private AssetLoader _assetLoader;

    private void Awake()
    {
        _assetLoader = FindObjectOfType<AssetLoader>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private async void Start()
    {
        StartSetup(brickType);
        var material = await _assetLoader.LoadResourceByKey<Material>(brickType.ToString());
        if (material != null)
        {
            _sprite.material = (Material) material;
            Addressables.Release(material);
        }
    }

    protected void ReduceHealth(bool instantDeath = false)
    {
        if (instantDeath)
            _brickStats.health = 0;
        else
            _brickStats.health--;
        CheckForDestroy();
    }

    private void CheckForDestroy()
    {
        if (_brickStats.health > 0) return;
        var actionOnDestroy = GetComponent<ActionOnBrickDestroy>();
        if (actionOnDestroy != null)
        {
            actionOnDestroy.DoAction();
        }
        BeforeDestroy?.Invoke(this);
        gameObject.SetActive(false);
    }
    private void StartSetup(BrickType type)
    {
        switch (type)
        {
            case BrickType.Yellow:
                _brickStats = new BrickStats(1);
                break;
            case BrickType.Cyan:
                _brickStats = new BrickStats(2);
                break;
            case BrickType.Red:
                _brickStats = new BrickStats(2);
                break;
            case BrickType.Pink:
                _brickStats = new BrickStats(2);
                break;
            case BrickType.Gray:
                _brickStats = new BrickStats(3);
                break;
            case BrickType.Blue:
                _brickStats = new BrickStats(3);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, "There is no such brick type");
        }
    }
}

public enum BrickType
{
    Yellow,
    Cyan,
    Red,
    Pink,
    Gray,
    Blue
}
public struct BrickStats
{
    public int health;

    public BrickStats(int health)
    {
        this.health = health;
    }
}
