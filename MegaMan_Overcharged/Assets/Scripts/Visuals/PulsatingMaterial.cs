using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PulsatingMaterial : NetworkBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private bool _infinite;
    [SerializeField] private int _times;
    [SerializeField] private float _timeToWait;
    private float _displacement = 0;
    private int _currentTimes;
    
    [Range(0,1)]
    [SerializeField] private float speed;

    private void Start()
    {
        StartCoroutine(WaitForTimer());      
    }

    IEnumerator Scroll()
    {

        if (_infinite)
        {
            while (_displacement > -1)
            {
                Displace();
                yield return new WaitForFixedUpdate();
            }
            _displacement = 0;

            yield return Scroll();
        }

        else if(!_infinite)
        {
            if (_currentTimes < _times)
            {
                while (_displacement > -1)
                {
                    Displace();
                    yield return new WaitForFixedUpdate();
                }
                _displacement = 0;

                _currentTimes++;
                yield return Scroll();
                yield break;
            }
        }
    }


    private void Displace()
    {
        _displacement -= speed;
        _meshRenderer.material.mainTextureOffset = new Vector2(0, _displacement);
    }

    private IEnumerator WaitForTimer()
    { 
        while(_timeToWait > 0)
        {
            _timeToWait -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield return Scroll();
    }
}
