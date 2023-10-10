using Assets.SCRIPTS.Start_Page.Lerpers;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace Assets.SCRIPTS.Start_Page
{
    [System.Serializable]
    public class Lerper
    {
        [SerializeField] private bool willLerp = false;
        [SerializeField] private bool inheritLast = false;
        [SerializeField] private bool inheritCurrent = false;

        private float _current = 0;
        private float _target = 0;
        [SerializeField] [Range(0, 10)] public float speed;
        [SerializeField] [Range(0, 20)] public float delay;
        [SerializeField] public AnimationCurve _curve;

        public void Lerp()
        {
            _current = Mathf.MoveTowards(_current, _target, speed * Time.deltaTime);
        }

        public float GetCurrent()
        {
            return _curve.Evaluate(_current);
        }

        public void StartLerping()
        {
            _target = 1;
        }

        public void StopLerping() {
            _current = _target;
        }

        public void GoToBeginning()
        {
            _current = 0;
            _target = 0;
        }

        public void Restart()
        {
            _current = 0;
            StartLerping();
        }
        public bool wasTargetReached()
        {
            return (willLerp && _current  == 1) || !willLerp;
        }

        public bool WillLerp()
        {
            return willLerp;
        }

        public bool willInheritLast()
        {
            return inheritLast;
        }

        public bool willInheritCurrent()
        {
            return inheritCurrent;
        }

        public Vector3 finalVector3()
        {
            Vector3 result;
            try
            {
                result = ((Vector3Lerper)this).getFinal();
            } catch(Exception e)
            {
                result = Vector3.zero;
            }

            return result;
        }

        public Color finalColor()
        {
            Color result;
            try
            {
                result = ((ColorLerper)this).getFinal();
            } catch(Exception e)
            {
                result = Color.black;
            }

            return result;
        }

        public Quaternion finalQuaternion()
        {
            Quaternion result;
            try
            {
                result = ((QuaternionLerper)this).getFinal();
            }
            catch (Exception e)
            {
                result = new Quaternion();
            }

            return result;
        }
    }
}