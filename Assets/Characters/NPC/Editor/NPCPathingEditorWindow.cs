using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class NPCPathingWindow : EditorWindow
{
    static NPCPathing[] NpcsInScene = new NPCPathing[0];
    static List<NPCPathing> uncollapsedPaths = new List<NPCPathing>();
    static GameObject pathNodePrefab;
    static GameObject visualPaths;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Game/AI Pathing")]
    static void Init()
    {
        pathNodePrefab = Resources.Load("AIPathNode") as GameObject;
        NPCPathingWindow window = (NPCPathingWindow)EditorWindow.GetWindow(typeof(NPCPathingWindow));
        window.Show();
    }

    void Update()
    {
        NpcsInScene = GameObject.FindObjectsOfType<NPCPathing>();

        foreach(NPCPathing path in NpcsInScene)
        {
            if (uncollapsedPaths.Contains(path))
            {
                for (int i = 0; i < path.pathNodes.Count; i++)
                {
                    if (i < path.pathNodes.Count - 1)
                    {
                        Debug.DrawLine(path.pathNodes[i], path.pathNodes[i + 1], Color.red);
                    }
                    else if (i == path.pathNodes.Count - 1 && path.cyclic)
                    {
                        Debug.DrawLine(path.pathNodes[i], path.pathNodes[0], Color.red);
                    }
                }
            }
        }
    }

    void OnGUI()
    {
        if (!Application.isPlaying)
        {
            foreach (NPCPathing npc in NpcsInScene)
            {
                bool uncollapsed = EditorGUILayout.Foldout(uncollapsedPaths.Contains(npc), npc.gameObject.name);
                if (uncollapsed)
                {
                    if (!uncollapsedPaths.Contains(npc))
                        uncollapsedPaths.Add(npc);

                    List<Vector3> nodes = npc.pathNodes;
                    foreach (Vector3 pos in nodes)
                    {
                        EditorGUILayout.Vector3Field("Position", pos);
                    }
                }
                else if (uncollapsedPaths.Contains(npc))
                {
                    uncollapsedPaths.Remove(npc);
                }
            }
        }
        else
        {
            this.Close();
        }
    }
}