using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _playerObject;

    [SerializeField]
    private Transform _laneMiddle;

    [SerializeField]
    private Transform _laneRight;

    [SerializeField]
    private Transform _laneLeft;

    [SerializeField]
    private float _movementSpeed;

    [SerializeField]
    private AnimationCurve _jumpCurve;

    [SerializeField]
    private float _jumpSpeed;

    [SerializeField]
    private float _jumpPower;

    private float _jumpTime;

    private bool isJumping = false;

    [SerializeField]
    private AnimationCurve _debugSlideCurve;

    private float _slideTime;

    private bool isSliding = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GoSideToSide();
        Jump();
        Slide();
    }

    void GoSideToSide()
    {
        float sideInput = Input.GetAxis("Horizontal");

        if (sideInput > 0)
        {
            _playerObject.position = Vector3.Lerp(_playerObject.position, _laneRight.position, Time.deltaTime * _movementSpeed);
        }else if (sideInput < 0)
        {
            _playerObject.position = Vector3.Lerp(_playerObject.position, _laneLeft.position, Time.deltaTime * _movementSpeed);
        }else if (_playerObject.position.x >= 0.01f || _playerObject.position.x <= -0.01f)
        {
            _playerObject.position = Vector3.Lerp(_playerObject.position, _laneMiddle.position, Time.deltaTime * _movementSpeed);
        }
    }

    void Jump()
    {
        if (!isSliding && (Input.GetButtonDown("Jump") || Input.GetAxis("Vertical") > 0))
        {
            if (!isJumping)
            {
                isJumping = true;
            }
        }
        if (isJumping)
        {
            if (_jumpTime <= 1)
            {
                _jumpTime += Time.deltaTime * _jumpSpeed;
                Vector3 jumpDistance = new Vector3(_playerObject.position.x, _laneMiddle.position.y + (_jumpCurve.Evaluate(_jumpTime) * _jumpPower), _playerObject.position.z);
                _playerObject.position = jumpDistance;
            }else
            {
                isJumping = false;
                _jumpTime = 0;
            }
        }
    }

    void Slide()
    {
        float slideInput = Input.GetAxis("Vertical");

        if (!isJumping && (Input.GetKeyDown(KeyCode.LeftShift) || slideInput <= -0.1f))
        {
            if (!isSliding)
            {
                isSliding = true;
            }
        }
        if (isSliding)
        {
            if (_slideTime <= 1)
            {
                _slideTime += Time.deltaTime * _jumpSpeed;
                Vector3 slideScale = new Vector3(1, _debugSlideCurve.Evaluate(_slideTime), 1);
                _playerObject.localScale = slideScale;
            }
            else
            {
                isSliding = false;
                _slideTime = 0;
            }
        }
    }
}
