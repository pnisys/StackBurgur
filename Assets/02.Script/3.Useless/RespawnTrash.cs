using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Oculus.Interaction.Samples;
public class RespawnTrash : RespawnOnDrop
{
    //SerializeField�� �̻��ϰ� private�ӿ��� ����ϸ� �ν�����â�� �״�� ��Ÿ����.
    //�̰� ���׷� �����Ѵ�.


    [SerializeField]
    private float _yThresholdForRespawn;

    [SerializeField]
    private UnityEvent _whenRespawned = new UnityEvent();

    // cached starting transform
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private Vector3 _initialScale;

    private Rigidbody _rigidBody;

    protected override void OnEnable()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _initialScale = transform.localScale;
        _rigidBody = GetComponent<Rigidbody>();
    }

    protected override void Update()
    {
        if (transform.position.y < _yThresholdForRespawn)
        {
            transform.position = _initialPosition;
            transform.rotation = _initialRotation;
            transform.localScale = _initialScale;

            if (_rigidBody)
            {
                _rigidBody.velocity = Vector3.zero;
                _rigidBody.angularVelocity = Vector3.zero;
            }

            _whenRespawned.Invoke();
        }
    }
}
