using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EventSO
{
#if UNITY_EDITOR
    [CustomEditor(typeof(EventItemSO), editorForChildClasses: true)]
    public class EventItemEditor : GenericEventEditor<ItemSO>
    {
    }
#endif
}