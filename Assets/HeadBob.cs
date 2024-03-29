using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{

    [SerializeField] private bool enable = true;
    [SerializeField, Range(0, 0.1f)] private float amplitude = .015f;
    [SerializeField, Range(0, 30)] private float freq = 10.0f;

    public Transform _camera;
    public Transform _cameraHolder;

    private float toggleSpeed = 3.0f;
    private Vector3 startPos;
    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        startPos = _camera.localPosition;
    }


    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * freq) * amplitude;
        pos.x += Mathf.Sin(Time.time * freq / 2) * amplitude * 2;
        return pos;
        

    }

     private void PlayMotion(Vector3 motion)
    {
       _camera.localPosition += motion; 
    }

    private void ResetPosition()
    {
        if (_camera.localPosition == startPos) 
        {
            return;

        }
        else 
        {
            _camera.localPosition = Vector3.Lerp(_camera.localPosition, startPos, 1 * Time.deltaTime);
        }
        
    }

    private void CheckMotion()
    {
        float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;

        if (speed < toggleSpeed) return;
        if (!controller.isGrounded) return;

        PlayMotion(FootStepMotion());
    }

   
    // Update is called once per frame
    void Update()
    {
        if (!enable) return;

        CheckMotion();
        ResetPosition();
        _camera.LookAt(FocusTarget());
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z);
        pos += _cameraHolder.forward * 15.0f;
        return pos;
    }
}
