﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShunpoBar : MonoBehaviour {

    float maxWidth;
    KatarinaShunpo katShunpo;
    RectTransform rectTransform;

    void Start() {
        katShunpo = Katarina.instance.GetComponent<KatarinaShunpo>();
        rectTransform = GetComponent<RectTransform>();
        maxWidth = rectTransform.sizeDelta.x;
    }

    void Update() {
        float currentWidth = katShunpo.cooldown / katShunpo.cooldownTime * maxWidth;
        rectTransform.sizeDelta = new Vector2(currentWidth, rectTransform.sizeDelta.y);
    }
}
