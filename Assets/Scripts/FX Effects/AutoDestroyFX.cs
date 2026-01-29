using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AutoDestroyFX : MonoBehaviour
{
    void Start()
    {
        var ps = GetComponent<ParticleSystem>();
        Destroy(gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
    }
}