using UnityEngine;
using UnityEditor;

public class PassengerGenerator : EditorWindow {

    [MenuItem("LD43/PassengerGenerator")]
    private static void ShowWindow() {
        var window = GetWindow<PassengerGenerator>();
        window.titleContent = new GUIContent("PassengerGenerator");
        window.Show();
    }

    string firstName;
    string lastName;
    string nationality;
    private void OnGUI() 
    {
        GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
                EditorGUILayout.Foldout(true, "Infos");
                    firstName = EditorGUILayout.TextField("First name", firstName);
                    lastName = EditorGUILayout.TextField("Last name", lastName);
                    nationality = EditorGUILayout.TextField("Nationality name", nationality);
                EditorGUILayout.Foldout(true, "Graphic");
                    firstName = EditorGUILayout.TextField("First name", firstName);
                    lastName = EditorGUILayout.TextField("Last name", lastName);
                    nationality = EditorGUILayout.TextField("Nationality name", nationality);
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            if(GUILayout.Button("Generate"))
            {
                
            }
            GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }
}