namespace Editor.Infrastructure
{
    public static class A 
    {
        public static HealthBarBuilder HealthBar()
        {
            return new HealthBarBuilder();
        }

        public static StaminaBarBuilder StaminaBar()
        {
            return new StaminaBarBuilder();
        }

        public static PlayerBuilder Player()
        {
            return new PlayerBuilder();
        }
    }
}
