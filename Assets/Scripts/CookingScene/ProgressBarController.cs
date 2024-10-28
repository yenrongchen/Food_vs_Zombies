using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public Slider slider;

    public void SetProgress(float val)
    {
        slider.value = val;
    }

    public void SetMaxVal(float val)
    {
        slider.maxValue = val;
        slider.value = val;
    }
}
