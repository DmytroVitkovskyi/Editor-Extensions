using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TextureCounter : EditorWindow
{
    string txt_field1;
    string txt_field2;
    string txt_area1;
    private int intValue;
    private float floatValue;
    private Vector2 vector2DValue;
    private Vector3 vector3DValue;
    float minFloatValue, maxFloatValue;
    private int selectedSizeIndex = 0;

    private enum DamageType
    {
        Fire,
        Frost,
        Electric,
        Shadow
    }

    private DamageType damageType;

    private Vector2 scrollPosition;


    [MenuItem("Window/Texture Counter")]
    public static void Init()
    {
        var window = EditorWindow.GetWindow<TextureCounter>("Texture Counter");
        // Запретить уничтожение этого окна при загрузке новой сцены
        DontDestroyOnLoad(window);
    }
    private void OnGUI()
    {
        // Здесь формируется пользовательский интерфейс редактора
        /*EditorGUILayout.LabelField("Current selected size is "); // + sizes[selectedSizeIndex]
        GUI.Label(new Rect(150, 50, 100, 20), "This is a label!");
        using (var verticalArea = new EditorGUILayout.VerticalScope())
        {
            GUILayout.Label("These");
            GUILayout.Label("Labels");
            GUILayout.Label("Will be shown");
            GUILayout.Label("On top of each other");
        }
        */
        using (var verticalArea = new EditorGUILayout.VerticalScope())
        {
            var buttonClicked = GUILayout.Button("Click me!");
            if (buttonClicked)
            {
                Debug.Log("The custom window's " + "button was clicked!");
            }
        }
        using (var verticalArea = new EditorGUILayout.VerticalScope())
        {
            txt_field1 = EditorGUILayout.TextField(txt_field1);
            
            txt_area1 = EditorGUILayout.TextArea(txt_area1, GUILayout.Height(80));
            
            txt_field2 = EditorGUILayout.DelayedTextField(txt_field2);

            this.intValue = EditorGUILayout.IntField("Int", this.intValue);
            this.floatValue = EditorGUILayout.FloatField("Float", this.floatValue);
            this.vector2DValue = EditorGUILayout.Vector2Field("Vector 2D", this.vector2DValue);
            this.vector3DValue = EditorGUILayout.Vector3Field("Vector 3D", this.vector3DValue);

            var minIntValue = 0;
            var maxIntValue = 10;
            this.intValue = EditorGUILayout.IntSlider(this.intValue, minIntValue, maxIntValue);
            
            EditorGUILayout.Space();

            var minLimit = 0;
            var maxLimit = 10;
            EditorGUILayout.MinMaxSlider(ref minFloatValue, ref maxFloatValue, minLimit, maxLimit);
            GUILayout.Label(minFloatValue.ToString());
            GUILayout.Label(maxFloatValue.ToString());

            EditorGUILayout.Space();

            var sizes = new string[] { "small", "medium", "large" };
            selectedSizeIndex = EditorGUILayout.Popup(selectedSizeIndex, sizes);
            GUILayout.Label(selectedSizeIndex.ToString());

            damageType = (DamageType)EditorGUILayout.EnumPopup(damageType);
        }
        using (var scrollView = new EditorGUILayout.ScrollViewScope(this.scrollPosition))
        {
            this.scrollPosition = scrollView.scrollPosition;
            GUILayout.Label("These");
            GUILayout.Label("Labels");
            GUILayout.Label("Will be shown");
            GUILayout.Label("On top of each other");
        }
        using (var vertical = new EditorGUILayout.VerticalScope())
        {
            // Получить список всех текстур
            var paths = AssetDatabase.FindAssets("t:texture");
            // Получить длину списка
            var count = paths.Length;
            // Вывести надпись
            EditorGUILayout.LabelField("Texture Count", count.ToString());
        }
    }
}
