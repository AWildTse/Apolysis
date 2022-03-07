using UnityEngine;
using UnityEngine.UI;

namespace Apolysis.E.Infrastructure
{
    public class SlideBuilder : TestDataBuilder<Slider>
    {
        private int _value;

        public SlideBuilder(int value)
        {
            _value = value;
        }

        public SlideBuilder() : this(0)
        {

        }

        public SlideBuilder WithValue(int value)
        {
            _value = value;
            return this;
        }

        public override Slider Build()
        {
            var slider = new GameObject().AddComponent<Slider>();
            slider.value = _value;
            return slider;
        }
    }
}
