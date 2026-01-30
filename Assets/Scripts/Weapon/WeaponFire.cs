using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _spawnBulletPosition;

    protected int MaxMagazineCapacity;
    private int _magazineCapacity;
    private int _freeAmmoCapacity = 20; //TODO

    public virtual void Shoot(Vector3 mouseWorldPosition)
    {
        if (_magazineCapacity == 0 && _freeAmmoCapacity == 0)
            return;

        if (_magazineCapacity <= 0)
            ReloadMagazine();

        Vector3 aimDirection = (mouseWorldPosition - _spawnBulletPosition.position).normalized;
        Instantiate(_bullet, _spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up));

        _magazineCapacity -= 1;

        if (_magazineCapacity <= 0)
            ReloadMagazine();
    }

    private void ReloadMagazine()
    {
        if (_freeAmmoCapacity <= MaxMagazineCapacity)
        {
            _magazineCapacity = _freeAmmoCapacity;
            _freeAmmoCapacity -= _freeAmmoCapacity;
        }
        else
        {
            _magazineCapacity = MaxMagazineCapacity;
            _freeAmmoCapacity -= MaxMagazineCapacity;
        }
    }
}
