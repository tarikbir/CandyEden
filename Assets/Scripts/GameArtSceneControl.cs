using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameArtSceneControl : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _sceneText;

    private float _fadeValue = 0;

    private void Update()
    {
        if (_fadeValue < 1)
        {
            _fadeValue += Time.deltaTime;
        }
    }

    public void SetBackgroundImageAndText(Sprite sprite, string text)
    {
        _image.sprite = sprite;
        _sceneText.text = text;
    }
}