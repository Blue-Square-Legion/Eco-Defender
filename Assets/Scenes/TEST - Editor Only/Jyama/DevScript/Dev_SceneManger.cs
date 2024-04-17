using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevTool
{
    public class Dev_SceneManger : MonoBehaviour
    {
        public SceneManagerSO Scene;

        public void Toggle()
        {

            if (Scene.IsLoaded())
            {
                UnLoad();
            }
            else
            {
                Load();
            }
        }

        public void Load()
        {
            Scene.AsyncLoad();
        }

        public void UnLoad()
        {
            Scene.AsyncUnload();
        }
    }

}
