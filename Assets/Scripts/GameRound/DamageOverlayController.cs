using UnityEngine;
using UnityEngine.UI;

namespace MRD
{
    public class DamageOverlayController : MonoBehaviour
    {
        private Image image;
        private float maxOpacity;
        private float opacity;
        private float timer;
        static float defalutRedLineY = 1.5f;
        static float defaultHeight = 12.96f;

        private void Start()
        {
            image = GetComponent<Image>();
        }

        private void Update()
        {
            if (opacity <= 0f) return;

            timer -= Time.deltaTime * RoundManager.Inst.playSpeed;
            if (timer <= 0f)
            {
                opacity -= maxOpacity * Time.deltaTime * RoundManager.Inst.playSpeed;
                if (opacity <= 0f)
                    opacity = 0f;
                image.color = new Color(1f, 1f, 1f, opacity);
            }
        }

        public void SetDamageOverlay(float damageStrength)
        {
            float targetStrength = damageStrength < 1f ? damageStrength : 1f;
            if (targetStrength < opacity) return;

            timer = 0.5f;
            maxOpacity = opacity = targetStrength;
            image.color = new Color(1f, 1f, 1f, opacity);
        }
        public void AdjustSize()
        {
            var redLineY = RoundManager.Inst.Grid.RedLineY;
            var newHeight = defaultHeight - (redLineY - defalutRedLineY);
            ((RectTransform)transform).sizeDelta = new Vector2(0f, newHeight);
        }
    }
}
