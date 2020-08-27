using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatBarUI : MonoBehaviour
{
    public Image bar;
    public Image background;
    public Text textBar;

    public ObservableAttribute<float> value;
    public ObservableAttribute<float> maxValue;

    private void Start()
    {
        bar.type = Image.Type.Filled;
    }

    public void Set(ObservableAttribute<float> value, ObservableAttribute<float> maxValue)
    {
        if (this.value != null)
            this.value.OnChangeEvent -= Value_OnChangeEvent;

        if (value != null)
        {
            this.value = value;
            this.maxValue = maxValue;
            bar.fillAmount = value.Get() / maxValue.Get();
            textBar.text = value.Get() + "/" + maxValue.Get();
            this.value.OnChangeEvent += Value_OnChangeEvent;
        }

        if (value.Get() < maxValue.Get())
        {
            this.value.OnChangeEvent -= Value_OnChangeEvent;
        }
    }

    private void Value_OnChangeEvent(ObservableAttribute<float> attribute, float oldValue, float newValue)
    {
        bar.fillAmount = newValue / maxValue.Get();
        textBar.text = newValue+"/"+ maxValue.Get();
    }
}
