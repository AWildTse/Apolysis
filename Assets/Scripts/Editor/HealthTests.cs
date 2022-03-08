using Apolysis.E.Infrastructure;
using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.UI;

using Apolysis.UserInterface;

namespace E
{
    public class HealthTests
    {
        private Slider _slider;
        private HealthBar _healthSlider;

        [SetUp]
        public void BeforeEveryTest()
        {
            _slider = A.Slider();
            _healthSlider = A.HealthSlider().With(_slider);
        }

        public class ReplenishHealthMethod : HealthTests
        {
            [Test]
            public void _0_Sets_Image_With_0_Percent_Fill_To_0_Percent_Fill()
            {
                _slider.value = 0;

                _healthSlider.ReplenishHealth(0);

                Assert.AreEqual(0, _slider.value);
            }

            [Test]
            public void _1_Sets_Image_With_0_Percent_Fill_To_1_Percent_Fill()
            {
                _slider.value = 0;

                _healthSlider.ReplenishHealth(1);

                Assert.AreEqual(0.01f, _slider.value);
            }

            [Test]
            public void _1_Sets_Image_With_1_Percent_Fill_To_25_Percent_Fill()
            {
                _slider.value = 0.01f;

                _healthSlider.ReplenishHealth(24);

                Assert.AreEqual(0.25f, _slider.value);
            }

            [Test]
            public void _Throws_Exception_For_Negative_Number_Of_Heart_Pieces()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _healthSlider.ReplenishHealth(-1));
            }
        }

        public class DepleteHealthMethod : HealthTests
        {
            [Test]
            public void _0_Sets_Image_With_100_Percent_Fill_To_100_Percent_Fill()
            {
                _slider.value = 1;
                _healthSlider.DepleteHealth(0);
                Assert.AreEqual(1, _slider.value);
            }

            [Test]
            public void _1_Sets_Image_With_100_Percent_Fill_To_99_Percent_Fill()
            {
                _slider.value = 1;

                _healthSlider.DepleteHealth(1);

                Assert.AreEqual(0.99f, _slider.value);
            }

            [Test]
            public void _2_Sets_Image_With_99_Percent_To_75_Percent_Fill()
            {
                _slider.value = 0.99f;

                _healthSlider.DepleteHealth(24);

                Assert.AreEqual(0.75f, _slider.value);
            }

            [Test]
            public void _Throws_Exception_For_Negative_Number_Of_Heart_Pieces()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _healthSlider.DepleteHealth(-1));
            }
        }
    }
}
