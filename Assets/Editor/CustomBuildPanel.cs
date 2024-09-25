using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomBuildPanel : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/CustomBuildPanel %g")]
    public static void ShowWindow()
    {
        CustomBuildPanel wnd = GetWindow<CustomBuildPanel>();
        wnd.titleContent = new GUIContent("CustomBuildPanel");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
    }
}

