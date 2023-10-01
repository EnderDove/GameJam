using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace Game
{
    public class HackObject : MonoBehaviour
    {
        [SerializeField] Slider slider;
        private Canvas canvas;
        public bool IsHackable = true;

        private void Start ()
        {
            canvas = slider.GetComponentInParent<Canvas>();
            canvas.enabled = false;
        }

        public void StartHacking()
        {
            if (!IsHackable)
                return;
            StartCoroutine(Hack());
        }

        public void AbortHacking()
        {
            canvas.enabled = false;
            StopCoroutine(Hack());
            slider.value = 0;
        }

        private IEnumerator Hack()
        {
            slider.value = 0;
            while (!Player.PlayerInstance.inputHandler.InteractInput)
                yield return null;

            canvas.enabled = true;
            float _time = 0;
            float _endTime = Player.PlayerInstance.playerState.hackingProgression.keys[^1].time;
            while (_time <= _endTime)
            {
                slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);

                _time += Player.PlayerInstance.inputHandler.InteractInput ? Time.fixedDeltaTime : -Time.fixedDeltaTime / 5f;
                float hackingValue = Player.PlayerInstance.playerState.hackingProgression.Evaluate(_time);
                slider.value = hackingValue;
                yield return new WaitForFixedUpdate();
            }

            if (slider.value == 1)
            {
                IsHackable = false;
                Debug.Log("Hacked");
                //Hacked
            }
        }

    }
}
