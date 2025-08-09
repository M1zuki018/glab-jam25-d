using UnityEngine;
using UnityEngine.EventSystems;

public class GauzeImage : MonoBehaviour
{
    CompressSpawner _compressSpawner;
    // Start is called before the first frame update
    void Start()
    {
        _compressSpawner = FindObjectOfType<CompressSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                _compressSpawner.SelectedCompress = EnumCompressType.Guaze;
                _compressSpawner.CanSpawn = true;
                Debug.Log("ガーゼにクリックした");
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            _compressSpawner.CanSpawn = false;
        }
    }
}
