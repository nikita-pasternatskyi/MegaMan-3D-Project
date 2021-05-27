using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimator : MonoBehaviour
{
    public enum WhatAxis {
        X,Y,Z
    }
    public WhatAxis _whatAxis;
    public int AnimationDuration;
    public float ZStepOne = 1.5f;
    public float ZStepTwo = 1;
    // Start is called before the first frame update
    private void Start()
    {
        switch (_whatAxis)
        {
            case WhatAxis.X:               
                break;
            case WhatAxis.Y:
                break;
            case WhatAxis.Z:
                break;
        }
    }

    //private IEnumerator TweenScaleX()
    //{
    //    Vector3 newScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    //    for (float i = 0; i < AnimationDuration;)
    //    {
    //        i += .1f * Time.deltaTime;
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
