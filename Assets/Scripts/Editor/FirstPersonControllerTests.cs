using NUnit.Framework;

namespace Editor
{
    public class FirstPersonControllerTests
    {
        public class TheSpeedTestEvent
        {
            private float currentStamina;
            private bool thresholdCheck;
            private float threshold;
            private FirstPersonController FPC;

            [SetUp]
            public void beforeEveryTest()
            {
                FPC = new FirstPersonController();
                threshold = 60;
            }

            [Test]
            public void Check_If_Sprinting_Is_Okay_At_Full_Stamina()
            {
                currentStamina = 100;
                thresholdCheck = false;
                Assert.AreEqual(true, FPC.IsActionAllowed(currentStamina, thresholdCheck, threshold));
            }
            [Test]
            public void Check_If_Sprinting_Is_Okay_Above_0()
            {
                currentStamina = 1;
                thresholdCheck = false;
                Assert.AreEqual(true, FPC.IsActionAllowed(currentStamina, thresholdCheck, threshold));
            }
            [Test]
            public void Check_If_Sprinting_Is_Okay_At_0()
            {
                currentStamina = 0;
                thresholdCheck = true;
                Assert.AreEqual(false, FPC.IsActionAllowed(currentStamina, thresholdCheck, threshold));
            }
            [Test]
            public void Check_If_Sprinting_Is_Okay_At_50_After_Capping()
            {
                currentStamina = 50;
                thresholdCheck = true;

                Assert.AreEqual(false, FPC.IsActionAllowed(currentStamina, thresholdCheck, threshold));
            }
            [Test]
            public void Check_If_Sprinting_Is_Okay_At_60_After_Capping()
            {
                currentStamina = 60;
                thresholdCheck = false;
                Assert.AreEqual(true, FPC.IsActionAllowed(currentStamina, thresholdCheck, threshold));
            }
        }
    }
}
