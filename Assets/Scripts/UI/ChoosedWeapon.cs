using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ChoosedWeapon : MonoBehaviour
{
    [SerializeField] Player _player;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();     
    }

    private void OnEnable()
    {
        _player.WeaponChanged += ChangeWeaponIcon;
    }

    private void OnDisable()
    {
        _player.WeaponChanged -= ChangeWeaponIcon;
    }

    private void ChangeWeaponIcon(Sprite sprite)
    {
        _image.sprite = sprite;
    }
}
