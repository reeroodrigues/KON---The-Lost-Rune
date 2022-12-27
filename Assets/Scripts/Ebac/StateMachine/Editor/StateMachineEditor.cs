using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FSMExample))]

public class StateMachineEditor : Editor
{

    public bool showFoldout;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FSMExample fsm = (FSMExample)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("StateMachine");

        if(fsm.stateMachine == null)
        {
            return;
        }

        if(fsm.stateMachine.currentState != null)
        {
            EditorGUILayout.LabelField("Current State: ", fsm.stateMachine.currentState.ToString());
        }

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Avaliable State");

        if (showFoldout)
        {
            if(fsm.stateMachine.dictionaryState != null)
            {
                var keys = fsm.stateMachine.dictionaryState.Keys.ToArray();
                var vals = fsm.stateMachine.dictionaryState.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], vals[i]));
                }
            }
            
        }
    }
}
