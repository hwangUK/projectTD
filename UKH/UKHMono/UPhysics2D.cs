using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UKH
{
    public enum ForceType
    {
        oneWay,
        yoyo,    
        missile,
    }

    [RequireComponent(typeof(CircleCollider2D))]
    public class UPhysics2D : MonoBehaviour
    {
        [SerializeField] ForceType      _forceType;
        [SerializeField] private Vector2 _friction; /*arrow 7*/

        private Vector2 _velocity;
        private Vector2 _dirVector;
        private float _mass = 1f;
        private IEnumerator _coUpdate;

        private Transform _missileTarget;

        private Vector2 _accel;
        private float   _force;

        public void AddForce(Vector2 dir, float forceAmt, Transform missileTarget = null)
        {
            GetComponent<CircleCollider2D>().enabled = true;

            _accel = Vector2.zero;

            _dirVector      = dir;
            _force          = forceAmt * 0.1f;
            _accel          = new Vector2(_accel.x + _force / _mass, _accel.y + _force / _mass);
            _missileTarget  = missileTarget;
            
            if (_coUpdate != null)
                StopCoroutine(_coUpdate);

            _coUpdate   = CoUpdate();
            StartCoroutine(_coUpdate);
        }
        public void Sleep(bool isDelayAutoSleep = false)
        {
            GetComponent<CircleCollider2D>().enabled = false;

            if (_coUpdate != null)
                StopCoroutine(_coUpdate);

            if (isDelayAutoSleep)
            {
                StartCoroutine(CoDelaySleep());
            }
            else
            {
                this.transform.position = Vector3.zero;
                this.gameObject.SetActive(false);
            }
        }

        IEnumerator CoDelaySleep()
        {
            yield return new WaitForSeconds(10f);
            this.transform.position = Vector3.zero;
            this.gameObject.SetActive(false);
        }

        //
        IEnumerator CoUpdate()
        {
            bool isLoop = true;
            Vector2 predictDirVec = Vector2.zero;

            while (isLoop)
            {
                //가속도  (마찰계수적용)
                _accel = new Vector2(_accel.x - _friction.x * 0.01f, _accel.y - _friction.y * 0.01f);// (Mathf.Pow(_friction.x * Time.deltaTime, 2)), _accel.y + (Mathf.Pow(_friction.y * Time.deltaTime, 2)));
                Debug.LogWarning($"가속도 :{_accel}");

                if (_forceType == ForceType.oneWay)
                {
                    if(_accel.x <= 0f && _accel.y <= 0f)
                    {
                        Debug.LogWarning("속도감소해서 종료됨");
                        _accel = Vector2.zero;
                        isLoop = false;
                    }
                    else
                    {
                        Vector2 resAccel = new Vector2(
                             _accel.x > 0f ? Mathf.Pow(_accel.x, 2) : 0f,
                             _accel.y > 0f ? Mathf.Pow(_accel.y, 2) : 0f
                            );

                        _velocity = resAccel * _dirVector;
                        this.transform.position += (Vector3)(_velocity);
                    }
                }
                else if(_forceType == ForceType.yoyo)
                {
                    if (_velocity.magnitude > _force)
                    {
                        isLoop = false;
                    }
                    _velocity = _accel * _dirVector;
                    this.transform.position += (Vector3)(_velocity  );
                }
                else if (_forceType == ForceType.missile)
                {
                    if(_missileTarget == null)
                    {
                        Debug.LogError("미사일 타겟 없음");
                        isLoop = false;
                    }
                    Vector3 prevPrdictionVec = predictDirVec;
                    predictDirVec = _missileTarget.position - this.transform.position;
                 
                    // 보간 보정유도탄 좌표
                    Vector3 resultVec = Vector3.Lerp(prevPrdictionVec, predictDirVec, 0.5f);

                    _velocity = _accel * resultVec;
                    this.transform.position += (Vector3)(  _velocity);

                    //타겟과 거리가 가깝다면
                    if (Vector3.Distance(this.transform.position , _missileTarget.position) < 0.5f)
                    {
                        isLoop = false;
                    }
                }

                yield return null;
            }

            Sleep(true);
        }
        private void Awake()
        {
        }

        // Start is called before the first frame update
        void Start()
        {

        }

    }
}
