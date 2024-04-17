using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    const float lookRateSpeed = 75.0F;


    [Header("Speed")]
    [SerializeField]
    [Tooltip("Z Axis - Rolling")]
    float forwardSpeed;

    [SerializeField]
    [Tooltip("X Axis - Pitching")]
    float strafeSpeed;

    [SerializeField]
    [Tooltip("Y Axis - Yawing")]
    float hoverSpeed;


    [Header("Acceleration")]
    [SerializeField]
    float forwardAcceleration;

    [SerializeField]
    float strafeAcceleration;

    [SerializeField]
    float hoverAcceleration;


    [Header("Roll")]
    [SerializeField]
    float rollSpeed;

    [SerializeField]
    float rollAcceleration;


    Rigidbody _rigidbody;

    Vector2 _screenCenter;
    Vector2 _lookDirection;
    Vector2 _mouseDistance;

    float _activeForwardSpeed;
    float _activeStrafeSpeed;
    float _activeHoverSpeed;

    float _activeRoll;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _screenCenter = new Vector2(Screen.width * 0.5F, Screen.height * 0.5F);
    }

    private void Update()
    {
        _lookDirection = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        _mouseDistance =
            new Vector2((_lookDirection.x - _screenCenter.x) / _screenCenter.x,
                (_lookDirection.y - _screenCenter.y) / _screenCenter.y);

        _mouseDistance = Vector2.ClampMagnitude(_mouseDistance, 1.0F);

        _activeRoll = Mathf.Lerp(_activeRoll, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        //Hacia delante
        float currentForwardSpeed = Input.GetAxisRaw("Forward") * forwardSpeed;
        currentForwardSpeed = Mathf.Clamp(currentForwardSpeed, forwardSpeed * 0.05F, forwardSpeed); //Siempre hacia delante
        _activeForwardSpeed =
            Mathf.Lerp(_activeForwardSpeed, currentForwardSpeed, forwardAcceleration * Time.deltaTime);

        //Hacia derecha o izquierda
        float currentStrafeSpeed = Input.GetAxisRaw("Horizontal") * strafeSpeed;
        _activeStrafeSpeed =
            Mathf.Lerp(_activeStrafeSpeed, currentStrafeSpeed, strafeAcceleration * Time.deltaTime);

        //Hacia arriba o abajo
        float currentHoverSpeed = Input.GetAxisRaw("Hover") * hoverSpeed;
        _activeHoverSpeed =
            Mathf.Lerp(_activeHoverSpeed, currentHoverSpeed, hoverAcceleration * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        float x = -_mouseDistance.y * lookRateSpeed * Time.fixedDeltaTime;
        float y = _mouseDistance.x * lookRateSpeed * Time.fixedDeltaTime;
        float z = _activeRoll * rollSpeed * Time.fixedDeltaTime;

        transform.Rotate(x, y, z, Space.Self);


        _rigidbody.position += transform.forward * _activeForwardSpeed * Time.fixedDeltaTime;
        _rigidbody.position += transform.right * _activeStrafeSpeed * Time.fixedDeltaTime;
        _rigidbody.position += transform.up * _activeHoverSpeed * Time.fixedDeltaTime;
    }




}