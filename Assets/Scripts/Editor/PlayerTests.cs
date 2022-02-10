using NUnit.Framework;
using System;
using Editor.Infrastructure;
namespace Editor
{
    public class PlayerTests
    {
        public class TheCurrentHealthProperty
        {
            private Player player;
            [SetUp]
            public void Before_Every_Test()
            {
                player = A.Player();
            }

            [Test]
            public void Health_Defaults_To_0()
            {
                Assert.AreEqual(0, player.CurrentHealth);
            }

            [Test]
            public void Throws_Exception_When_Current_Health_Is_Less_Than_0()
            {
                //Assert.Throws<ArgumentOutOfRangeException>(() => new GameObject().AddComponent<Player>());
                Assert.Throws<ArgumentOutOfRangeException>(() => new Player(-1));
            }

            [Test]
            public void Throws_Exception_When_Current_Health_Is_Greather_Than_Maximum_Health()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new Player(2, 1));
            }

            public class TheHealMethod
            {
                private Player player;
                [SetUp]
                public void Before_Every_Test()
                {
                    player = A.Player();
                    player.CurrentHealth = 0;
                    player.MaximumHealth = 1;
                }

                [Test]
                public void _0_Does_Nothing()
                {
                    player.Heal(0);

                    Assert.AreEqual(0, player.CurrentHealth);
                }

                [Test]
                public void _1_Increments_Current_Health()
                {
                    player.Heal(1);

                    Assert.AreEqual(1, player.CurrentHealth);
                }

                [Test]
                public void Overhealing_Is_Ignored()
                {
                    player.Heal(2);

                    Assert.AreEqual(1, player.CurrentHealth);
                }
            }
            public class TheDamageMethod
            {
                private Player player;
                [SetUp]
                public void Before_Every_Test()
                {
                    player = A.Player();
                    player.CurrentHealth = 1;
                }

                [Test]
                public void _0_Does_Nothing()
                {
                    player.Damage(0);

                    Assert.AreEqual(1, player.CurrentHealth);
                }

                [Test]
                public void _1_Decrements_Current_Health()
                {
                    player.Damage(1);

                    Assert.AreEqual(0, player.CurrentHealth);
                }

                [Test]
                public void _2_Overkill_Is_Ignored()
                {
                    player.Damage(2);

                    Assert.AreEqual(0, player.CurrentHealth);
                }
            }

            public class TheHealedEvent
            {
                private Player player;
                [SetUp]
                public void Before_Every_Test()
                {
                    player = A.Player();
                    player.CurrentHealth = 1;
                    player.MaximumHealth = 1;
                }

                [Test]
                public void Raises_Event_On_Heal()
                {
                    var amount = -1f;

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
                private Player player;
                [SetUp]
                public void Before_Every_Test()
                {
                    player = A.Player();
                }

                [Test]
                public void Raises_Event_On_Hit()
                {
                    var amount = -1f;

                    player.CurrentHealth = 1;

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

                    player.CurrentHealth = 0
                        ;
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
            private Player player;
            [SetUp]
            public void Before_Every_Test()
            {
                player = A.Player();
                player.CurrentStamina = 0;
            }

            [Test]
            public void Stamina_Defaults_To_0()
            {
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
                private Player player;
                [SetUp]
                public void Before_Every_Test()
                {
                    player = A.Player();
                    player.CurrentStamina = 0;
                    player.MaximumStamina = 1;
                }

                [Test]
                public void _0_Does_Nothing()
                {
                    player.Rest(0);

                    Assert.AreEqual(0, player.CurrentStamina);
                }

                [Test]
                public void _1_Increments_Current_Stamina()
                {
                    player.Rest(1);

                    Assert.AreEqual(1, player.CurrentStamina);
                }

                [Test]
                public void Overresting_Is_Ignored()
                {
                    player.Rest(2);

                    Assert.AreEqual(1, player.CurrentStamina);
                }
            }
            public class TheUseStaminaMethod
            {
                private Player player;
                [SetUp]
                public void Before_Every_Test()
                {
                    player = A.Player();
                    player.CurrentStamina = 1;
                    player.MaximumStamina = 1;
                }

                [Test]
                public void _0_Does_Nothing()
                {
                    player.Sprint(0);

                    Assert.AreEqual(1, player.CurrentStamina);
                }

                [Test]
                public void _1_Decrements_Current_Stamina()
                {
                    player.Sprint(1);

                    Assert.AreEqual(0, player.CurrentStamina);
                }

                [Test]
                public void _2_Oversprint_Is_Ignored()
                {
                    player.CurrentStamina = 0;

                    player.Sprint(2);

                    Assert.AreEqual(0, player.CurrentStamina);
                }
            }

            public class TheRestEvent
            {
                private Player player;
                [SetUp]
                public void Before_Every_Test()
                {
                    player = A.Player();
                    player.CurrentStamina = 1;
                    player.MaximumStamina = 1;
                }

                [Test]
                public void Raises_Event_On_Rest()
                {
                    var amount = -1f;

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
                private Player player;
                [SetUp]
                public void Before_Every_Test()
                {
                    player = A.Player();
                    player.CurrentStamina = 1;
                    player.MaximumStamina = 1;
                }

                [Test]
                public void Raises_Event_On_Run()
                {
                    var amount = -1f;

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

                    player.CurrentStamina = 0;

                    player.Sprinted += (sender, args) =>
                    {
                        amount = args.Amount;
                    };

                    player.Sprint(1);

                    Assert.AreEqual(0, amount);
                }
            }
        }
    }



}
