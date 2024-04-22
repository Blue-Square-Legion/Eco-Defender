using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var inLab = UnityEngine.SceneManagement.SceneManager.GetActiveScene().isSubScene;
        var labObjects = UnityEngine.SceneManagement.SceneManager.GetSceneByName("LabScene").GetRootGameObjects();
        foreach (var labObject in labObjects)
        {
            if (!labObject.CompareTag("Untagged"))//labObject.CompareTag("Player") || labObject.CompareTag("MainCamera") || labObject.CompareTag("Inventory") || labObject.CompareTag("Tool"))
            {
                continue;
            }
            labObject.gameObject.SetActive(!inLab);
        }
    }

}
