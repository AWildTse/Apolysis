using System;
using UnityEngine.UI;

namespace Apolysis.UserInterface
{
    public class StaminaSlider
    {
        private const float _fillPercentage = 0.01f;
        private Slider _slider;
        
        public StaminaSlider(Slider slider)
        {
            _slider = slider;
        }

        public void RestoreStamina(float numberOfStamina)
        {
            if (numberOfStamina < 0) throw new ArgumentOutOfRangeException("numberOfStamina");
            _slider.value += numberOfStamina * _fillPercentage;
        }

        public void DepleteStamina(float numberOfStamina)
        {
            if (numberOfStamina < 0) throw new ArgumentOutOfRangeException("numberOfStamina");
            _slider.value -= numberOfStamina * _fillPercentage;
        }
    }
}