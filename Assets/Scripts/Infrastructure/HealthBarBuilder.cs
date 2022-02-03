using UnityEngine.UI;

namespace Editor.Infrastructure
{
    public class HealthBarBuilder :TestDataBuilder<HealthBar>
    {
        private Image _image;

        public HealthBarBuilder(Image image)
        {
            _image = image;
        }

        public HealthBarBuilder() : this(An.Image())
        {

        }
        
        public HealthBarBuilder With(Image image)
        {
            _image = image;
            return this;
        }

        public override HealthBar Build()
        {
            return new HealthBar(_image);
        }
    }
}
