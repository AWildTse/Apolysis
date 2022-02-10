using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Editor
{
    public class FirstPersonController : MonoBehaviour
    {
        #region Serializable Fields
        [Header("Player Stats")]
        [SerializeField] private int _currentHP = 100;
        [SerializeField] private int _maximumHP = 100;
        [SerializeField] private int _currentSP = 100;
        [SerializeField] private int _maximumSP = 100;
        [SerializeField] private int _walkingS = 2;
        [SerializeField] private int _runningS = 4;
        [SerializeField] private int _crouchingReduction = 1;

        [Header("Health and Stamina Bar")]
        [SerializeField] private float _healthAmount = 10f;
        [SerializeField] private float _staminaDepleteAmount = 0.04f;
        [SerializeField] private float _staminaRestoreAmount = 0.02f;

        [Header("Hardcode for Testing Mouse Movement")]
        [SerializeField] private float _horizontalMouseSensitivity = 1f;
        [SerializeField] private float _verticalMouseSensitivity = 1f;
        #endregion

        #region GetComponent Variables
        private IUnityService _unityService;
        private Player _player;
        private GameObject _placeHolder;
        private CapsuleCollider _capsuleCollider;
        #endregion

        #region Camera Variables
        private Camera _camera;
        private float _xRotation;
        private float _yRotation;
        public const float FieldOfVision = 60f;
        public bool CameraCanMove { get; private set; } = true;
        #endregion

        #region HealthBar Variables
        [SerializeField] private Image _healthBarImage;
        [SerializeField] private Image _healthBarImageBG;
        private HealthBar _healthBar;
        #endregion

        #region Movement Variables
        public bool PlayerCanMove { get; private set; } = true;

        #region Sprinting Variables
        private bool _staminaThresholdCheck = false;
        private const float _staminaThreshold = 60f;
        [SerializeField] private Image _staminaBarImage;
        [SerializeField] private Image _staminaBarImageBG;
        private StaminaBar _staminaBar;
        #endregion
        
        #region Jumping Variables
        private float _jumpPower = 5f;
        private bool _isGrounded = true;
        private Rigidbody _rigidbody;

        public bool PlayerCanJump { get; private set; } = true;
        #endregion

        #region Crouching Variables
        private float _crouchingHeight = 1.5f;
        private float _standingHeight = 2f;
        private bool _isCrouched = false;
        private Vector3 _originalScale;
        public bool PlayerCanCrouch { get; private set; } = true;
        #endregion
        #endregion

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            #region Set Up Variables
            _player = new Player(_currentHP, _maximumHP, _currentSP, _maximumSP, _walkingS, _runningS, _crouchingReduction);
            _rigidbody = GetComponent<Rigidbody>();
            _camera = GetComponentInChildren<Camera>();
            _capsuleCollider = GetComponent<CapsuleCollider>();

            _placeHolder = gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
            _healthBarImage = _placeHolder.GetComponent<Image>();
            _placeHolder = gameObject.transform.GetChild(1).GetChild(0).GetChild(1).gameObject;
            _healthBarImageBG = _placeHolder.GetComponent<Image>();
            _placeHolder = gameObject.transform.GetChild(1).GetChild(1).GetChild(0).gameObject;
            _staminaBarImage = _placeHolder.GetComponent<Image>();
            _placeHolder = gameObject.transform.GetChild(1).GetChild(1).GetChild(1).gameObject;
            _staminaBarImageBG = _placeHolder.GetComponent<Image>();

            _healthBar = new HealthBar(_healthBarImage);
            _staminaBar = new StaminaBar(_staminaBarImage);
            #endregion

            #region Set Up EventArgs
            _player.Healed += (sender, args) => _healthBar.ReplenishHealth(args.Amount);
            _player.Damaged += (sender, args) => _healthBar.DepleteHealth(args.Amount);
            _player.Rested += (sender, args) => _staminaBar.RestoreStamina(args.Amount);
            _player.Sprinted += (sender, args) => _staminaBar.DepleteStamina(args.Amount);
            #endregion

            if (_unityService == null)
                _unityService = new UnityService();
        }

        // Update is called once per frame
        void Update()
        {
            if(PlayerCanMove)
            {
                HandleMouseMovement();
            }

            if(CameraCanMove)
            {
                HandlePlayerMovement();
            }

            if(PlayerCanJump)
            {
                if (Input.GetButtonDown("Jump") && _isGrounded)
                {
                    Jump();
                }
            }
            CheckGround();

            if (PlayerCanCrouch)
            {
                if (Input.GetButtonDown("Crouch"))
                {
                    _isCrouched = false;
                    Crouch();
                }
                else if(Input.GetButtonUp("Crouch"))
                {
                    _isCrouched = true;
                    Crouch();
                }
            }

            TestHPAndSPBars();
        }

        public void Jump()
        {
            _rigidbody.AddForce(0f, _jumpPower, 0f, ForceMode.Impulse);
            _isGrounded = false;
        }

        public void Crouch()
        {
            if (_isCrouched)
            {
                //transform.localScale = new Vector3(_originalScale.x, _originalScale.y, _originalScale.z);
                //_capsuleCollider.height = _standingHeight;
                StartCoroutine(setCrouchingSpeed(_standingHeight, _crouchingHeight));
                _walkingS /= _crouchingReduction;

                _isCrouched = false;
            }
            // Crouches player down to set height
            // Reduces walkSpeed
            else
            {
                //transform.localScale = new Vector3(_originalScale.x, _crouchHeight, _originalScale.z);
                //_capsuleCollider.height = _crouchingHeight;
                StartCoroutine(setCrouchingSpeed(_standingHeight, _crouchingHeight));
                _walkingS *= _crouchingReduction;

                _isCrouched = true;
            }
        }
        public IEnumerator setCrouchingSpeed(float standingHeight, float crouchingHeight)
        {
            if (_isCrouched)
            {
                float math;
                for(math = standingHeight - crouchingHeight; math > 0; math -= 0.1f)
                {
                    _capsuleCollider.height += 0.1f;
                    yield return new WaitForSecondsRealtime(.01f);
                }
            }
            else
            {
                float math;
                for (math = standingHeight - crouchingHeight; math > 0; math -= 0.1f)
                {
                    _capsuleCollider.height -= 0.1f;
                    yield return new WaitForSecondsRealtime(.01f);
                }
            }
        }
        public void CheckGround()
        {
            Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
            Vector3 direction = transform.TransformDirection(Vector3.down);
            float distance = .75f;

            if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
            {
                Debug.DrawRay(origin, direction * distance, Color.red);
                _isGrounded = true;
            }
            else
            {
                _isGrounded = false;
            }
        }    

        public void HandleMouseMovement()
        {
            _camera.transform.localEulerAngles = CalculateMouseMovement(
                    _unityService.GetAxisRaw("Mouse X"),
                    _unityService.GetAxisRaw("Mouse Y"),
                    _horizontalMouseSensitivity,
                    _verticalMouseSensitivity);
        }

        public void HandlePlayerMovement()
        {
            DecideSpeed(_player.CurrentStamina, _player.MaximumStamina, _staminaThresholdCheck, _staminaThreshold);
        }

        public Vector3 CalculateMouseMovement(float horizontal, float vertical, float hSpeed, float vSpeed)
        {
            float mouseX = horizontal * hSpeed;
            float mouseY = vertical * vSpeed;

            _yRotation += mouseX;
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90, 50);
            transform.localEulerAngles = new Vector3(0, _yRotation, 0);

            return new Vector3(_xRotation, 0, 0);
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
                _player.Sprint(_staminaDepleteAmount);
                transform.position += ReturnPosition(_runningS);
            }
            else if (IsActionAllowed(currentStamina, thresholdCheck, threshold) == false)
            {
                _player.Rest(_staminaRestoreAmount);
                transform.position += ReturnPosition(_walkingS);

            }
            else if (currentStamina == maxStamina)
            {
                transform.position += ReturnPosition(_walkingS);

            }
            else if (IsActionAllowed(currentStamina, thresholdCheck, threshold) == true)
            {
                _player.Rest(_staminaRestoreAmount);
                transform.position += ReturnPosition(_walkingS);
            }
        }

        public Vector3 ReturnPosition(int speed)
        {
            //We simply have a method that returns another method to keep our code looking clean.
            //Having to write this blob down is bad for readability
            
            return CalculateMovement(
                    _unityService.GetAxisRaw("Horizontal"),
                    _unityService.GetAxisRaw("Vertical"),
                    _unityService.GetDeltaTime(),
                    speed);
        }
        public void TestHPAndSPBars()
        {
            //hard code input to test damage and healing
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("CurrentHealth: " + _player.CurrentHealth);

                _player.Heal(_healthAmount);

            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                Debug.Log("CurrentHealth: " + _player.CurrentHealth);

                _player.Damage(_healthAmount);

            }
        }
    }
}
