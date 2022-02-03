using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElements : MonoBehaviour
{
    [SerializeField] public Image _healthBarImage;
    [SerializeField] public Image _staminaBarImage;
    public HealthBar _healthBar;
    public StaminaBar _staminaBar;
    // Start is called before the first frame update
    void Start()
    {
        _healthBar = new HealthBar(_healthBarImage);
        _staminaBar = new StaminaBar(_staminaBarImage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
