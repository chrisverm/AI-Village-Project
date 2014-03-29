using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// This is a class that the unity inspector will use when any field is a color.
/// 
/// Issues : In arrays, if you open the RGBA of the first element, any other arrays will also open the RGBA of the first element (WEIRD)
/// </summary>
[CustomPropertyDrawer(typeof(Color))]
public class ColorPropertyDrawer : PropertyDrawer
{
    char[] labels = { 'R', 'G', 'B', 'A' };
    bool open = false;

    const int FOLDOUT_SPACE = 50;
    const int LABLE_SPACE = 14;
    const int HEIGHT_PADDING = 2;
    const int FLOAT_PADDING = 2;

    /// <summary>
    /// Gets the amount of verticle space this property takes up in the inspector.
    /// Normal if foldout isnt open.
    /// Twice + 2 if open.
    /// </summary>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (open) return base.GetPropertyHeight(property, label) * 2 + HEIGHT_PADDING;
        else return base.GetPropertyHeight(property, label);
    }

    /// <summary>
    /// Draw our color property.
    /// 
    /// Draws normal color property, plus a foldout to expose float values for the color (RGBA).
    /// A change in either the normal color prop, or the ARGB float values will affect the same color value.
    /// </summary>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        float indent = EditorGUI.indentLevel * 15;
        Rect rect = position;

        if (open)
        {
            // Ignore the space for the second line.
            rect.height /= 2;
            rect.height -= HEIGHT_PADDING/2;
        }

        rect.width -= FOLDOUT_SPACE;
        EditorGUI.PropertyField(rect, property, label);

        rect.x += rect.width - indent + 15; // 15 to get past the eye dropper part of the color prop.
        rect.width = FOLDOUT_SPACE;
        open = EditorGUI.Foldout(rect, open, "RGBA");

        if (open)
        {
            rect = position;
            rect.height /= 2;
            rect.height -= HEIGHT_PADDING / 2;
            rect.y += rect.height + HEIGHT_PADDING;

            // This is an empty "name" lable on the left of the inspector that when put there gives us the rectangle where content should be.
            rect = EditorGUI.PrefixLabel(rect, new GUIContent(" "));

            float width = rect.width /= 4;
            width -= 3.5f - indent;

            Color color = property.colorValue;

            for (int i = 0; i < 4; i++)
            {
                rect.width = LABLE_SPACE;
                GUI.Label(rect, labels[i] + "");

                rect.x += rect.width - indent;
                rect.width = width - LABLE_SPACE + FLOAT_PADDING; 
                color[i] = EditorGUI.FloatField(rect, color[i]);

                rect.x += rect.width + FLOAT_PADDING;
            }

            property.colorValue = color;
        }
    }
}
