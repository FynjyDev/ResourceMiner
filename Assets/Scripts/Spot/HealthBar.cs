using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject sliderParent;
    public Slider hpSlider;

    public void Init(float hpValue)
    {
        hpSlider.maxValue = hpValue;
        hpSlider.value = hpValue;
    }

    public void ChangeValue(float hpValue)
    {
        StopAllCoroutines();

        sliderParent.SetActive(true);


        ChangeSliderValue(hpSlider, hpValue, 1);
    }

    public void ChangeSliderValue(Slider slider, float hpValue, float time)
    { 
        StartCoroutine(OnChangeSliderValue(slider, hpValue, time));
    }

    public IEnumerator OnChangeSliderValue(Slider slider, float hpValue, float seconds)
    {
        float animationTime = 0f;
        while (animationTime < seconds)
        {
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            slider.value = Mathf.Lerp(slider.value, hpValue, lerpValue);
            yield return null;
        }
    }
}
