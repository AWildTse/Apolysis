using Apolysis.E.Infrastructure;
using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.UI;

using Apolysis.UserInterface;

namespace Apolysis.E
{
    public class HealthTests
    {
        private Image _image;
        private HealthBar _healthBar;

        [SetUp]
        public void BeforeEveryTest()
        {
            _image = An.Image();
            _healthBar = A.HealthBar().With(_image);
        }

        public class ReplenishHealthMethod : HealthTests
        {
            [Test]
            public void _0_Sets_Image_With_0_Percent_Fill_To_0_Percent_Fill()
            {

                _image.fillAmount = 0;

                _healthBar.ReplenishHealth(0);

                Assert.AreEqual(0, _image.fillAmount);
            }

            [Test]
            public void _1_Sets_Image_With_0_Percent_Fill_To_1_Percent_Fill()
            {
                _image.fillAmount = 0;

                _healthBar.ReplenishHealth(1);

                Assert.AreEqual(0.01f, _image.fillAmount);
            }

            [Test]
            public void _1_Sets_Image_With_1_Percent_Fill_To_25_Percent_Fill()
            {
                _image.fillAmount = 0.01f;

                _healthBar.ReplenishHealth(24);

                Assert.AreEqual(0.25f, _image.fillAmount);
            }

            [Test]
            public void _Throws_Exception_For_Negative_Number_Of_Heart_Pieces()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _healthBar.ReplenishHealth(-1));
            }
        }

        public class DepleteHealthMethod : HealthTests
        {
            [Test]
            public void _0_Sets_Image_With_100_Percent_Fill_To_100_Percent_Fill()
            {
                _image.fillAmount = 1;
                _healthBar.DepleteHealth(0);
                Assert.AreEqual(1, _image.fillAmount);
            }

            [Test]
            public void _1_Sets_Image_With_100_Percent_Fill_To_99_Percent_Fill()
            {
                _image.fillAmount = 1;

                _healthBar.DepleteHealth(1);

                Assert.AreEqual(0.99f, _image.fillAmount);
            }

            [Test]
            public void _2_Sets_Image_With_99_Percent_To_75_Percent_Fill()
            {
                _image.fillAmount = 0.99f;

                _healthBar.DepleteHealth(24);

                Assert.AreEqual(0.75f, _image.fillAmount);
            }

            [Test]
            public void _Throws_Exception_For_Negative_Number_Of_Heart_Pieces()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _healthBar.DepleteHealth(-1));
            }
        }
    }
}
