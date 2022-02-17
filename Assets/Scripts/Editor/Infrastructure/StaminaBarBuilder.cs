using UnityEngine.UI;

namespace Editor.Infrastructure
{
    public class StaminaBarBuilder :TestDataBuilder<StaminaBar>
    {
        private Image _image;

        public StaminaBarBuilder(Image image)
        {
            _image = image;
        }

        public StaminaBarBuilder() : this(An.Image())
        {

        }

        public StaminaBarBuilder With(Image image)
        {
            _image = image;
            return this;
        }

        public override StaminaBar Build()
        {
            return new StaminaBar(_image);
        }
    }
}
