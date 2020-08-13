using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    #region Public Varibales
    public Slider HealthSlider;
    public Gradient FillGradient;
    public Image FillImage;
    #endregion

    public void SetMaxHealth (int health)
    {
        HealthSlider.maxValue = health;
        HealthSlider.value = health;

       FillImage.color = FillGradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        HealthSlider.value = health;

        FillImage.color = FillGradient.Evaluate(HealthSlider.normalizedValue);
    }

}
