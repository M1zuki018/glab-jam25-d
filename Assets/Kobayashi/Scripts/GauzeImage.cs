using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GauzeImage : MonoBehaviour
{
    [SerializeField] CompressSpawner _compressSpawner;
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] GraphicRaycaster _raycaster;// Start is called before the first frame update
    void Start()
    {
        //_compressSpawner = FindObjectOfType<CompressSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //if (EventSystem.current.IsPointerOverGameObject())
            if (IsPointerOverSpecificUI(this.gameObject))
            {
                _compressSpawner.SelectedCompress = EnumCompressType.Guaze;
                _compressSpawner.CanSpawn = true;
                Debug.Log("ÉKÅ[É[Ç…ÉNÉäÉbÉNÇµÇΩ");
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            _compressSpawner.CanSpawn = false;
        }
    }
    bool IsPointerOverSpecificUI(GameObject uiElement)
    {
        PointerEventData pointerData = new PointerEventData(_eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        _raycaster.Raycast(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == uiElement)
            {
                return true;
            }
        }
        return false;
    }
}
