using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor
{
    public class FirstPersonController : MonoBehaviour
    {
        public bool CanMove { get; private set; } = true;

        public IUnityService UnityService;
        private Player Player;
        private UIElements UIElements;

        [Header("Player Stats")]
        [SerializeField] private int _currentHP = 100;
        [SerializeField] private int _maximumHP = 100;
        [SerializeField] private int _currentSP = 100;
        [SerializeField] private int _maximumSP = 100;
        [SerializeField] private int _walkingS = 2;
        [SerializeField] private int _runningS = 4;

        [Header("Hardcode for Testing Health/Stamina Bar")]
        [SerializeField] private float _healthAmount = 10f;
        [SerializeField] private float _staminaDepleteAmount = 0.04f;
        [SerializeField] private float _staminaRestoreAmount = 0.02f;

        [Header("Hardcode for Testing Mouse Movement")]
        [SerializeField] public float _horizontalMouseSpeed = 1f;
        [SerializeField] public float _verticalMouseSpeed = 1f;

        [Header("Hardcode for Testing Jumping")]
        [SerializeField] private float jumpForce = 8f;
        [SerializeField] private float gravity = 30f;

        //Mouse Input Variables
        private float xRotation;
        private float yRotation;

        private GameObject Character;

        //Sprint Stamina Check Variables
        private bool _staminaThresholdCheck = false;
        private const float _staminaThreshold = 60f;

        private void Awake()
        {
            UIElements = gameObject.GetComponent<UIElements>();
            Character = transform.gameObject;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            Player = new Player(100, 100, 100, 100, 4, 2);

            Player.Healed += (sender, args) => UIElements._healthBar.ReplenishHealth(args.Amount);
            Player.Damaged += (sender, args) => UIElements._healthBar.DepleteHealth(args.Amount);
            Player.Rested += (sender, args) => UIElements._staminaBar.RestoreStamina(args.Amount);
            Player.Sprinted += (sender, args) => UIElements._staminaBar.DepleteStamina(args.Amount);
            Player = new Player(_currentHP, _maximumHP, _currentSP, _maximumSP, _walkingS, _runningS);

            if (UnityService == null)
                UnityService = new UnityService();
        }

        // Update is called once per frame
        void Update()
        {
            PlayerController();
        }

        public void PlayerController()
        {
            DecideSpeed(Player.CurrentStamina, Player.MaximumStamina, _staminaThresholdCheck, _staminaThreshold);
            Character.transform.eulerAngles = ReturnMousePosition(_horizontalMouseSpeed, _verticalMouseSpeed);

            //bool jump = Input.GetButton("Jump");



            //hard code input to test damage and healing
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("CurrentHealth: " + Player.CurrentHealth);

                Player.Heal(_healthAmount);

            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                Debug.Log("CurrentHealth: " + Player.CurrentHealth);

                Player.Damage(_healthAmount);

            }
        }

        public Vector3 CalculateMouseMovement(float horizontal, float vertical, float hSpeed, float vSpeed)
        {
            float mouseX = horizontal * hSpeed;
            float mouseY = vertical * vSpeed;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            return new Vector3(xRotation, yRotation, 0.0f);
        }

        public Vector3 CalculateMovement(float horizontal, float vertical, float deltaTime, float speed)
        {
            //we calculate any type of movement here by passing in the horizontal and vertical inputs along with the player's speed at time of use.
            float x = horizontal * speed * deltaTime;
            float z = vertical * speed * deltaTime;

            return new Vector3(x, 0, z);
        }

        public bool IsActionAllowed(float stamina, bool thresholdCheck, float threshold)
        {
            //We know as soon as stamina hits 0, we need to turn the threshold on
            //and return false. The player should not be able to sprint, so return false
            if(stamina == 0)
            {
                thresholdCheck = true;
                return false;
            }
            //We know as long as stamina is less than threshold, and the check has been
            //initially made, that we continue return false.
            if (stamina < threshold && thresholdCheck == true)
            {
                return false;
            }
            //Once we repass our threshold, we know it is okay for the player to sprint again
            //so we make the check false so the above if doesn't get checked and we return
            //true so the player can sprint again.
            if(stamina >= threshold)
            {
                thresholdCheck = false;
                return true;
            }
            //Otherwise, we return true just because the action should be allowed when stamina
            //threshold has not been breached.
            thresholdCheck = false;
            return true;
        }

        public void DecideSpeed(float currentStamina, float maxStamina, bool thresholdCheck, float threshold)
        {
            bool sprint = Input.GetButton("Sprint");
            //Default: player is able to sprint as long as their stamina stays above 0.
            if (sprint && (IsActionAllowed(currentStamina, thresholdCheck, threshold) == true))
            {
                Player.Sprint(_staminaDepleteAmount);
                transform.position += ReturnPosition(_runningS);
            }
            //Out of Energy: player is forced to wait until threshold is reached before they can sprint again.
            else if(IsActionAllowed(currentStamina, thresholdCheck, threshold) == false)
            {
                Player.Rest(_staminaRestoreAmount);
                transform.position += ReturnPosition(_walkingS);
            }
            //Full Energy: player does not need to call rest or sprint while they're at full energy walking around.
            else if (currentStamina == maxStamina)
            {
                transform.position += ReturnPosition(_walkingS);
            }
            //Recovering Energy: player normally recovers stamina as they're playing.
            else if(IsActionAllowed(currentStamina, thresholdCheck, threshold) == true)
            {
                Player.Rest(_staminaRestoreAmount);
                transform.position += ReturnPosition(_walkingS);
            }
        }

        public Vector3 ReturnPosition(int speed)
        {
            //We simply have a method that returns another method to keep our code looking clean.
            //Having to write this blob down is bad for readability
            return CalculateMovement(
                    UnityService.GetAxisRaw("Horizontal"),
                    UnityService.GetAxisRaw("Vertical"),
                    UnityService.GetDeltaTime(),
                    speed);
        }

        public Vector3 ReturnMousePosition(float hSpeed, float vSpeed)
        {
            //We simply have a method that returns another method to keep our code looking clean.
            //Having to write this blob down is bad for readability
            return CalculateMouseMovement(
                    UnityService.GetAxisRaw("Mouse X"),
                    UnityService.GetAxisRaw("Mouse Y"),
                    hSpeed,
                    vSpeed);
        }
    }
}
