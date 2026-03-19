using System;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CarController1 : MonoBehaviour
{

    [SerializeField] LevelManager levelManager;
    [SerializeField] private AudioClip crashSFX;

    private bool canMove;

    public enum Axel
    {
        Front, Back
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject WheelModel;
        public WheelCollider WheelCollider;
        public Axel Axel;
    }

    public float MaxAcceleration = 30.0f;
    public float BrakeAcceleration = 50.0f;

    public float TurnSensitivity = 1.0f;
    public float MaxSteerAngle = 30.0f;

    public Vector3 _CenterOfMass;

    public List<Wheel> wheels;

    float MoveInput;
    float SteerInput;

    private Rigidbody CarRigidBody;

    private void Start()
    {
        CarRigidBody = GetComponent<Rigidbody>();
        CarRigidBody.centerOfMass = _CenterOfMass;
        canMove = true;
    }

    void Update()
    {
        if(canMove)
        {
            GetInputs();
            AnimateWheels();
        }
    }

    void LateUpdate()
    {
        if (canMove)
        {
            Move();
            Steer();
            Brake();
        }
        else
        {
            stop();
        }
    }

    void GetInputs()
    {
        MoveInput = Input.GetAxis("Vertical");
        SteerInput = Input.GetAxis("Horizontal");
    }

    void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.WheelCollider.motorTorque = MoveInput * 400 * MaxAcceleration * Time.deltaTime;
        }
    }

    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.Axel == Axel.Front)
            {
                var _steerAngle = SteerInput * TurnSensitivity * MaxSteerAngle;
                wheel.WheelCollider.steerAngle = Mathf.Lerp(wheel.WheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    void Brake()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            foreach(var wheel in wheels)
            {
                wheel.WheelCollider.brakeTorque = 300 * BrakeAcceleration * Time.deltaTime;
            }
        }
        else
        {
            foreach( var wheel in wheels)
            {
                wheel.WheelCollider.brakeTorque = 0;
            }
        }
    }

    void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.WheelCollider.GetWorldPose(out pos, out rot);
            wheel.WheelModel.transform.position = pos;
            wheel.WheelModel.transform.rotation = rot;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Enemy") && !collision.transform.CompareTag("MobileProp"))
        {
            Debug.Log("Player hit wall");
            SoundFXManager.Instance.PlaySoundFXClip(crashSFX, transform, 1f);
            levelManager.PlayerLost();
        }
    }

    public void cantMoveAnymore()
    {
        canMove = false;
    }

    void stop()
    {
        foreach (var wheel in wheels)
        {
            wheel.WheelCollider.motorTorque = 0;
            wheel.WheelCollider.brakeTorque = 500;
        }
    }
}
