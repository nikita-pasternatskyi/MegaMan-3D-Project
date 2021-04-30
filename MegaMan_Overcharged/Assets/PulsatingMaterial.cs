using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PulsatingMaterial : MonoBehaviour
{
    public Material currentMaterial;
    public bool Infinite;
    public int times;
    public float speed;
    float displacement = 0;
    int currentTimes;

    private void Start()
    {
        StartCoroutine(Displace());
    }

    IEnumerator Displace()
    {
        if (!Infinite)
        {
            while (displacement > -1)
            {
                displacement -= speed;
                currentMaterial.SetTextureOffset("_BaseMap", new Vector2(0, displacement));
                yield return new WaitForFixedUpdate();
            }
            displacement = 0;
            if (currentTimes < times)
            {
                currentTimes++;
                yield return Displace();
                yield break;
            }
        }
            
        else if(Infinite)
        {
            while (displacement > -1)
            {
                displacement -= speed;
                currentMaterial.SetTextureOffset("_BaseMap", new Vector2(0, displacement));
                yield return new WaitForFixedUpdate();
            }
            displacement = 0;
            yield return Displace();
        }
    }

}
