using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SomeScript))]
public class SomeScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.HelpBox("This is a help box", MessageType.Info);
    }
}

//--------------------------------------

// AssetPostProcessor, classe basica per processar imports
public class SpriteProcessor : AssetPostprocessor
{
    //Override del metode de AssetPostProcessor
    void OnPostprocessTexture(Texture2D texture)
    {
        string lowerCaseAssetPath = assetPath.ToLower();
        bool isInSpritesDirectory = lowerCaseAssetPath.IndexOf("/sprites/") != -1;

        if (isInSpritesDirectory)
        {
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.textureType = TextureImporterType.Sprite;
        }
    }
}

//---------------------------------------

//Els wizards son finestres que s'obren normalment des del menu i que quan es creen (s'apreta el seu boto) fan algo i es destrueixen
public class SelectAllOfTag : ScriptableWizard
{

    public string searchTag = "Your tag here";

    //Tot es menu item. Si despres es una finestra o wizard etc dependra del tipus de la classe 
    [MenuItem("My Tools/Select All Of Tag...")]
    static void SelectAllOfTagWizard()
    {
        ScriptableWizard.DisplayWizard<SelectAllOfTag>("Select All Of Tag...", "Make Selection"); 
    }

    //Funcio que s'activa quan s'apreta el boto principal del wizard (en aquest cas el boto es diu "Make Selection")
    void OnWizardCreate()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(searchTag);
        Selection.objects = gameObjects;
    }
}

//--------------------------------------
public class MyWindow : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/My Window")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        MyWindow window = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

    }

}