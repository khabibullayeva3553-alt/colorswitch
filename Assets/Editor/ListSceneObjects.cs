using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ListSceneObjects
{
    [MenuItem("Tools/List Scene Objects")]
    static void List()
    {
        var scene = SceneManager.GetActiveScene();
        Debug.Log("Sahna: " + scene.name);
        foreach (var root in scene.GetRootGameObjects())
            Print(root.transform, 0);
    }

    static void Print(Transform t, int depth)
    {
        Debug.Log(new string(' ', depth * 2) + "- " + t.name);
        foreach (Transform child in t)
            Print(child, depth + 1);
    }
}