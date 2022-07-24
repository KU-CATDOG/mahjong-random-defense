using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MRD{
public class DamageOverlayController : MonoBehaviour
{
    public Image image;
    private float timer = 0f;
    private float opacity = 0f;
    private float maxOpacity = 0f;

    public void SetDamageOverlay(float damageStrength)
    {
        var targetStrength = damageStrength < 1f? damageStrength : 1f;
        if(targetStrength < opacity) return;

        timer = 0.5f;
        maxOpacity = opacity = targetStrength;
        image.color = new Color(1f, 1f, 1f, opacity);
    }

    void Start()
    {
        image = GetComponent<Image>();
    }
    void Update()
    {
        if(opacity <= 0f) return;

        timer -= Time.deltaTime * RoundManager.Inst.playSpeed;
        if(timer <= 0f)
        {
            opacity -= maxOpacity * Time.deltaTime * RoundManager.Inst.playSpeed;
            if (opacity <= 0f) 
                opacity = 0f;
            image.color = new Color(1f, 1f, 1f, opacity);
        }
    }
}
}