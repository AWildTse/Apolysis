using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Editor.Infrastructure
{
    public class PlayerBuilder : TestDataBuilder<Player>
    {
        private float _currentHealth;
        private float _maximumHealth;
        private float _currentStamina;
        private float _maximumStamina;

        public PlayerBuilder()
        {
        }

        public PlayerBuilder(float currentHealth, float maximumHealth, float currentStamina, float maximumStamina)
        {
            _currentHealth = currentHealth;
            _maximumHealth = maximumHealth;
            _currentStamina = currentStamina;
            _maximumStamina = maximumStamina;
        }
        public override Player Build()
        {
            return new GameObject().AddComponent<Player>();
        }
    }
}
