using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // �O���[�o���A�N�Z�X�p�̃V���O���g���C���X�^���X
    public static SoundManager Instance { get; private set; }

    [Header("�I�[�f�B�I�\�[�X")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("BGM")]
    public AudioClip titleBgm;
    public AudioClip gameBgm;

    [Header("���ʉ�")]
    public AudioClip startButtonSfx;
    public AudioClip buttonSfx;
    public bool startButtonSfxHasPlayed = false;

    [Header("���ʐݒ�")]
    [SerializeField, Range(0f, 1f)] private float sfxVolume = 0.3f;
    [SerializeField, Range(0f, 1f)] private float musicVolume = 0.1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
        sfxVolume = 0.5f;

        ApplyVolumes();
    }

    private void Start()
    {
        foreach (var btn in GameObject.FindGameObjectsWithTag("SoundButton"))
            btn.GetComponent<Button>().onClick.AddListener(PlayButtonSFX);

        // ���݂̃V�[���ɉ�����BGM���Đ�
        Scene currentScene = SceneManager.GetActiveScene();
        PlayMusicForScene(currentScene.name);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);

        foreach (var btn in GameObject.FindGameObjectsWithTag("SoundButton"))
            btn.GetComponent<Button>().onClick.AddListener(PlayButtonSFX);
    }

    // �V�[���ǂݍ��ݎ��ɓK�؂�BGM���Đ�
    private void PlayMusicForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "TitleMenu":
                PlayMusic(titleBgm);
                break;
            case "InGame":
                musicSource.Stop();
                break;
            default:
                break;
        }
    }

    // �w�肳�ꂽ���ʂŌ��ʉ���1�񂾂��Đ�
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    // BGM���Đ��܂��͐؂�ւ��i�����ȂȂ�čĐ����Ȃ��j
    public void PlayMusic(AudioClip musicClip, bool loop = true)
    {
        if (musicSource.clip == musicClip && musicSource.isPlaying) return;

        musicSource.clip = musicClip;
        musicSource.loop = loop;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // ����̌��ʉ����Đ�����֗����\�b�h
    public void PlayStartSFX()
    {
        if (startButtonSfxHasPlayed) return;

        PlaySFX(startButtonSfx);
        startButtonSfxHasPlayed = true;
    }

    public void PlayButtonSFX() => PlaySFX(buttonSfx);


    // BGM���ʂ�ݒ肵�ĕۑ�
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    // ���ʐݒ���I�[�f�B�I�\�[�X�ɓK�p
    private void ApplyVolumes()
    {
        musicSource.volume = musicVolume;
    }
}
