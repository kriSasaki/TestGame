using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(TMP_Text))]
public class HealStatue : MonoBehaviour
{
    [SerializeField] private int _heal = 5;
    [SerializeField] private TMP_Text _text;

    private AudioSource _audio;

    private bool _isUsed = false;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player) && _isUsed == false)
        {
            _text.gameObject.SetActive(true);

            if (player.Use)
            {
                _text.gameObject.SetActive(false);
                _audio.Play();
                player.ApplyHeal(_heal);
                _isUsed = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player) && _isUsed == false)
        {
            _text.gameObject.SetActive(false);
        }
    }
}
