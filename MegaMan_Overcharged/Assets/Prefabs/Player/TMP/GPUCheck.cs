using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        print(SystemInfo.graphicsDeviceName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
