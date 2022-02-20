using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using Editor.Infrastructure;
using UnityEngine.TestTools;
using System.Collections;

using Apolysis.Interfaces;


namespace Editor
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
            private float currentStamina;
            private bool thresholdCheck;
            private float threshold;
            private FirstPersonController FPC;

            [SetUp]
            public void beforeEveryTest()
            {
                FPC = new GameObject().AddComponent<FirstPersonController>();
                threshold = 60;
            }

            [Test]
            public void Check_If_Sprinting_Is_Okay_At_Full_Stamina()
            {
                currentStamina = 100;
                thresholdCheck = false;
                Assert.AreEqual(true, FPC.CanPlayerSprint(currentStamina, thresholdCheck, threshold));
            }
            [Test]
            public void Check_If_Sprinting_Is_Okay_Above_0()
            {
                currentStamina = 1;
                thresholdCheck = false;
                Assert.AreEqual(true, FPC.CanPlayerSprint(currentStamina, thresholdCheck, threshold));
            }
            [Test]
            public void Check_If_Sprinting_Is_Okay_At_0()
            {
                currentStamina = 0;
                thresholdCheck = true;
                Assert.AreEqual(false, FPC.CanPlayerSprint(currentStamina, thresholdCheck, threshold));
            }
            [Test]
            public void Check_If_Sprinting_Is_Okay_At_50_After_Capping()
            {
                currentStamina = 50;
                thresholdCheck = true;

                Assert.AreEqual(false, FPC.CanPlayerSprint(currentStamina, thresholdCheck, threshold));
            }
            [Test]
            public void Check_If_Sprinting_Is_Okay_At_60_After_Capping()
            {
                currentStamina = 60;
                thresholdCheck = false;
                Assert.AreEqual(true, FPC.CanPlayerSprint(currentStamina, thresholdCheck, threshold));
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