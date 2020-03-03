using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservableAttribute<T>
{
    private T value;
    public delegate void OnValueChange(ObservableAttribute<T> attribute, T oldValue, T newValue);
    public event OnValueChange OnChangeEvent;

    public ObservableAttribute() { }
    public ObservableAttribute(T value)
    {
        this.value = value;
    }

    public void Notify(T oldValue, T newValue)
    {
        if (OnChangeEvent != null)
            OnChangeEvent(this, oldValue, newValue);
    }

    public void Set(T value)
    {
        T oldValue = this.value;
        this.value = value;

        Notify(oldValue, value);
    }

    public T Get()
    {
        return value;
    }
}
