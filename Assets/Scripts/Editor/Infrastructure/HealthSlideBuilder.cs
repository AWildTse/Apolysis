using UnityEngine.UI;
using Apolysis.UserInterface;

namespace Apolysis.E.Infrastructure
{
    public class HealthSlideBuilder :TestDataBuilder<HealthSlider>
    {
        private Slider _slider;

        public HealthSlideBuilder(Slider slider)
        {
            _slider = slider;
        }

        public HealthSlideBuilder() : this(A.Slider())
        {

        }
        
        public HealthSlideBuilder With(Slider slider)
        {
            _slider = slider;
            return this;
        }

        public override HealthSlider Build()
        {
            return new HealthSlider(_slider);
        }
    }
}
