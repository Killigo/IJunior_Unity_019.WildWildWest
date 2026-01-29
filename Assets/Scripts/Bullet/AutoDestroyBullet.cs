using UnityEngine;

public class AutoDestroyBullet : MonoBehaviour
{
    private float _timeToDestroy = 5f;

    void Start()
    {
        var ps = GetComponent<ParticleSystem>();
        Destroy(gameObject, _timeToDestroy);
    }
}
