using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private RectTransform[] _image;
    [SerializeField] private FixedJoystick _joystick;
    private PlayerMovement _playerMovement;
    private CameraRotate _cameraRotate;
    private GunController _gunShoot;
    [SerializeField] private Image _shootButton;
    public Vector2 hot;
    public Vector2 hot2;

    public Vector2[] imageSizeMax;
    public Vector2[] imageSizeMin;
    private Touch _touch;
    private Vector3 velocity;
    private Vector2 _camRot;
    private bool shift;
    private bool isShoot;
    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _cameraRotate = GetComponent<CameraRotate>();
        _gunShoot = GetComponentInChildren<GunController>();
        imageSizeMax = new Vector2[_image.Length];
        imageSizeMin = new Vector2[_image.Length];
        Debug.Log(_image.Length);
        for (int i = 0; i < _image.Length; i++)
        {
            Debug.Log(i);
            imageSizeMax[i] = new Vector2(_image[i].position.x - _image[i].rect.width, _image[i].position.y + _image[i].rect.height);
            imageSizeMin[i] = new Vector2(_image[i].position.x + _image[i].rect.width, _image[i].position.y - _image[i].rect.height);
        }
    }

    private void Update()
    {
        MobileMove();
        _cameraRotate.Rotate(_camRot);
    }

    private void FixedUpdate()
    {
        _playerMovement.Move(velocity, shift);
    }
    private void MobileMove()
    {
        velocity.x = _joystick.Horizontal;
        velocity.z = _joystick.Vertical;

        _camRot.x = Input.GetAxis(GlobStringVars.MOUSEX_AXIS);
        _camRot.y = Input.GetAxis(GlobStringVars.MOUSEY_AXIS);
        if (isShoot)
        {
            _gunShoot.Shoot();
        }
        Clamping();
    }

    private async void Clamping()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _touch = touch;
                
            }
            if (touch.phase == TouchPhase.Moved)
            {
                for (int i = 0; i < _image.Length; i++)
                {
                    if (_touch.position.x > imageSizeMax[i].x && _touch.position.x < imageSizeMin[i].x)
                    {
                        if (_touch.position.y < imageSizeMax[i].y && _touch.position.y > imageSizeMin[i].y) continue;
                    }
                }
                _camRot.x = _touch.position.x - touch.position.x;
                _camRot.y = _touch.position.y - touch.position.y;
                Mathf.Clamp(_camRot.x, -80f, 80f);
                _touch = touch;
            }
            else if (touch.phase == TouchPhase.Ended) _touch = new Touch();
        }
    }
    
    private void PcMove()
    {
        velocity.x = Input.GetAxis(GlobStringVars.HORIZONTAL_AXIS);
        velocity.z = Input.GetAxis(GlobStringVars.VERTICAL_AXIS);
        shift = Input.GetKey(GlobStringVars.SHIFT);

        _camRot.x = Input.GetAxis(GlobStringVars.MOUSEX_AXIS);
        _camRot.y = Input.GetAxis(GlobStringVars.MOUSEY_AXIS);

        _cameraRotate.Rotate(_camRot);
        if (Input.GetButton(GlobStringVars.FIRE1))
        {
            _gunShoot.Shoot();
        }
        if (Input.GetKey(GlobStringVars.RELOAD))
        {
            _gunShoot.Reload();
        }
        _gunShoot.Aim(Input.GetButton(GlobStringVars.FIRE2));
    }

    public void ShootingDown()
    {
        isShoot = true;
    }
    public void ShootingUp()
    {
        isShoot = false;
    }
}
