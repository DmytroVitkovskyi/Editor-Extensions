using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Range))]
public class RangeEditor : PropertyDrawer
{
    // ���� �������� ����� ���������� ��� ���� ���������
    // ���������� ���� ��� ������ - � ������ ����� ���������� ��������,
    // � �� ������ - ��������� ���� ����� ��� �����������������
    // ��������� ��������
    const int LINE_COUNT = 2;
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // ������� ������ ������������ � ��������,
        // ������� ������ ��� �������� � ����������

        return base.GetPropertyHeight(property, label) * LINE_COUNT;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // �������� �������, �������������� ���� ������
        // ������� �������� Range
        var minProperty = property.FindPropertyRelative("min");
        var maxProperty = property.FindPropertyRelative("max");
        var minLimitProperty = property.FindPropertyRelative("minLimit");
        var maxLimitProperty = property.FindPropertyRelative("maxLimit");
        // ����� �������� ���������� ������ PropertyScope �����
        // ��������� �������� � ��������� - ��������, ������������
        // �� �������� � �������, ����� ������������ ������; �� �������
        // �������� �� �������� ������ ������� � ������� �������� �� �������
        using (var propertyScope = new EditorGUI.PropertyScope(position, label, property))
        {
            // �������� �������; ���� ����� ���������� ������ Rect,
            // ������������ ������� ��������������, ������������ ��� ������
            // ��������� ������
            Rect sliderRect = EditorGUI.PrefixLabel(position, label);
            // ��������������� �������������� ��� ���� ��������� ����������:
            // ��������� ������ ������ ����
            var lineHeight = position.height / LINE_COUNT;
            // ������ �������� ������ ��������� � ������� ����
            sliderRect.height = lineHeight;
            // ������� ��� ���� ����� ����� ����� �� �� �������,
            // ��� � ������� ��� ��������, �� ������� �� ���� ��� ����
            var valuesRect = sliderRect;
            valuesRect.y += sliderRect.height;
            // ���������� �������������� ��� ���� ����� �����
            var minValueRect = valuesRect;
            minValueRect.width /= 2.0f;
            var maxValueRect = valuesRect;
            maxValueRect.width /= 2.0f;
            maxValueRect.x += minValueRect.width;
            // ������� ������������ ��������
            var minValue = minProperty.floatValue;
            var maxValue = maxProperty.floatValue;
            // ������ �������� ��������� - ��� ���������� ���
            // ��������� �������������� ���������� ��������
            EditorGUI.BeginChangeCheck();
            // �������� ��������
            EditorGUI.MinMaxSlider(sliderRect, ref minValue, ref maxValue, minLimitProperty.floatValue,
            maxLimitProperty.floatValue);
            // �������� ���� �����
            minValue = EditorGUI.FloatField(minValueRect, minValue);
            maxValue = EditorGUI.FloatField(maxValueRect, maxValue);
            // �������� ����������?
            var valueWasChanged = EditorGUI.EndChangeCheck();
            if (valueWasChanged)
            {
                // ��������� ������������ ��������
                minProperty.floatValue = minValue;
                maxProperty.floatValue = maxValue;
            }
        }
    }
}
