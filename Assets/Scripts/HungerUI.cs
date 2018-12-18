using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerUI : MonoBehaviour {

    [SerializeField]
    private Hunger hunger;
    [SerializeField]
    private RectTransform bar;
    [SerializeField]
    private Image hungerImage;



    private void Update()
    {
        float hungerLevel = hunger.hunger / hunger.MaxHunger;
        float imgTransparency = hungerLevel > 0.5f ? 0f : 1f - hungerLevel * 2f;
        bar.localScale = new Vector3(hungerLevel, 1f, 1f);
        hungerImage.color = new Color(hungerImage.color.r, hungerImage.color.g, hungerImage.color.b, imgTransparency);
    }

}
