using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Static Member Value:" + StaticMember.Index);
        //�m�F�p�A����StaticMember�ɓ����Ă���int���m�F����B�V�[���ɓ���ă��[�h��ɃR���\�[�����Ďg��
    }

}
