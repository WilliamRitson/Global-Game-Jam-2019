using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleDestroyDelay : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var destroyTime = 5;
        Destroy(gameObject, destroyTime);
    }
}
