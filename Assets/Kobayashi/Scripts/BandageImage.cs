using UnityEngine;
using UnityEngine.EventSystems;

public class BandageImage : MonoBehaviour
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
                _compressSpawner.SelectedCompress = EnumCompressType.Bandage;
                _compressSpawner.CanSpawn = true;
                Debug.Log("„JënçpÇ…ÉNÉäÉbÉNÇµÇΩ");
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            _compressSpawner.CanSpawn = false;
        }
    }
}
