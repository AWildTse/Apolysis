using Editor.Infrastructure;
using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Editor
{
    public class StaminaTests
    {
        private Image _image;
        private StaminaBar _staminaBar;

        [SetUp]
        public void BeforeEveryTest()
        {
            _image = An.Image();
            _staminaBar = A.StaminaBar().With(_image);
        }

        public class RestoreStaminaMethod : StaminaTests
        {
            [Test]
            public void _0_Sets_Image_With_0_Percent_Fill_To_0_Percent_Fill()
            {
                _image.fillAmount = 0;

                _staminaBar.RestoreStamina(0);

                Assert.AreEqual(0, _image.fillAmount);
            }

            [Test]
            public void _1_Sets_Image_With_0_Percent_Fill_To_1_Percent_Fill()
            {
                _image.fillAmount = 0;

                _staminaBar.RestoreStamina(1);

                Assert.AreEqual(0.01f, _image.fillAmount);
            }

            [Test]
            public void _2_Sets_Image_With_1_Percent_Fill_To_25_Percent_Fill()
            {
                _image.fillAmount = 0.01f;

                _staminaBar.RestoreStamina(24);

                Assert.AreEqual(0.25f, _image.fillAmount);
            }

            [Test]
            public void _Throws_Exception_For_Negative_Numbers()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _staminaBar.RestoreStamina(-1));
            }
        }

        public class DepleteStaminaMethod : StaminaTests
        {
            [Test]
            public void _0_Sets_Image_With_100_Percent_Fill_To_100_Percent_Fill()
            {
                _image.fillAmount = 1;

                _staminaBar.DepleteStamina(0);

                Assert.AreEqual(1, _image.fillAmount);
            }

            [Test]
            public void _1_Sets_Image_With_100_Percent_Fill_To_99_Percent_Fill()
            {
                _image.fillAmount = 1;

                _staminaBar.DepleteStamina(1);

                Assert.AreEqual(0.99f, _image.fillAmount);
            }

            [Test]
            public void _2_Sets_Image_With_99_Percent_Fill_To_75_Percent_Fill()
            {
                _image.fillAmount = 0.99f;

                _staminaBar.DepleteStamina(24);

                Assert.AreEqual(0.75f, _image.fillAmount);
            }

            [Test]
            public void _Throws_Exception_For_Negative_Numbers()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _staminaBar.DepleteStamina(-1));
            }
        }
    }
}
