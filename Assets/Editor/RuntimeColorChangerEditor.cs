using UnityEngine;
using System.Collections;
using System.Collections.Generic; // необходимо для словаря
using UnityEditor;

// Это - редактор для RuntimeColorChangers
[CustomEditor(typeof(RuntimeColorChanger))]
// Поддерживает одновременное редактирование нескольких объектов
[CanEditMultipleObjects]
class RuntimeColorChangerEditor : Editor
{
    // Коллекция пар строка/цвет
    private Dictionary<string, Color> colorPresets;
    // Представляет свойство "color" во всех выбранных объектах
    private SerializedProperty colorProperty;

    // Вызывается при первом появлении редактора
    public void OnEnable()
    {
        // Подготовить список предопределенных цветов
        colorPresets = new Dictionary<string, Color>();
        colorPresets["Red"] = Color.red;
        colorPresets["Green"] = Color.green;
        colorPresets["Blue"] = Color.blue;
        colorPresets["Yellow"] = Color.yellow;
        colorPresets["White"] = Color.white;
        // Получить свойство из объекта (объектов),
        // выбранного в данный момент
        colorProperty = serializedObject.FindProperty("color");
    }

    // Вызывается для отображения пользовательского
    // интерфейса в инспекторе
    public override void OnInspectorGUI()
    {
        // Гарантировать актуальность состояния serializedObject
        serializedObject.Update();

        // Отобразить интерфейс инспектора по умолчанию
        // DrawDefaultInspector();

        // Начать создание вертикального списка элементов управления
        using (var area = new EditorGUILayout.VerticalScope())
        {
            // Для каждого предопределенного цвета...
            foreach (var preset in colorPresets)
            {
                // Отобразить кнопку
                var clicked = GUILayout.Button(preset.Key);
                // Если был произведен щелчок, обновить свойство
                if (clicked)
                {
                    colorProperty.colorValue = preset.Value;
                }
            }
            // В заключение вывести текстовое поле ввода, чтобы
            // дать возможность напрямую ввести код цвета
            EditorGUILayout.PropertyField(colorProperty);
        }
        // Применить любые произведенные изменения
        serializedObject.ApplyModifiedProperties();
    }
}