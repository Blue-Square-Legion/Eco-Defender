using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JyamaTestScript : MonoBehaviour
{
    [SerializeField]private List<ItemSO> _list;


    // Start is called before the first frame update
    void Start()
    {
        _list.ForEach((ItemSO item) => print($"{item.name} : {item.Type}"));
    }

}
