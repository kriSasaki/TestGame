using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
[RequireComponent(typeof(Weapon))]

public class PickedUpWeapon : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private TMP_Text _text;

    private Coroutine _weaponDroping;

    private bool _isPickable = false;
    private readonly float _delay = 1;
    private float _time = 0;

    private void Start()
    {
        _weaponDroping = StartCoroutine(DropWeapon());
    }

    private IEnumerator DropWeapon()
    {
        while (_time < _delay)
        {
            _time += Time.deltaTime;
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.002f);

            yield return null;
        }

        if (_time >= _delay)
        {
            _isPickable = true;
            StopCoroutine(_weaponDroping);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player) && _isPickable)
        {
            _text.gameObject.SetActive(true);

            if (player.Use == 1)
            {
                _text.gameObject.SetActive(false);
                player.AddWeapon(_weapon);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player) && _isPickable)
        {
            _text.gameObject.SetActive(false);
        }
    }
}
