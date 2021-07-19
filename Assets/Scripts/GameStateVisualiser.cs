using System;
using UnityEngine;

public class GameStateVisualiser : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _winPanel;
    
    private LevelLogic _levelLogic;
    private void Awake()
    {
        _levelLogic = FindObjectOfType<LevelLogic>();
    }

    private void ShowGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }

    private void ShowWinPanel()
    {
        _winPanel.SetActive(true);
    }
    
    private void OnEnable()
    {
        BallHealth.OnLivesEnded += ShowGameOverPanel;
        _levelLogic.OnBricksEnded += ShowWinPanel;
    }

    private void OnDisable()
    {
        BallHealth.OnLivesEnded -= ShowGameOverPanel;
        _levelLogic.OnBricksEnded -= ShowWinPanel;
    }
}
