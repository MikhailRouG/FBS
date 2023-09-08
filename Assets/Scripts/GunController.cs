using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Animator _cameraAnimator;

    [Header("Characteristics")]
    [SerializeField] private int _ammo;
    [SerializeField] private int _currentAmmo;
    [SerializeField] private float _timeToShoot;
    private float _shootTime = 0;

    [Header("Rotation Recoil")]
    [SerializeField] private float _recoilRotationX;
    [SerializeField] private float _recoilRotationY;
    [SerializeField] private float _rotationRecoilZ;
    [SerializeField] private float _returnRotationSpeed;
    [SerializeField] private float _smoothRotation;
    private Vector3 targetRotation;
    private Vector3 currentRotation;

    [Header("Position Recoil")]
    [SerializeField] private float _recoilPositionX;
    [SerializeField] private float _recoilPositionY;
    [SerializeField] private float _recoilPositionZ;
    [SerializeField] private float _returnPositionSpeed;
    [SerializeField] private float _smoothPosition;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 currentPosition;

    [Header("Aim Position Recoil")]
    [SerializeField] private float returnAimPosSpeed;
    [SerializeField] private float smothAimPosSpeed;
    [SerializeField] private Vector3 aimPosition;
    private bool isAim;

    [Header("Bullet")]
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform _BulletStartPos;

    [Header("Audio")]
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private void Awake()
    {
        isAim = false;
        startPosition = transform.localPosition;
        _audioSource = GetComponent<AudioSource>();
        _currentAmmo = _ammo;
    }

    private void Update()
    {
        Recoil();
    }

    private void Recoil()
    {
        if (!Input.GetButton(GlobStringVars.FIRE1))
        {
            targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, _returnRotationSpeed * Time.deltaTime);
            currentRotation = Vector3.Slerp(currentRotation, targetRotation, _smoothRotation * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(currentRotation);
        }
        if (!isAim)
        {
            targetPosition = Vector3.Lerp(targetPosition, startPosition, _returnPositionSpeed * Time.deltaTime);
            currentPosition = Vector3.Slerp(currentPosition, targetPosition, _smoothPosition * Time.deltaTime);
            transform.localPosition = currentPosition;
        }
        else
        {
            targetPosition = Vector3.Lerp(targetPosition, aimPosition, returnAimPosSpeed * Time.deltaTime);
            currentPosition = Vector3.Slerp(currentPosition, targetPosition, smothAimPosSpeed * Time.deltaTime);
            transform.localPosition = currentPosition;
        }
    }

    public void Shoot()
    {
        if (Time.time < (_timeToShoot + _shootTime)) return;

        if (_currentAmmo <= 0) return;
        _currentAmmo -= 1;
        _shootTime = Time.time;
        Instantiate(BulletPrefab, _BulletStartPos.position, transform.rotation);
        _audioSource.PlayOneShot(_audioClip);
        _cameraAnimator.Play("CameraShake");
        targetRotation += new Vector3(-_recoilRotationX, Random.Range(-_recoilRotationY, _recoilRotationY), Random.Range(-_recoilRotationY, _recoilRotationY));
        targetPosition += new Vector3(Random.Range(-_recoilPositionX, _recoilPositionX), _recoilPositionY, -_recoilPositionZ);
    }

    public void Aim(bool isAim)
    {
        this.isAim = isAim;
    }

    public void Reload()
    {
        _currentAmmo = _ammo;
    }
}
