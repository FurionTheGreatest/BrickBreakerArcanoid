using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Audio Sources
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource breakableYellowHit;
    [SerializeField] private AudioSource breakableCyanHit;
    [SerializeField] private AudioSource breakableRedHit;
    [SerializeField] private AudioSource breakablePinkHit;
    [SerializeField] private AudioSource breakableGrayHit;
    [SerializeField] private AudioSource breakableBlueHit;
    [SerializeField] private AudioSource unbreakableHit;
    #endregion
    private AssetLoader _assetLoader;

    #region Sound Keys

    private const string SOUND1 = "sound1";
    private const string SOUND2 = "sound2";
    private const string SOUND3 = "sound3";
    private const string SOUND4 = "sound4";
    private const string SOUND5 = "sound5";
    private const string SOUND6 = "sound6";
    private const string UNBREAKABLE_SOUND = "unbreakable";
    

    #endregion
    public static AudioManager instance; // Only one instance of AudioManager can exist at the same time
    private void Awake()
    {
        _assetLoader = FindObjectOfType<AssetLoader>();

        // If there is no instance of AudioManger currently
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private async void Start()
    {
        var result = _assetLoader.LoadResourceByReference<AudioClip>(_assetLoader.backgroundAudioReference);
        var clip = await result;
        backgroundMusic.clip =  clip as AudioClip;
        
        PlayMusic();
    }

    private void PlayMusic()
    {
        backgroundMusic.Play();
        backgroundMusic.loop = true;
    }
    public void PlayBrickSound(BrickType type)
    {
        switch (type)
        {
            case BrickType.Yellow:
                PlayHitSound(SOUND1, breakableYellowHit);
                break;
            case BrickType.Cyan:
                PlayHitSound(SOUND2, breakableCyanHit);
                break;
            case BrickType.Red:
                PlayHitSound(SOUND3, breakableRedHit);
                break;
            case BrickType.Pink:
                PlayHitSound(SOUND4, breakablePinkHit); 
                break;
            case BrickType.Gray:
                PlayHitSound(SOUND5, breakableGrayHit);
                break;
            case BrickType.Blue:
                PlayHitSound(SOUND6, breakableBlueHit);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, "There is no such brick type");
        }
    }

    public void PlayUnbreakable()
    {
        PlayHitSound(UNBREAKABLE_SOUND, unbreakableHit);
    }

    private async void PlayHitSound(string key, AudioSource sourceToAssign)
    {
        var result = _assetLoader.LoadResourceByKey<AudioClip>(key);
        var clip = await result;
        sourceToAssign.clip =  clip as AudioClip;
        sourceToAssign.Play();
    }
}
