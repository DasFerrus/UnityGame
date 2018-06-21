using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float sens = 6f;

    [SerializeField]
    private float jumpForce = 60f;

    [Header("Jump Settings:")]
    [SerializeField]
    private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField]
    private float jointSpring = 50f;
    [SerializeField]
    private float jointMaxForce = 100f;

    private PlayerMotor motor;
    private ConfigurableJoint joint;

    void Start(){
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(jointSpring);
    }

    void Update(){
        float _xMove = Input.GetAxisRaw("Horizontal");
        float _zMove = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _xMove;
        Vector3 _moveVertical = transform.forward * _zMove;

        //final vector
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * speed;
        motor.Move(_velocity);


        float _yRot = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * sens;
        motor.Rotate(_rotation);


        float _xRot = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRot * sens;

        Vector3 _jumpForce = Vector3.zero;
        motor.RotateCamera(_cameraRotationX);
        if (Input.GetButton("Jump"))
        {
            _jumpForce = Vector3.up * jumpForce;
            SetJointSettings(0f);
        }
        else{
            SetJointSettings(jointSpring);
        }
        motor.Jump(_jumpForce);

    }

    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive {
            mode = jointMode,
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }

}
