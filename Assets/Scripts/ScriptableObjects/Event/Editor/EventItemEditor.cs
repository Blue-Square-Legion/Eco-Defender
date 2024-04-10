using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EventSO
{
    [CustomEditor(typeof(EventItemSO), editorForChildClasses: true)]
    public class EventItemEditor : GenericEventEditor<ItemSO>
    {
    }

}