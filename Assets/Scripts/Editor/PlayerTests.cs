using NUnit.Framework;
using System;
using NSubstitute;
using System.Collections;
using UnityEngine.TestTools;
using Editor.Infrastructure;


namespace Editor
{
    public class PlayerTests
    {
        public class TheCurrentHealthProperty
        {
            [Test]
            public void Health_Defaults_To_0()
            {
                var player = new Player(0);
                Assert.AreEqual(0, player.CurrentHealth);
            }

            [Test]
            public void Throws_Exception_When_Current_Health_Is_Less_Than_0()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new Player(-1));
            }

            [Test]
            public void Throws_Exceptiop_When_Current_Health_Is_Greather_Than_Maximum_Health()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new Player(2, 1));
            }

            public class TheHealMethod
            {
                [Test]
                public void _0_Does_Nothing()
                {
                    var player = new Player(0);
                    player.Heal(0);

                    Assert.AreEqual(0, player.CurrentHealth);
                }

                [Test]
                public void _1_Increments_Current_Health()
                {
                    var player = new Player(0);

                    player.Heal(1);

                    Assert.AreEqual(1, player.CurrentHealth);
                }

                [Test]
                public void Overhealing_Is_Ignored()
                {
                    var player = new Player(0, 1);

                    player.Heal(2);
                    Assert.AreEqual(1, player.CurrentHealth);
                }
            }
            public class TheDamageMethod
            {
                [Test]
                public void _0_Does_Nothing()
                {
                    var player = new Player(1);

                    player.Damage(0);

                    Assert.AreEqual(1, player.CurrentHealth);
                }

                [Test]
                public void _1_Decrements_Current_Health()
                {
                    var player = new Player(1);
                    player.Damage(1);
                    Assert.AreEqual(0, player.CurrentHealth);
                }

                [Test]
                public void _2_Overkill_Is_Ignored()
                {
                    var player = new Player(1);
                    
                    player.Damage(2);
                    Assert.AreEqual(0, player.CurrentHealth);
                }
            }

            public class TheHealedEvent
            {
                [Test]
                public void Raises_Event_On_Heal()
                {
                    var amount = -1f;
                    var player = new Player(1);
                    player.Healed += (sender, args) =>
                    {
                        amount = args.Amount;
                    };

                    player.Heal(0);

                    Assert.AreEqual(0, amount);
                }

                [Test]
                public void Overhealing_Is_Ignored()
                {
                    var amount = -1f;
                    var player = new Player(1, 1);
                    player.Healed += (sender, args) =>
                    {
                        amount = args.Amount;
                    };

                    player.Heal(1);

                    Assert.AreEqual(0, amount);
                }
            }
            public class TheDamagedEvent
            {
                [Test]
                public void Raises_Event_On_Hit()
                {
                    var amount = -1f;
                    var player = new Player(1);
                    player.Damaged += (sender, args) =>
                    {
                        amount = args.Amount;
                    };

                    player.Damage(0);

                    Assert.AreEqual(0, amount);
                }

                [Test]
                public void Overkill_Is_Ignored()
                {
                    var amount = -1f;
                    var player = new Player(0);
                    player.Damaged += (sender, args) =>
                    {
                        amount = args.Amount;
                    };

                    player.Damage(1);

                    Assert.AreEqual(0, amount);
                }
            }
        }
        public class TheCurrentStaminaProperty
        {

            [Test]
            public void Stamina_Defaults_To_0()
            {
                var player = new Player(1, 1, 0);
                Assert.AreEqual(0, player.CurrentStamina);
            }

            [Test]
            public void Throws_Exception_When_Current_Stamina_Is_Less_Than_0()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new Player(1, 1, -1));
            }

            [Test]
            public void Throws_Exceptiop_When_Current_Health_Is_Greather_Than_Maximum_Stamina()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new Player(1, 1, 2, 1));
            }
            public class TheRestoreStaminaMethod
            {
                [Test]
                public void _0_Does_Nothing()
                {
                    var player = new Player(1, 1, 0);
                    player.Rest(0);

                    Assert.AreEqual(0, player.CurrentStamina);
                }

                [Test]
                public void _1_Increments_Current_Stamina()
                {
                    var player = new Player(1, 1, 0);

                    player.Rest(1);

                    Assert.AreEqual(1, player.CurrentStamina);
                }

                [Test]
                public void Overresting_Is_Ignored()
                {
                    var player = new Player(1, 1, 0, 1);

                    player.Rest(2);
                    Assert.AreEqual(1, player.CurrentStamina);
                }
            }
            public class TheUseStaminaMethod
            {
                [Test]
                public void _0_Does_Nothing()
                {
                    var player = new Player(1, 1, 1, 1);

                    player.Sprint(0);

                    Assert.AreEqual(1, player.CurrentStamina);
                }

                [Test]
                public void _1_Decrements_Current_Stamina()
                {
                    var player = new Player(1, 1, 1, 1);
                    player.Sprint(1);
                    Assert.AreEqual(0, player.CurrentStamina);
                }

                [Test]
                public void _2_Oversprint_Is_Ignored()
                {
                    var player = new Player(1, 1, 0, 1);

                    player.Sprint(2);
                    Assert.AreEqual(0, player.CurrentStamina);
                }
            }

            public class TheRestEvent
            {
                [Test]
                public void Raises_Event_On_Rest()
                {
                    var amount = -1f;
                    var player = new Player(1, 1, 1);
                    player.Rested += (sender, args) =>
                    {
                        amount = args.Amount;
                    };

                    player.Rest(0);

                    Assert.AreEqual(0, amount);
                }

                [Test]
                public void Overresting_Is_Ignored()
                {
                    var amount = -1f;
                    var player = new Player(1, 1, 1, 1);
                    player.Rested += (sender, args) =>
                    {
                        amount = args.Amount;
                    };

                    player.Rest(1);

                    Assert.AreEqual(0, amount);
                }
            }
            public class TheSprintEvent
            {
                [Test]
                public void Raises_Event_On_Run()
                {
                    var amount = -1f;
                    var player = new Player(1, 1, 1);
                    player.Sprinted += (sender, args) =>
                    {
                        amount = args.Amount;
                    };

                    player.Sprint(0);

                    Assert.AreEqual(0, amount);
                }

                [Test]
                public void Oversprinting_Is_Ignored()
                {
                    var amount = -1f;
                    var player = new Player(1, 1, 0, 1);
                    player.Sprinted += (sender, args) =>
                    {
                        amount = args.Amount;
                    };

                    player.Sprint(1);

                    Assert.AreEqual(0, amount);
                }
            }
        }
        public class MovementTests
        {
            private IUnityService _unityService;
            private Player _player;
            private FirstPersonController FirstPersonController;


            [SetUp]
            public void BeforeEveryTest()
            {
                FirstPersonController = new FirstPersonController();
                _player = A.Player();
                _player.WalkingSpeed = 2;
                _player.RunningSpeed = 4;
                _unityService = Substitute.For<IUnityService>();
            }

            [UnityTest]
            public IEnumerator Moves_Along_X_Axis_For_Horizontal_Input_Walking()
            {
                _unityService.GetAxisRaw("Horizontal").Returns(1);
                _unityService.GetDeltaTime().Returns(1);
                _player.UnityService = _unityService;

                yield return null;

                Assert.AreEqual(2, FirstPersonController.CalculateMovement(1, 0, 1, _player.WalkingSpeed).x, 0.1f);
            }

            [UnityTest]
            public IEnumerator Moves_Along_Z_Axis_For_Vertical_Input_Walking()
            {
                _unityService.GetAxisRaw("Vertical").Returns(1);
                _unityService.GetDeltaTime().Returns(1);
                _player.UnityService = _unityService;

                yield return null;

                Assert.AreEqual(2, FirstPersonController.CalculateMovement(0, 1, 1, _player.WalkingSpeed).z, 0.1f);
            }

            [UnityTest]
            public IEnumerator Moves_Along_X_Axis_For_Horizontal_Input_Running()
            {
                _unityService.GetAxisRaw("Horizontal").Returns(1);
                _unityService.GetDeltaTime().Returns(1);
                _player.UnityService = _unityService;

                yield return null;

                Assert.AreEqual(4, FirstPersonController.CalculateMovement(1, 0, 1, _player.RunningSpeed).x, 0.1f);
            }

            [UnityTest]
            public IEnumerator Moves_Along_Z_Axis_For_Vertical_Input_Running()
            {
                _unityService.GetAxisRaw("Vertical").Returns(1);
                _unityService.GetDeltaTime().Returns(1);
                _player.UnityService = _unityService;

                yield return null;

                Assert.AreEqual(4, FirstPersonController.CalculateMovement(0, 1, 1, _player.RunningSpeed).z, 0.1f);
            }
        }
    }

    

}
