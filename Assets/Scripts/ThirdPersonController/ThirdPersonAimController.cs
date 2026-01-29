using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(StarterAssetsInputs))]
[RequireComponent(typeof(ThirdPersonController))]
[RequireComponent(typeof(Animator))]
public class ThirdPersonAimController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity = 1f;
    [SerializeField] private float aimSensitivity = 0.7f;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform bulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Rig aimRig;

    private StarterAssetsInputs _starterAssetsInputs;
    private ThirdPersonController _thirdPersonController;
    private Animator _animator;

    private float aimRigWeight;

    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if (_starterAssetsInputs.aim)
        {
            aimRigWeight = 1f;

            aimVirtualCamera.gameObject.SetActive(true);
            _thirdPersonController.SetSensitivity(aimSensitivity);
            _thirdPersonController.SetRotateOnMove(false);
            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDetection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDetection, Time.deltaTime * 20f);
        }
        else
        {
            aimRigWeight = 0f;

            aimVirtualCamera.gameObject.SetActive(false);
            _thirdPersonController.SetSensitivity(normalSensitivity);
            _thirdPersonController.SetRotateOnMove(true);
            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }

        if (_starterAssetsInputs.shoot)
        {
            Vector3 aimDirection = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(bulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up));
            _starterAssetsInputs.shoot = false;
        }

        aimRig.weight = Mathf.Lerp(aimRig.weight, aimRigWeight, Time.deltaTime * 20f);
    }
}
