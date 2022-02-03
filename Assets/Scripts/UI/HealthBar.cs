using System;
using UnityEngine.UI;

public class HealthBar
{
    private const float FillPercentage = 0.01f;
    private readonly Image _image;
    public HealthBar(Image image)
    {
        _image = image;
    }

    public void ReplenishHealth(float numberOfHealth)
    {
        if (numberOfHealth < 0) throw new ArgumentOutOfRangeException("numberOfHealth");
        _image.fillAmount += numberOfHealth * FillPercentage;
    }

    public void DepleteHealth(float numberOfHealth)
    {
        if (numberOfHealth < 0) throw new ArgumentOutOfRangeException("numberOfHealth");
        _image.fillAmount -= numberOfHealth * FillPercentage;
    }
}
