using UnityEngine.UI;
namespace Apolysis.E.Infrastructure
{
    public static class A 
    {
        public static HealthSlideBuilder HealthSlider()
        {
            return new HealthSlideBuilder();
        }

        public static PlayerBuilder Player()
        {
            return new PlayerBuilder();
        }

        public static StaminaSlideBuilder StaminaSlider()
        {
            return new StaminaSlideBuilder();
        }
        public static SlideBuilder Slider()
        {
            return new SlideBuilder();
        }
    }
}
