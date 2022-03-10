using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using Apolysis.E.Infrastructure;
using UnityEngine.TestTools;
using System.Collections;

using Apolysis.Interfaces;


namespace E
{
    public class FirstPersonControllerTests
    {
        public class MovementTests
        {
            private IUnityService _unityService;
            private Player _player;
            private FirstPersonController FirstPersonController;


            [SetUp]
            public void BeforeEveryTest()
            {
                FirstPersonController = new GameObject().AddComponent<FirstPersonController>();
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
        public class TheSpeedTestEvent
        {
            private int currentStamina;
            private bool thresholdCheck;
            private float threshold;
            private FirstPersonController FPC;

            [SetUp]
            public void BeforeEveryTest()
            {
                FPC = new GameObject().AddComponent<FirstPersonController>();
                threshold = 60;
            }

            [Test]
            public void Check_If_Sprinting_Is_Okay_At_Full_Stamina()
            {
                currentStamina = 100;
                FPC.CurrentStamina = currentStamina;
                thresholdCheck = false;
                FPC._staminaThresholdCheck = thresholdCheck;

                Assert.AreEqual(true, FPC.CanPlayerSprint());
            }
            [Test]
            public void Check_If_Sprinting_Is_Okay_Above_0()
            {
                currentStamina = 1;
                FPC.CurrentStamina = currentStamina;
                thresholdCheck = false;
                FPC._staminaThresholdCheck = thresholdCheck;

                Assert.AreEqual(true, FPC.CanPlayerSprint());
            }
            [Test]
            public void Check_If_Sprinting_Is_Okay_At_0()
            {
                currentStamina = 0;
                FPC.CurrentStamina = currentStamina;
                thresholdCheck = true;
                FPC._staminaThresholdCheck = thresholdCheck;

                Assert.AreEqual(false, FPC.CanPlayerSprint());
            }
            [Test]
            public void Check_If_Sprinting_Is_Okay_At_50_After_Capping()
            {
                currentStamina = 50;
                FPC.CurrentStamina = currentStamina;
                thresholdCheck = true;
                FPC._staminaThresholdCheck = thresholdCheck;

                Assert.AreEqual(false, FPC.CanPlayerSprint());
            }
            [Test]
            public void Check_If_Sprinting_Is_Okay_At_60_After_Capping()
            {
                currentStamina = 60;
                FPC.CurrentStamina = currentStamina;
                thresholdCheck = false;
                FPC._staminaThresholdCheck = thresholdCheck;

                Assert.AreEqual(true, FPC.CanPlayerSprint());
            }
        }

        public class CanPlayerActTests
        {
            private FirstPersonController FPC;

            [SetUp]
            public void beforeEveryTest()
            {
                FPC = new GameObject().AddComponent<FirstPersonController>();
            }
        }
    }
}