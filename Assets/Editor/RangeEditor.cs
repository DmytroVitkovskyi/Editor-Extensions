using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Range))]
public class RangeEditor : PropertyDrawer
{
    // Этот редактор будет отображать два ряда элементов
    // управления друг под другом - в первом будет находиться ползунок,
    // а во втором - текстовые поля ввода для непосредственного
    // изменения значений
    const int LINE_COUNT = 2;
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Вернуть высоту пространства в пикселах,
        // которые займут это свойство в инспекторе

        return base.GetPropertyHeight(property, label) * LINE_COUNT;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Получить объекты, представляющие поля внутри
        // данного свойства Range
        var minProperty = property.FindPropertyRelative("min");
        var maxProperty = property.FindPropertyRelative("max");
        var minLimitProperty = property.FindPropertyRelative("minLimit");
        var maxLimitProperty = property.FindPropertyRelative("maxLimit");
        // Любые элементы управления внутри PropertyScope будут
        // правильно работать с шаблонами - значения, отличающиеся
        // от заданных в шаблоне, будут отображаться жирным; вы сможете
        // щелкнуть на значении правой кнопкой и вернуть значение из шаблона
        using (var propertyScope = new EditorGUI.PropertyScope(position, label, property))
        {
            // Показать надпись; этот метод возвращает объект Rect,
            // определяющий размеры прямоугольника, необходимого для вывода
            // указанной строки
            Rect sliderRect = EditorGUI.PrefixLabel(position, label);
            // Сконструировать прямоугольники для всех элементов управления:
            // вычислить размер одного ряда
            var lineHeight = position.height / LINE_COUNT;
            // Высота ползунка должна совпадать с высотой ряда
            sliderRect.height = lineHeight;
            // Область для двух полей ввода имеет те же размеры,
            // что и область для ползунка, но смещена на один ряд ниже
            var valuesRect = sliderRect;
            valuesRect.y += sliderRect.height;
            // Определить прямоугольники для двух полей ввода
            var minValueRect = valuesRect;
            minValueRect.width /= 2.0f;
            var maxValueRect = valuesRect;
            maxValueRect.width /= 2.0f;
            maxValueRect.x += minValueRect.width;
            // Вывести вещественные значения
            var minValue = minProperty.floatValue;
            var maxValue = maxProperty.floatValue;
            // Начать проверку изменений - это необходимо для
            // поддержки редактирования нескольких объектов
            EditorGUI.BeginChangeCheck();
            // Показать ползунок
            EditorGUI.MinMaxSlider(sliderRect, ref minValue, ref maxValue, minLimitProperty.floatValue,
            maxLimitProperty.floatValue);
            // Показать поля ввода
            minValue = EditorGUI.FloatField(minValueRect, minValue);
            maxValue = EditorGUI.FloatField(maxValueRect, maxValue);
            // Значение изменилось?
            var valueWasChanged = EditorGUI.EndChangeCheck();
            if (valueWasChanged)
            {
                // Сохранить изменившиеся значения
                minProperty.floatValue = minValue;
                maxProperty.floatValue = maxValue;
            }
        }
    }
}
