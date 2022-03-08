using Apolysis.E.Infrastructure;
using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.UI;

using Apolysis.UserInterface;

namespace E
{
    public class StaminaTests
    {
        private Slider _slider;
        private StaminaSlider _staminaSlider;

        [SetUp]
        public void BeforeEveryTest()
        {
            _slider = A.Slider();
            _staminaSlider = A.StaminaSlider().With(_slider);
        }

        public class RestoreStaminaMethod : StaminaTests
        {
            [Test]
            public void _0_Sets_Image_With_0_Percent_Fill_To_0_Percent_Fill()
            {
                _slider.value = 0;

                _staminaSlider.RestoreStamina(0);

                Assert.AreEqual(0, _slider.value);
            }

            [Test]
            public void _1_Sets_Image_With_0_Percent_Fill_To_1_Percent_Fill()
            {
                _slider.value = 0;

                _staminaSlider.RestoreStamina(1);

                Assert.AreEqual(0.01f, _slider.value);
            }

            [Test]
            public void _2_Sets_Image_With_1_Percent_Fill_To_25_Percent_Fill()
            {
                _slider.value = 0.01f;

                _staminaSlider.RestoreStamina(24);

                Assert.AreEqual(0.25f, _slider.value);
            }

            [Test]
            public void _Throws_Exception_For_Negative_Numbers()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _staminaSlider.RestoreStamina(-1));
            }
        }

        public class DepleteStaminaMethod : StaminaTests
        {
            [Test]
            public void _0_Sets_Image_With_100_Percent_Fill_To_100_Percent_Fill()
            {
                _slider.value = 1;

                _staminaSlider.DepleteStamina(0);

                Assert.AreEqual(1, _slider.value);
            }

            [Test]
            public void _1_Sets_Image_With_100_Percent_Fill_To_99_Percent_Fill()
            {
                _slider.value = 1;

                _staminaSlider.DepleteStamina(1);

                Assert.AreEqual(0.99f, _slider.value);
            }

            [Test]
            public void _2_Sets_Image_With_99_Percent_Fill_To_75_Percent_Fill()
            {
                _slider.value = 0.99f;

                _staminaSlider.DepleteStamina(24);

                Assert.AreEqual(0.75f, _slider.value);
            }

            [Test]
            public void _Throws_Exception_For_Negative_Numbers()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _staminaSlider.DepleteStamina(-1));
            }
        }
    }
}
