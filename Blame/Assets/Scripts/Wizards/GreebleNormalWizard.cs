using UnityEngine;
using UnityEditor;
using System.Collections;

public class GreebleNormalWizard : ScriptableWizard {

    [MenuItem("Assets/Create/Greeble Normal Texture")]
    private static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<GreebleNormalWizard>("Create Greeble Normal Texture");
    }

    [Range(1, 512)]
    public int width;
    [Range(1, 512)]
    public int height;

    private void OnWizardCreate()
    {
        string path = EditorUtility.SaveFilePanelInProject(
            "Save Greeble Normal Texture",
            "Greeble Normal Texture",
            "asset",
            "Specify where to save the texture");
        if (path.Length > 0)
        {
            Texture2D texture = GreebleGenerator.Create(width, height);
            AssetDatabase.CreateAsset(texture, path);
            Selection.activeObject = texture;
        }
    }
}
