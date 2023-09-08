using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private CameraRotate cameraRotate;
     private GunController gunShoot; 
    private Vector3 velocity;
    private Vector2 sensitivity;
    private bool shift;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        cameraRotate = GetComponent<CameraRotate>();
        gunShoot = GetComponentInChildren<GunController>();
    }

    private void Update()
    {
        velocity.x = Input.GetAxis(GlobStringVars.HORIZONTAL_AXIS);
        velocity.z = Input.GetAxis(GlobStringVars.VERTICAL_AXIS);
        shift = Input.GetKey(GlobStringVars.SHIFT);

        sensitivity.x = Input.GetAxis(GlobStringVars.MOUSEX_AXIS);
        sensitivity.y = Input.GetAxis(GlobStringVars.MOUSEY_AXIS);

        cameraRotate.Rotate(sensitivity);
        if (Input.GetButton(GlobStringVars.FIRE1))
        {
            gunShoot.Shoot();
        }
        if(Input.GetKey(GlobStringVars.RELOAD))
        {
            gunShoot.Reload();
        }
        gunShoot.Aim(Input.GetButton(GlobStringVars.FIRE2));
    }

    private void FixedUpdate()
    {
        playerMovement.Move(velocity, shift);
    }
}
