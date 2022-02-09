using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Editor
{
    public class FirstPersonController : MonoBehaviour
    {
        public bool CanMove { get; private set; } = true;
        public bool IsMoving { get; private set; } = true;
        public bool CanJump { get; private set; } = true;

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

        [Header("Health and Stamina Bar")]
        [SerializeField] private float _healthAmount = 10f;
        [SerializeField] private float _staminaDepleteAmount = 0.04f;
        [SerializeField] private float _staminaRestoreAmount = 0.02f;

        [Header("Hardcode for Testing Mouse Movement")]
        [SerializeField] public float _horizontalMouseSpeed = 1f;
        [SerializeField] public float _verticalMouseSpeed = 1f;

        [Header("Hardcode for Testing Jumping")]
        [SerializeField] private float jumpForce = 8f;
        [SerializeField] private float gravity = 30f;

        private Camera playerCamera;

        //Mouse Input Variables
        private float xRotation;
        private float yRotation;

        private GameObject Character;
        private Rigidbody rigidbody;
        private Camera camera;

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
            Player = new Player(_currentHP, _maximumHP, _currentSP, _maximumSP, _walkingS, _runningS);
            rigidbody = GetComponent<Rigidbody>();
            camera = GetComponentInChildren<Camera>();

            Player.Healed += (sender, args) => UIElements._healthBar.ReplenishHealth(args.Amount);
            Player.Damaged += (sender, args) => UIElements._healthBar.DepleteHealth(args.Amount);
            Player.Rested += (sender, args) => UIElements._staminaBar.RestoreStamina(args.Amount);
            Player.Sprinted += (sender, args) => UIElements._staminaBar.DepleteStamina(args.Amount);


            if (UnityService == null)
                UnityService = new UnityService();
        }

        // Update is called once per frame
        void Update()
        {
            IsMoving = true;

            if (CanMove)
            {
                HandleMouseMovement();
                HandlePlayerMovement();
            }
            TestHPAndSPBars();
        }

        public void HandleMouseMovement()
        {
            camera.transform.localEulerAngles = CalculateMouseMovement(
                    UnityService.GetAxisRaw("Mouse X"),
                    UnityService.GetAxisRaw("Mouse Y"),
                    _horizontalMouseSpeed,
                    _verticalMouseSpeed);
        }

        public void HandlePlayerMovement()
        {
            DecideSpeed(Player.CurrentStamina, Player.MaximumStamina, _staminaThresholdCheck, _staminaThreshold);
        }

        public Vector3 CalculateMouseMovement(float horizontal, float vertical, float hSpeed, float vSpeed)
        {
            float mouseX = horizontal * hSpeed;
            float mouseY = vertical * vSpeed;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);
            transform.localEulerAngles = new Vector3(0, yRotation, 0);

            return new Vector3(xRotation, 0, 0);
        }

        public Vector3 CalculateMovement(float horizontal, float vertical, float deltaTime, float speed)
        {
            float x = horizontal * speed * deltaTime;
            float z = vertical * speed * deltaTime;

            Vector3 move = (transform.right * x) + (transform.forward * z);

            return move;
        }

        public bool IsActionAllowed(float stamina, bool thresholdCheck, float threshold)
        {
            if (stamina == 0)
            {
                thresholdCheck = true;
                return false;
            }
            if (stamina < threshold && thresholdCheck == true)
            {
                return false;
            }
            if (stamina >= threshold)
            {
                thresholdCheck = false;
                return true;
            }
            thresholdCheck = false;
            return true;
        }

        public void DecideSpeed(float currentStamina, float maxStamina, bool thresholdCheck, float threshold)
        {
            bool sprint = Input.GetButton("Sprint");
            if (sprint && (IsActionAllowed(currentStamina, thresholdCheck, threshold) == true))
            {
                Player.Sprint(_staminaDepleteAmount);
                transform.position += ReturnPosition(_runningS);
                IsMoving = true;
            }
            else if (IsActionAllowed(currentStamina, thresholdCheck, threshold) == false)
            {
                Player.Rest(_staminaRestoreAmount);
                transform.position += ReturnPosition(_walkingS);
                IsMoving = true;

            }
            else if (currentStamina == maxStamina)
            {
                transform.position += ReturnPosition(_walkingS);
                IsMoving = true;

            }
            else if (IsActionAllowed(currentStamina, thresholdCheck, threshold) == true)
            {
                Player.Rest(_staminaRestoreAmount);
                transform.position += ReturnPosition(_walkingS);
                IsMoving = true;
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
        public void TestHPAndSPBars()
        {
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
    }
}
