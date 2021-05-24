using System;
using System.Collections;
using UnityEngine;

namespace Core.Player
{
    public static class Timer
    {
        public static Coroutine StartTimer(MonoBehaviour target, Action<int> TimerCallBack, params int[] waitTimes)
        {
            return target.StartCoroutine(Charge(waitTimes, TimerCallBack));
        }

        private static IEnumerator Charge(int[] items, Action<int> TimerCallBack)
        {
            int chargeCounter = 0;
            for (int i = 0; i < items.Length;)
            {
                while (items[i] > chargeCounter)
                {
                    Debug.Log("Charging");
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
