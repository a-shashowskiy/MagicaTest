using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicaTest
{
    public class MoveController : MonoBehaviour
    {
        private float _horizontal;
        private float _vertical;

        private float _horizontalRaw;
        private float _verticalRaw;

        private float _horizontalTurn;
        private float _verticalTurn;
        private float _horizontalTurnRaw;
        private float _verticalTurnRaw;

        private Vector3 _targetRotation;

        [SerializeField] private float _rotationSpeed = 10;
        [SerializeField] private float _movementSpeed = 200;

        Rigidbody rb;
        Animator animatorM;
        // Start is called before the first frame update
        public void Init(Rigidbody rigidbody, Animator anim)
        {
            rb = rigidbody;
            animatorM = anim;
        }

        public bool GetMovmentStatus()
        {
            return animatorM.GetBool("Muve");
        }

        // Update is called once per frame
        void Update()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");
             
            _horizontalRaw = Input.GetAxisRaw("Horizontal");
            _verticalRaw = Input.GetAxisRaw("Vertical");

            _horizontalTurn = Input.GetAxis("TurnHorizontal");
            _verticalTurn = Input.GetAxis("TurnVertical");

            _horizontalTurnRaw = Input.GetAxisRaw("TurnHorizontal");
            _verticalTurnRaw = Input.GetAxisRaw("TurnVertical");

            animatorM.SetInteger("Horizontal", (int)_horizontalRaw);
            animatorM.SetInteger("Vertical", (int)_verticalRaw);

            Vector3 input = new Vector3(_horizontal, 0, _vertical);
            Vector3 inputRaw = new Vector3(_horizontalRaw, 0, _verticalRaw);

            Vector3 _Turn = new Vector3(_horizontalTurn, 0, _verticalTurn);
            Vector3 _TurnRaw = new Vector3(_horizontalTurnRaw, 0, _verticalTurnRaw);

            if(input.sqrMagnitude > 1f)
            {
                input.Normalize();
            }
            if(inputRaw.sqrMagnitude > 1f)
            {
                inputRaw.Normalize();
            }

            if (_Turn.sqrMagnitude > 1f)
            {
                _Turn.Normalize();
            }

            if (_TurnRaw.sqrMagnitude > 1f)
            {
                _TurnRaw.Normalize();
            }

            if(_TurnRaw != Vector3.zero)
            {
                _targetRotation = Quaternion.LookRotation(_Turn).eulerAngles; 
            }

            rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_targetRotation.x, Mathf.Round(_targetRotation.y/45)*45 , _targetRotation.z), Time.deltaTime * _rotationSpeed);
        
            Vector3 vel = input * _movementSpeed * Time.deltaTime;
            rb.velocity = vel;

            if (vel.x > 0 || vel.z > 0 || vel.x < 0 || vel.z < 0) animatorM.SetBool("Muve", true);
            else if (vel.x == 0 && vel.z == 0) animatorM.SetBool("Muve", false);
        }
    }
}
