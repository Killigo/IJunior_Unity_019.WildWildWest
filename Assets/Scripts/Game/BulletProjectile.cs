using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 40f;
    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;

    private Rigidbody _bulletRigidbody;

    private void Awake()
    {
        _bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletTarget>() != null)
        {
            //Hit target;
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        }
        else
        {
            //ColliderHit something else;
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
