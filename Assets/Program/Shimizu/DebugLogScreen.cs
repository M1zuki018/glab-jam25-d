using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Static Member Value:" + StaticMember.Index);
        //確認用、現在StaticMemberに入っているintを確認する。シーンに入れてロード後にコンソール見て使う
    }

}
