/*
 * MapEditorCameraController.cs
 * Script Author: Charles d'Ansembourg
 * Creation Date: 08/08/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

public class MapEditorCameraController : MonoBehaviour
{
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _distance = 10.0f;
    [SerializeField] private float _xSpeed = 120.0f;
    [SerializeField] private float _ySpeed = 120.0f;
    [SerializeField] private float _scrollSpeed = 10.0f;
    [SerializeField] private float _yMinLimit = -20f;
    [SerializeField] private float _yMaxLimit = 80f;
    [SerializeField] private float _distanceMin = 2f;
    [SerializeField] private float _distanceMax = 15f;

    [SerializeField] private float _moveSpeed = 0.02f;

    [SerializeField] private float _keyboardOrbitSpeed = 60.0f;

    private float x = 0.0f;
    private float y = 0.0f;

    private Vector3 _lastMousePosition;
    private bool _mouseDragging;

    public void Start()
    {
        x = 0;
        y = 60;
    }

    private void LateUpdate()
    {
        HandleMouseInput();
        HandleKeyboardInput();

        _distance -= Input.GetAxis("Mouse ScrollWheel") * _scrollSpeed;
        _distance = Mathf.Clamp(_distance, _distanceMin, _distanceMax);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -_distance) + _targetPosition + new Vector3(0, 0.75f, 0);

        _camera.rotation = rotation;
        _camera.position = position;
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(2))
        {
            _mouseDragging = true;
            _lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(2))
        {
            _mouseDragging = false;
        }

        if (_mouseDragging)
        {
            Vector3 deltaMousePosition = Input.mousePosition - _lastMousePosition;

            x += deltaMousePosition.x * _xSpeed * 0.02f;
            y -= deltaMousePosition.y * _ySpeed * 0.02f;

            y = ClampAngle(y, _yMinLimit, _yMaxLimit);

            _lastMousePosition = Input.mousePosition;
        }
    }

    private void HandleKeyboardInput()
    {
        Vector3 forward = _camera.forward;
        Vector3 right = _camera.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        if (Input.GetKey(KeyCode.DownArrow))
            _targetPosition -= forward * _moveSpeed;
        if (Input.GetKey(KeyCode.UpArrow))
            _targetPosition += forward * _moveSpeed;
        if (Input.GetKey(KeyCode.RightArrow))
            _targetPosition += right * _moveSpeed;
        if (Input.GetKey(KeyCode.LeftArrow))
            _targetPosition -= right * _moveSpeed;

        if (Input.GetKey(KeyCode.W))
            y += _keyboardOrbitSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            y -= _keyboardOrbitSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            x += _keyboardOrbitSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            x -= _keyboardOrbitSpeed * Time.deltaTime;

        y = ClampAngle(y, _yMinLimit, _yMaxLimit);
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    public void FaceNorth()
    {
        x = 0;
    }
}