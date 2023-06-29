using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

[RequireComponent(typeof(PickedUpWeapon))]
[RequireComponent(typeof(TMP_Text))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour
{
    [SerializeField] private PickedUpWeapon _weapon;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private TMP_Text _text;

    private Animator _animator;

    private AudioSource _audio;

    private bool _isOpen = false;

    const string IsChestOpen = "_isChestOpen";

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player) && _isOpen == false)
        {
            _text.gameObject.SetActive(true);

            if (player.Use)
            {
                _audio.Play();
                _text.gameObject.SetActive(false);
                Open();
                _isOpen = true;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player) && _isOpen == false)
        {
            _text.gameObject.SetActive(false);
        }
    }

    private void Open()
    {
        _animator.SetBool(IsChestOpen, true);
        Instantiate(_weapon, _spawnPoint.position, Quaternion.identity);
    }
}
