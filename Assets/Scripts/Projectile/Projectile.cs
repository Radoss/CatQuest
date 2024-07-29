using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _timeToLive = 5;
    [SerializeField] private float _speed = 10;
    [SerializeField] private int _damage = 1;
    private Attacker _firedFrom;

    public void Fire(Vector3 direction, Attacker firedFrom) {
        _firedFrom = firedFrom;
        transform.position = _firedFrom.transform.position;
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = direction * _speed;
        StartCoroutine(ProjectileLife());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Attacker>(out Attacker attacker))
        {
            if (attacker == _firedFrom)
            {
                return;
            }
        }

        if (collision.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            damagable.TakeDamage(_damage);
            Deactivate();
        }
    }

    private IEnumerator ProjectileLife()
    {
        yield return new WaitForSeconds(_timeToLive);
        Deactivate();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
