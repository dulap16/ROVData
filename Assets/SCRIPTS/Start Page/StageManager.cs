using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;

namespace Assets.SCRIPTS.Start_Page
{
    [Serializable]
    public class StageManager
    {
        [SerializeField] private List<Stage> stages;
        [SerializeField] private int current = 0;

        public void addStage(Stage stage)
        {
            if(stages == null)
                stages = new List<Stage>();

            stages.Add(stage);
        }

        public void TimeToSpeedWhereNeeded()
        {
            foreach(Stage s in stages)
                s.TimeToSpeedIfNeeded();
        }

        public bool goToNextStage()
        {
            if (current + 1>= stages.Count)
                return false;
            
            current++;

            getCurrentStage().GoToBeginning();
            return true;
        }

        public int getCurrentIndex()
        {
            return current;
        }

        public Stage getCurrentStage()
        {
            return getStageOfIndex(current);
        }

        public bool isCurrentStageFinished()
        {
            return getCurrentStage().wasStageFinished();
        }

        public Stage getStageOfIndex(int i)
        {
            return stages[i];
        }

        public void setCurrent(int x)
        {
            current = x;
        }

        public void setCurrent(string name)
        {
            int index = getIndexOfName(name);
            if (index != -1)
                current = index;
            else Debug.Log("ERROR: name not found: " + name);
        }

        public int getIndexOfName(string name)
        {
            for(int i = 0; i < stages.Count; i++)
            {
                if(stages[i].name == name)
                {
                    return i;
                }
            }

            return -1;
        }

        public void setStage(int index, Stage s)
        {
            stages[index] = s;
        }

        public void setStageOfIndex(int i, Stage stage)
        {
            stages[i] = stage;
        }

        public int getNumberOfStages()
        {
            return stages.Count;
        }

        public void setInitValues(Vector3 pos, Vector3 scale, Color color, Quaternion rotation)
        {
            getStageOfIndex(0).setInitValuesOfStage(pos, scale, color, rotation);
        }

        public bool finishedEntireAnimation()
        {
            return isCurrentStageFinished() && (getNumberOfStages() - 1) == getCurrentIndex();
        }

        public void Restart()
        {
            setCurrent(0);
            getCurrentStage().RestartAll();
        }

        public bool advanceIfCase()
        {
            if(isCurrentStageFinished())
            {
                return goToNextStage();
            }
            return false;
        }
    }
}