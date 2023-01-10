using UnityEngine;
using System.Collections;
using System.Collections.Generic; // ���������� ��� �������
using UnityEditor;

// ��� - �������� ��� RuntimeColorChangers
[CustomEditor(typeof(RuntimeColorChanger))]
// ������������ ������������� �������������� ���������� ��������
[CanEditMultipleObjects]
class RuntimeColorChangerEditor : Editor
{
    // ��������� ��� ������/����
    private Dictionary<string, Color> colorPresets;
    // ������������ �������� "color" �� ���� ��������� ��������
    private SerializedProperty colorProperty;

    // ���������� ��� ������ ��������� ���������
    public void OnEnable()
    {
        // ����������� ������ ���������������� ������
        colorPresets = new Dictionary<string, Color>();
        colorPresets["Red"] = Color.red;
        colorPresets["Green"] = Color.green;
        colorPresets["Blue"] = Color.blue;
        colorPresets["Yellow"] = Color.yellow;
        colorPresets["White"] = Color.white;
        // �������� �������� �� ������� (��������),
        // ���������� � ������ ������
        colorProperty = serializedObject.FindProperty("color");
    }

    // ���������� ��� ����������� �����������������
    // ���������� � ����������
    public override void OnInspectorGUI()
    {
        // ������������� ������������ ��������� serializedObject
        serializedObject.Update();

        // ���������� ��������� ���������� �� ���������
        // DrawDefaultInspector();

        // ������ �������� ������������� ������ ��������� ����������
        using (var area = new EditorGUILayout.VerticalScope())
        {
            // ��� ������� ����������������� �����...
            foreach (var preset in colorPresets)
            {
                // ���������� ������
                var clicked = GUILayout.Button(preset.Key);
                // ���� ��� ���������� ������, �������� ��������
                if (clicked)
                {
                    colorProperty.colorValue = preset.Value;
                }
            }
            // � ���������� ������� ��������� ���� �����, �����
            // ���� ����������� �������� ������ ��� �����
            EditorGUILayout.PropertyField(colorProperty);
        }
        // ��������� ����� ������������� ���������
        serializedObject.ApplyModifiedProperties();
    }
}