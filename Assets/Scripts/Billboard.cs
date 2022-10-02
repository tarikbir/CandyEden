using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private float _rotationMultiplier = 4f;
    [SerializeField] private float _offsetMultiplier = 25f;
    [SerializeField] private bool _doesWave;
    private Camera _mainCamera;

    protected virtual void Start()
    {
        _mainCamera = GameManager.Instance.MainCamera;
        if (_doesWave)
        {
            _rotationMultiplier = Random.Range(0, _rotationMultiplier);
            _offsetMultiplier = Random.Range(0, _offsetMultiplier);
        }
    }

    protected virtual void LateUpdate()
    {
        transform.LookAt(_mainCamera.transform);

        if (_doesWave)
        {
            var sin = Mathf.Sin(Time.timeSinceLevelLoad + _offsetMultiplier);
            var cos = Mathf.Cos(Time.timeSinceLevelLoad + _offsetMultiplier);
            transform.rotation = Quaternion.Euler(sin * _rotationMultiplier, transform.rotation.eulerAngles.y - 180, cos * _rotationMultiplier);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y - 180, 0f);
        }
    }
}
