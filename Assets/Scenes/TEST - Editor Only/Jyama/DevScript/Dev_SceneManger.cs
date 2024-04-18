using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevTool
{
    public class Dev_SceneManger : MonoBehaviour
    {
        public List<SceneManagerSO> Scene;
        public PortalManager _portalManager;

        private int index = 0;

        public void Load()
        {
            index = (index+1) % Scene.Count;
            _portalManager.Open(Scene[index]);
        }

        public void UnLoad()
        {
            _portalManager.Close();
        }
    }

}
