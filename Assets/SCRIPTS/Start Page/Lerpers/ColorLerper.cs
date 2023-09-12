using System.Collections;
using UnityEngine;

namespace Assets.SCRIPTS.Start_Page.Lerpers
{
    [System.Serializable]
    public class ColorLerper : Lerper
    {
        [SerializeField] private Color init, final;

        public Color getCurrentValue()
        {
            return Color.Lerp(init, final, GetCurrent());
        }

        public void setInit(Color New)
        {
            init = New;
        }

        public Color getFinal()
        {
            return final;
        }
    }
}