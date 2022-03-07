using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Apolysis.UserInterface;

namespace Apolysis.E.Infrastructure
{
    public class StaminaSlideBuilder : TestDataBuilder<StaminaSlider>
    {
        private Slider _slider;

        public StaminaSlideBuilder(Slider slider)
        {
            _slider = slider;
        }
        public StaminaSlideBuilder() : this(A.Slider())
        {

        }
        public StaminaSlideBuilder With(Slider slider)
        {
            _slider = slider;
            return this;
        }
        public override StaminaSlider Build()
        {
            return new StaminaSlider(_slider);
        }
    }
}
