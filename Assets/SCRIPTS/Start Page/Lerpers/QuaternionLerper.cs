using System.Collections;
using UnityEngine;

namespace Assets.SCRIPTS.Start_Page.Lerpers
{
    [System.Serializable]
    public class QuaternionLerper : Lerper
    {
        [SerializeField] private Quaternion init, final;

        public Quaternion getCurrentValue()
        {
            return Quaternion.Lerp(init, final, GetCurrent());
        }

        public void setInit(Quaternion New)
        {
            init = New;
        }

        public Quaternion getFinal()
        {
            return final;
        }
    }
}