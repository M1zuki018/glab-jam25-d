using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // �V���O���g��

    public List<SoundData> _bgmSounds;
    public List<SoundData> _seSounds;

    public AudioSource BGMSource => _bgmSource; // �v���p�e�B
    private AudioSource _bgmSource;

    public AudioSource SESource => _seSource;
    private AudioSource _seSource;

    private float _fadeSpeed;
    private float _fadeTime = 1f;
    private float _startVolume;
    private bool _isFading;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // ���g�̃Q�[���I�u�W�F�N�g��AudioSource�R���|�[�l���g��ǉ�����
        _bgmSource = gameObject.AddComponent<AudioSource>();
        _seSource = gameObject.AddComponent<AudioSource>();
    }
    private void Update()
    {
        if (_isFading)
        {
            FadeBGM();
        }
    }

    /// <summary>
    /// BGM���Đ�����
    /// </summary>
    public void PlayBGM(string name)
    {
        SoundData data = _bgmSounds.Find(sound => sound._name == name);

        if (data != null)
        {
            // �f�[�^������������A�o�^���ꂽ�f�[�^��BGM�p��AudioSource�ɓK�p����
            BGMSource.clip = data._clip;
            BGMSource.volume = data._volume;
            BGMSource.pitch = data._pitch;
            BGMSource.loop = true;
            BGMSource.Play();
        }
    }

    /// <summary>
    /// SE��1��Đ�����
    /// </summary>
    public void PlaySE(string name)
    {
        SoundData data = _seSounds.Find(sound => sound._name == name);

        if (data != null)
        {
            // �f�[�^������������ASE�p��AudioSource��SE���Đ�
            _seSource.PlayOneShot(data._clip, data._volume);
        }
    }

    /// <summary>
    /// �Đ�����SE�𒆒f���āA�w���SE���Đ�����
    /// </summary>
    /// <param name="name"></param>
    public void PlaySEInterrupt(string name)
    {
        if (_seSource.isPlaying)
        {
            _seSource.Stop();
        }
        PlaySE(name);
    }

    /// <summary>
    /// BGM���t�F�[�h�A�E�g������
    /// </summary>
    public void FadeBGM()
    {
        _startVolume = BGMSource.volume;
        _fadeSpeed = _startVolume / _fadeTime;
        BGMSource.volume -= _fadeSpeed * Time.deltaTime;

        if (BGMSource.volume < 0.14f)
        {
            BGMSource.volume = 0;
            BGMSource.Stop();
            BGMSource.volume = _startVolume;
            _isFading = false;
        }
    }

    /// <summary>
    /// BGM���t�F�[�h�C��������
    /// </summary>
    public void FadeInBGM()
    {
        _startVolume = 0f;
        _fadeSpeed = 0.3f;
        PlayBGM("bossFight");
        BGMSource.volume += _fadeSpeed * Time.deltaTime;

        if (BGMSource.volume > 0.6f)
        {
            BGMSource.volume = 0.6f;
        }
    }

    /// <summary>
    /// �t�F�[�h�A�E�g���J�n����
    /// </summary>
    public void OnFading() => _isFading = true; // �����_��
}
