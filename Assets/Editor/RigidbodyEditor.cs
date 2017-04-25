/*using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Rigidbody))]
public class RigidbodyEditor : Editor
{
	void OnSceneGUI()
	{
		Rigidbody rb = target as Rigidbody;
		Handles.color = Color.red;
		Vector3 centerOfMass = rb.transform.TransformPoint (rb.centerOfMass);
		Vector3 groundPoint = new Vector3 (centerOfMass[0], 0, centerOfMass[2]);
		Handles.SphereCap(1, groundPoint, rb.rotation, 0.3f);
		Handles.DrawDottedLine(centerOfMass, groundPoint, 5);
		Handles.DrawDottedLine(groundPoint, (new Vector3 (groundPoint[0],groundPoint[1], groundPoint[2]+200)), 5);
		Handles.DrawDottedLine(groundPoint, (new Vector3 (groundPoint[0],groundPoint[1], groundPoint[2]-200)), 5);
	}
	public override void OnInspectorGUI()
	{
		GUI.skin = EditorGUIUtility.GetBuiltinSkin(UnityEditor.EditorSkin.Inspector);
		DrawDefaultInspector();
	}
}*/