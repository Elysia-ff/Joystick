using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Joystick))]
public class JoystickEditor : Editor
{
	private Joystick script;

	private SerializedProperty pointerProperty;
	private SerializedProperty minDistanceProperty;
	private SerializedProperty maxDistanceProperty;
	private SerializedProperty minValueThresholdProperty;
	private SerializedProperty maxValueThresholdProperty;
	private SerializedProperty unitAngleCountProperty;

	private void OnEnable()
	{
		script = target as Joystick;

		pointerProperty = serializedObject.FindProperty("pointer");
		minDistanceProperty = serializedObject.FindProperty("minDistance");
		maxDistanceProperty = serializedObject.FindProperty("maxDistance");
		minValueThresholdProperty = serializedObject.FindProperty("minValueThreshold");
		maxValueThresholdProperty = serializedObject.FindProperty("maxValueThreshold");
		unitAngleCountProperty = serializedObject.FindProperty("unitAngleCount");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		using (EditorGUI.ChangeCheckScope scope = new EditorGUI.ChangeCheckScope())
		{
			EditorGUILayout.PropertyField(pointerProperty);
			EditorGUILayout.PropertyField(minDistanceProperty);
			EditorGUILayout.PropertyField(maxDistanceProperty);
			EditorGUILayout.PropertyField(minValueThresholdProperty);
			EditorGUILayout.PropertyField(maxValueThresholdProperty);
			EditorGUILayout.PropertyField(unitAngleCountProperty);

			serializedObject.ApplyModifiedProperties();

			if (scope.changed)
			{
				script.OnPropertyChanged();
			}
		}
	}
}
