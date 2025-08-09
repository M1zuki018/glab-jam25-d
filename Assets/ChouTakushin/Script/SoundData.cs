using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SoundData
{
    public string _name; // �����f�[�^�̖��O
    public AudioClip _clip; // �f�[�^
    [Range(0, 1)] public float _volume = 1.0f; // ����
    [Range(0, 1)] public float _pitch = 1.0f; // �s�b�`
}
