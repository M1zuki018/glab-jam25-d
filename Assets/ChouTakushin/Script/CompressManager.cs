using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class CompressManager : MonoBehaviour
{
    [SerializeField, Tooltip("患者の湿布を格納する子オブジェクト")]
    GameObject _patiendCompresses = default;

    /// <summary>
    /// 貼り薬を全て消去する
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
