using UnityEngine;


namespace System_Programming.Lesson4
{
    public class PlayerCharacter : Character
    {
        [Range(0, 100)][SerializeField] private int _health = 100;
        [Range(0.5f, 10.0f)][SerializeField] private float _movingSpeed = 8.0f;
        [SerializeField] private float _acceleration = 3.0f;
        private const float GRAVITY = -9.8f;
        private CharacterController _characterController;
        private MouseLook _mouseLook;
        private Vector3 _currentVelocity;
        protected override FireAction FireAction { get; set; }


        private void Start()
        {
            if (IsOwner)
            {
                AttachCamera();
            }
            Initiate();
        }

        private void AttachCamera()
        {
            var camera = Camera.main;
            camera.transform.SetParent(transform, false);
            camera.transform.localPosition = Vector3.zero;
            camera.transform.localRotation = Quaternion.identity;
        }

        protected override void Initiate()
        {
            base.Initiate();
            if (IsOwner)
            {
                _characterController = GetComponentInChildren<CharacterController>();
                _characterController ??= gameObject.AddComponent<CharacterController>();
                FireAction = gameObject.AddComponent<RayShooter>();
                FireAction.Reloading();
                _mouseLook = GetComponentInChildren<MouseLook>();
                _mouseLook ??= gameObject.AddComponent<MouseLook>();
            }
        }

        public override void Movement()
        {
            if (IsOwner)
            {
                var moveX = Input.GetAxis("Horizontal") * _movingSpeed;
                var moveZ = Input.GetAxis("Vertical") * _movingSpeed;
                var movement = new Vector3(moveX, 0, moveZ);
                movement = Vector3.ClampMagnitude(movement, _movingSpeed);
                movement *= Time.deltaTime;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    movement *= _acceleration;
                }
                movement.y = GRAVITY;
                movement = transform.TransformDirection(movement);
                _characterController.Move(movement);
                _mouseLook.Rotation();
                _serverPosition.Value = transform.position;
                _serverRotation.Value = transform.rotation;
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position,
                    _serverPosition.Value, ref _currentVelocity, _movingSpeed * Time.deltaTime);
                transform.rotation = _serverRotation.Value;
            }
        }

        private void OnGUI()
        {
            if (IsOwner)
            {
                if (Camera.main == null)
                {
                    return;
                }
                var info = $"Health: {_health}\nClip: {FireAction.BulletCount}";
                var size = 12;
                var bulletCountSize = 50;
                var posX = Camera.main.pixelWidth / 2 - size / 4;
                var posY = Camera.main.pixelHeight / 2 - size / 2;
                var posXBul = Camera.main.pixelWidth - bulletCountSize * 2;
                var posYBul = Camera.main.pixelHeight - bulletCountSize;
                GUI.Label(new Rect(posX, posY, size, size), "+");
                GUI.Label(new Rect(posXBul, posYBul, bulletCountSize * 2,
                bulletCountSize * 2), info);
            }
        }
    }
}