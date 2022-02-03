using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Editor.Infrastructure
{
    public class PlayerBuilder : TestDataBuilder<Player>
    {
        private float _speed;

        public PlayerBuilder()
        {
        }

        public PlayerBuilder(float speed)
        {
            _speed = speed;
        }
        public override Player Build()
        {
            return new Player();
        }
    }
}
