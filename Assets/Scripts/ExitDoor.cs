using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _image;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player))
        {
            _text.gameObject.SetActive(true);

            if (player.Use == 1)
            {
                Time.timeScale = 0;
                _text.gameObject.SetActive(false);
                _image.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player))
        {
            _text.gameObject.SetActive(false);
        }
    }
}
