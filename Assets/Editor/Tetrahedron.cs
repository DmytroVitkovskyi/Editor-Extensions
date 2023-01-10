using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tetrahedron : ScriptableWizard
{
    // Эта переменная будет выглядеть в окне мастера
    // так же, как в инспекторе
    public Vector3 size = new Vector3(1, 1, 1);

    // Этому методу можно дать любое имя, главное, чтобы
    // он был объявлен статическим и имел атрибут MenuItem
    [MenuItem("GameObject/3D Object/Tetrahedron")]
    static void ShowWizard()
    {
        // Первый параметр - текст в заголовке окна, второй - надпись
        // на кнопке создания
        ScriptableWizard.DisplayWizard<Tetrahedron>(
        "Create Tetrahedron", "Create");
    }
    // Вызывается, когда пользователь щелкает по кнопке Create
    void OnWizardCreate()
    {
        // Создать меш
        var mesh = new Mesh();
        // Создать четыре точки
        Vector3 p0 = new Vector3(0, 0, 0);
        Vector3 p1 = new Vector3(1, 0, 0);
        Vector3 p2 = new Vector3(0.5f, 0, Mathf.Sqrt(0.75f));
        Vector3 p3 = new Vector3(0.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        // Масштабировать в соответствии с размером
        p0.Scale(size);
        p1.Scale(size);
        p2.Scale(size);
        p3.Scale(size);
        
        // Передать список вершин
        mesh.vertices = new Vector3[] { p0, p1, p2, p3 };
        // Передать список треугольников, связанных вершинами
        mesh.triangles = new int[] {
            0,1,2,
            0,2,3,
            2,1,3,
            0,3,1 };
        
        // Обновить некоторые дополнительные данные в меше,
        // используя эти данные
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        
        // Создать игровой объект, использующий меш
        var gameObject = new GameObject("Tetrahedron");
        var meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        var meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));
    }

    // Вызывается, когда пользователь изменяет какое-то значение в мастере
    void OnWizardUpdate()
    {
        // Проверить допустимость введенных значений
        if (this.size.x <= 0 || this.size.y <= 0 || this.size.z <= 0)
        {
            // Когда isValid получает значение true, разрешается
            // щелкнуть по кнопке Create
            this.isValid = false;
            // Объяснить причину
            this.errorString = "Size cannot be less than zero";
        }
        else
        {
            // Пользователь может щелкнуть по кнопке Create, поэтому
            // разрешим ему это и очистим сообщение об ошибке
            this.errorString = null;
            this.isValid = true;
        }
    }
}
