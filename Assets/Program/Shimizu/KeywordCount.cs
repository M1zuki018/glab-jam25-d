using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeywordCount : MonoBehaviour
{
    public static KeywordCount Instance { get; private set; }

    [SerializeField]
    private int keywordcount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // ÉVÅ[ÉìÇÇ‹ÇΩÇ¢Ç≈égÇ¢ÇΩÇ¢èÍçá
    }

    public int Getcount()
    {
        return keywordcount;
    }
    public void AddCount(int value)
    {
        keywordcount += value;
        Debug.Log("Keyword count: " + keywordcount);
    }
}