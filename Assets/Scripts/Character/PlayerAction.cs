using System.Collections;
using UnityEngine;


namespace Game
{
    public class PlayerAction : MonoBehaviour
    {
        private bool canHack = true;


        #region Hacking
        private void Hack()
        {
            canHack = false;
            //Hacking
            StartCoroutine(CooldownHacking(10));
        }

        private IEnumerator CooldownHacking(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            canHack = true;
        }
        #endregion
    }
}
