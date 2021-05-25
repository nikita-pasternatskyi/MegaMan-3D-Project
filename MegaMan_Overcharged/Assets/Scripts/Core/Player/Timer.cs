using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Player
{
    public static class Timer
    {
        public static Coroutine CreateTimer(MonoBehaviour target, Action<int> TimerCallBack, params int[] waitTimes)
        {
            return target.StartCoroutine(TimerCoroutine(waitTimes, TimerCallBack));
        }
        private static IEnumerator TimerCoroutine(int[] items, Action<int> TimerCallBack)
        {
            int chargeCounter = 0;
            for (int i = 0; i < items.Length;)
            {
                while (items[i] > chargeCounter)
                {
                    chargeCounter++;
                    yield return new WaitForFixedUpdate();
                }
                TimerCallBack.Invoke(i);
                chargeCounter = 0;
                i++;
            }
            yield break;
        }
    }

}
