#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace FishNet.Managing.Predicting.Editing
{
    [CustomEditor(typeof(PredictionManager), true)]
    [CanEditMultipleObjects]
    public class PredictionManagerEditor : Editor
    {
        //Client.
        private SerializedProperty _reduceReconcilesWithFramerate;
        private SerializedProperty _minimumClientReconcileFramerate;
        private SerializedProperty _createLocalStates;
        private SerializedProperty trueInterpolation;
        private SerializedProperty trueOrder;
        
        //Server.
        private SerializedProperty _dropExcessiveReplicates;
        private SerializedProperty _maximumServerReplicates;
       //private SerializedProperty _maximumConsumeCount;

        protected virtual void OnEnable()
        {
            //Client.
            _reduceReconcilesWithFramerate = serializedObject.FindProperty(nameof(_reduceReconcilesWithFramerate));
            _minimumClientReconcileFramerate = serializedObject.FindProperty(nameof(_minimumClientReconcileFramerate));
            _createLocalStates = serializedObject.FindProperty(nameof(_createLocalStates));
            trueInterpolation = serializedObject.FindProperty(nameof(trueInterpolation));
            trueOrder = serializedObject.FindProperty(nameof(trueOrder));
            
            //Server.
            _dropExcessiveReplicates = serializedObject.FindProperty(nameof(_dropExcessiveReplicates));
            _maximumServerReplicates = serializedObject.FindProperty(nameof(_maximumServerReplicates));
            //_maximumConsumeCount = serializedObject.FindProperty(nameof(_maximumConsumeCount));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((PredictionManager)target), typeof(PredictionManager), false);
            GUI.enabled = true;


            EditorGUILayout.LabelField("Client", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(_reduceReconcilesWithFramerate);
            if (_reduceReconcilesWithFramerate.boolValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_minimumClientReconcileFramerate);
                EditorGUI.indentLevel--;
            }
            
            EditorGUILayout.PropertyField(_createLocalStates);

            int interpolationValue = trueInterpolation.intValue;
            if (interpolationValue == 0)
                EditorGUILayout.HelpBox(PredictionManager.ZEROtrue_INTERPOLATION_MESSAGE, MessageType.Warning);
            else if (trueOrder.intValue == (int)ReplicateStateOrder.Appended && interpolationValue < PredictionManager.MINIMUM_APPENDED_INTERPOLATION_RECOMMENDATION)
                EditorGUILayout.HelpBox(PredictionManager.LESS_THAN_MINIMUM_APPENDED_MESSAGE, MessageType.Warning);
            else if (trueOrder.intValue == (int)ReplicateStateOrder.Inserted && interpolationValue < PredictionManager.MINIMUM_INSERTED_INTERPOLATION_RECOMMENDATION)
                EditorGUILayout.HelpBox(PredictionManager.LESS_THAN_MINIMUM_INSERTED_MESSAGE, MessageType.Warning);
            EditorGUILayout.PropertyField(trueInterpolation);

            EditorGUILayout.PropertyField(trueOrder);
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Server", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            // EditorGUILayout.PropertyField(_serverInterpolation);
            EditorGUILayout.PropertyField(_dropExcessiveReplicates);
            EditorGUI.indentLevel++;
            if (_dropExcessiveReplicates.boolValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_maximumServerReplicates);
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;


            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif