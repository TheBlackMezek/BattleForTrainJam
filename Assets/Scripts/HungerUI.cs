using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerUI : MonoBehaviour {

    [SerializeField]
    private Hunger hunger;
    [SerializeField]
    private RectTransform bar;



    private void Update()
    {
        bar.localScale = new Vector3(hunger.hunger / hunger.MaxHunger, 1f, 1f);
    }

}
