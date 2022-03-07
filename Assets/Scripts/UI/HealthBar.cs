﻿using System;
using UnityEngine.UI;

namespace Apolysis.UserInterface
{
    public class HealthBar
    {
        private const float _fillPercentage = 0.01f;
        private Slider _slider;
        public HealthBar(Slider slider)
        {
            _slider = slider;
        }

        public void ReplenishHealth(float numberOfHealth)
        {
            if (numberOfHealth < 0) throw new ArgumentOutOfRangeException("numberOfHealth");
            _slider.value += numberOfHealth * _fillPercentage;
        }

        public void DepleteHealth(float numberOfHealth)
        {
            if (numberOfHealth < 0) throw new ArgumentOutOfRangeException("numberOfHealth");
            _slider.value -= numberOfHealth * _fillPercentage;
        }
    }
}