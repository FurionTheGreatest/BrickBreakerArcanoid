using TMPro;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _BestScoreText;
    private LevelLogic _levelLogic;
    private const string BEST_SCORE_KEY = "BestScore";
    private int _currentScore;
    private int _currentBestScore;
    private void Awake()
    {
        _levelLogic = FindObjectOfType<LevelLogic>();

        _currentBestScore = PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);
    }

    private void Start()
    {
        _BestScoreText.text = _currentBestScore.ToString();
    }

    private void UpdateScore()
    {
        _currentScore++;
        _scoreText.text = _currentScore.ToString();
        if(_currentScore < _currentBestScore) return;
        UpdateBestScore();
    }

    private void UpdateBestScore()
    {
        _currentBestScore = _currentScore;
        _BestScoreText.text = _currentBestScore.ToString();
        PlayerPrefs.SetInt(BEST_SCORE_KEY,_currentBestScore);
        PlayerPrefs.Save();
    }

    private void OnEnable()
    {
        _levelLogic.OnBrickDestroyed += UpdateScore;
    }

    private void OnDisable()
    {
        _levelLogic.OnBrickDestroyed -= UpdateScore;
    }
}
