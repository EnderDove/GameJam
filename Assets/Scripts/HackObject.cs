using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace Game
{
    public class HackObject : MonoBehaviour
    {
        [SerializeField] Slider slider;
        [SerializeField] Image fillImage;
        [SerializeField] Image backroundImage;
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

        private void EndHacking()
        {
            Debug.Log("Hacked");
            canvas.enabled = false;
        }

        private IEnumerator Hack()
        {
            
            slider.value = 0;
            fillImage.color = Color.red;
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
                StartCoroutine(ChangeColor());
            }
        }

        private IEnumerator ChangeColor()
        {
            while (fillImage.color.g <= 0.99f)
            {
                slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);

                fillImage.color = Color.Lerp(fillImage.color, Color.green, Time.fixedDeltaTime*10);
                yield return new WaitForFixedUpdate();
            }

            backroundImage.color = new Color(0, 1, 0, 0);
            yield return new WaitForSeconds(0.5f);

            while (fillImage.color.a >= 0.01f)
            {
                slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);

                fillImage.color = Color.Lerp(fillImage.color, new Color(0,1,0,0), Time.fixedDeltaTime * 10);
                yield return new WaitForFixedUpdate();
            }

            EndHacking();
        }

    }
}
