using System;
using UnityEngine.UI;
public class StaminaBar
{
    private const float FillPercentage = 0.01f;
    private readonly Image _image;

    public StaminaBar(Image image)
    {
        _image = image;
    }

    public void RestoreStamina(float numberOfStamina)
    {
        if (numberOfStamina < 0) throw new ArgumentOutOfRangeException("numberOfStamina");
        _image.fillAmount += numberOfStamina * FillPercentage;
    }

    public void DepleteStamina(float numberOfStamina)
    {
        if (numberOfStamina < 0) throw new ArgumentOutOfRangeException("numberOfStamina");
        _image.fillAmount -= numberOfStamina * FillPercentage;
    }
}
