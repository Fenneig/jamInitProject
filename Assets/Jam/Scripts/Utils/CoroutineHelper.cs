using System.Collections;
using UnityEngine;

namespace Jam.Scripts.Utils
{
    public class CoroutineHelper : MonoBehaviour
    {
        public void RunCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}
