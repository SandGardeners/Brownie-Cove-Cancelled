using UnityEngine;
using UnityEditor;

public class PassengerDatabaseEditor : EditorWindow {

    [MenuItem("LD43/PassengerDatabase")]
    private static void ShowWindow() 
    {
        var window = GetWindow<PassengerDatabaseEditor>();
        window.titleContent = new GUIContent("PassengerDatabase");
        window.database = AssetDatabase.LoadAssetAtPath<PassengersDatabase>("Assets/Resources/PassengersDatabase.asset");
        window.Show();
    }

    PassengersDatabase database;
    private void OnGUI() 
    {

    }
}