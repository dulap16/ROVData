using Assets.SCRIPTS.Start_Page.Lerpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.SCRIPTS.Start_Page
{
    [Serializable]
    public class Stage
    {
        [SerializeField] private Vector3Lerper positionLerper;
        [SerializeField] private Vector3Lerper scaleLerper;
        [SerializeField] private ColorLerper colorLerper;
        [SerializeField] private QuaternionLerper rotationLerper;

        private List<Lerper> lerpers;
        public Dictionary<String, Lerper> lerperDict;

        public Stage()
        {
            lerpers = new List<Lerper>();

            positionLerper = new Vector3Lerper();
            scaleLerper = new Vector3Lerper();
            colorLerper = new ColorLerper();
            rotationLerper = new QuaternionLerper();
            lerpers.Add(positionLerper);
            lerpers.Add(scaleLerper);
            lerpers.Add(colorLerper);
            lerpers.Add(rotationLerper);

            lerperDict = new Dictionary<String, Lerper>();
            lerperDict.Add("position", positionLerper);
            lerperDict.Add("scale", scaleLerper);
            lerperDict.Add("color", colorLerper);
            lerperDict.Add("rotation", rotationLerper);
        }

        public Lerper getLerper(String name)
        {
            return lerperDict[name];
        }

        public void LerpAll()
        {
            if(positionLerper.WillLerp())
                positionLerper.Lerp();

            if(scaleLerper.WillLerp())
                scaleLerper.Lerp();
            
            if(colorLerper.WillLerp())
                colorLerper.Lerp();
            
            if(rotationLerper.WillLerp())
                rotationLerper.Lerp();

            /*foreach (Lerper l in lerpers)
                if (l.WillLerp())
                {
                    Debug.Log(l);
                    l.Lerp();
                }*/
        }

        public void StartAll()
        {
            positionLerper.StartLerping();
            scaleLerper.StartLerping();
            colorLerper.StartLerping();
            rotationLerper.StartLerping();

            /*foreach (Lerper l in lerpers)
                l.StartLerping();*/
        }

        public void StopAll()
        {
            positionLerper.StopLerping();
            scaleLerper.StopLerping();
            colorLerper.StopLerping();
            rotationLerper.StopLerping();

            /*foreach (Lerper l in lerpers)
                l.StopLerping();*/
        }

        public void RestartAll()
        {
            positionLerper.Restart();
            scaleLerper.Restart();
            colorLerper.Restart();
            rotationLerper.Restart();

            /*foreach (Lerper l in lerpers)
                l.Restart(); */
        }

        public void GoToBeginning()
        {
            positionLerper.GoToBeginning();
            scaleLerper.GoToBeginning();
            colorLerper.GoToBeginning();
            rotationLerper.GoToBeginning();

            /*foreach (Lerper l in lerpers)
                l.GoToBeginning();*/
        }

        public bool wasStageFinished()
        {
            return positionLerper.wasTargetReached()
                && scaleLerper.wasTargetReached()
                && colorLerper.wasTargetReached()
                && rotationLerper.wasTargetReached();
        }

        public bool willLerpProperty(String name)
        {
            return lerperDict[name].WillLerp();
        }

        public void setInitValuesOfStage(Vector3 pos, Vector3 scale, Color color, Quaternion rotation)
        {
            positionLerper.setInit(pos);
            scaleLerper.setInit(scale);
            colorLerper.setInit(color);
            rotationLerper.setInit(rotation);
        }

        public void setInitIfCase(Vector3 pos, Vector3 scale, Color color, Quaternion rotation)
        {
            if(positionLerper.willInheritLast())
                positionLerper.setInit(pos);
            if (scaleLerper.willInheritLast())
                scaleLerper.setInit(scale);
            if (colorLerper.willInheritLast())
                colorLerper.setInit(color);
            if (rotationLerper.willInheritLast())
                rotationLerper.setInit(rotation);
        }
    }
}