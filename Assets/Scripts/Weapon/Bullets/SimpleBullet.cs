using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class SimpleBullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private Player _player;
     
    private Rigidbody2D _rigidbody2D;

    private SpriteRenderer _spriteRenderer;
   
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sortingLayerName = _player.Layer;
        gameObject.layer = LayerMask.NameToLayer(_player.Layer);
        _rigidbody2D.velocity = transform.right * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);

            Destroy(gameObject);
        }

        if (collider.gameObject.TryGetComponent(out Subject subject))
        {
            Destroy(gameObject);
        }

        if (collider.gameObject.TryGetComponent(out DestroyableObject destroyableObject))
        {
            destroyableObject.BreakObject();
            Destroy(gameObject);
        }
    }
}
