using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class CompressManager : MonoBehaviour
{
    [SerializeField, Tooltip("���҂̎��z���i�[����q�I�u�W�F�N�g")]
    GameObject _patiendCompresses = default;

    /// <summary>
    /// �\����S�ď�������
    /// </summary>
    public void ClearCompress()
    {
        DestroyController[] destroyControllers = _patiendCompresses.GetComponentsInChildren<DestroyController>();
        foreach (var item in _patiendCompresses.GetComponentsInChildren<DestroyController>())
        {
            item.DoDestroy();
        }
    }
}
