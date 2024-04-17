using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSO;
using System;

namespace DevTool
{
    public class JyamaTestScript : MonoBehaviour
    {
        [SerializeField] private List<ItemSO> _list;

        [SerializeField] private EventItemSO OnColllect;

        // Start is called before the first frame update
        void Start()
        {
            //_list.ForEach((ItemSO item) => print($"{item.name} : {item.Type}"));


        }

        private void OnEnable()
        {
            OnColllect.AddEventListener(OnCollected);
        }



        private void OnDisable()
        {
            OnColllect.RemoveEventListener(OnCollected);
        }

        private void OnCollected(ItemSO obj)
        {
            print($"Collected: {obj.name}");
        }

    }
}

