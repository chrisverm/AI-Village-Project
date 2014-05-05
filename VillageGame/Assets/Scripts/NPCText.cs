using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 
/// Call AddText(string) to add a line of text to the bubble.
/// <code> yourNPC.text.AddText("Hello World") </code>
/// 
/// Call Clear Text to clear the bubble.
/// 
/// Let me know if you want :
///     Functionality for handling newlines in strings passed through.
///     Border to make a "text bubble"
///     Ability to delete specific lines rather than entire list.
///     anything else..    
/// 
/// </summary>
public class NPCText : MonoBehaviour {

	public TextMesh textPrefab;
    public Transform root;
	public float drawDistance;

	private List<TextMesh> meshes;
	private Vector3 initialPos;

	// Use this for initialization
	void Start() 
	{
		meshes = new List<TextMesh>();
        initialPos = root.localPosition;
	}
	
	// Update is called once per frame
	void Update() 
	{
		if (Camera.current != null)
		{
			bool enable = Vector3.Distance(transform.position, Camera.current.transform.position) < drawDistance;

            if (enable)
            {
                if (!root.gameObject.activeSelf)
                    root.gameObject.SetActive(enable);

                transform.LookAt(Camera.current.transform, Vector3.up);
            }
            else
            {
                if (root.gameObject.activeSelf)
                    root.gameObject.SetActive(enable);
            }
		}
	}

	/// <summary>
	/// Adds the text to this text bubble.
    /// 
    /// Each add text will be a new line.
    /// You may use \n to do newlines, but i'm only padding text based on them being one line, so it might draw over another line below it.
    /// If this becomes a big problem let me know, but it should be easier to just manage it this way than actualy keep track of that on my end.
	/// </summary>
	/// <param name="text">Text.</param>
	public void AddText(string text)
	{
        root.position += Vector3.up * 1.5f;
        TextMesh newText = (TextMesh)Instantiate(textPrefab, root.position + Vector3.down * meshes.Count * 1.5f, root.rotation);
        newText.transform.parent = root;
		
		newText.text = text;

		meshes.Add(newText);
	}

	/// <summary>
	/// Clears all text from this text bubble.
	/// </summary>
    public void ClearText()
	{
		foreach (TextMesh obj in meshes)
		{ Destroy(obj.gameObject); }

		root.localPosition = initialPos;
		meshes.Clear();
	}
}
