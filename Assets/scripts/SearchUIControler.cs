using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SearchUIControler : MonoBehaviour
{
    public InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to the OnEndEdit event of the input field
        inputField.onEndEdit.AddListener(OnEndEdit);
    }

    // This function is called when the user finishes editing the input field
    void OnEndEdit(string text)
    {
        // Convert the text to uppercase
        string uppercaseText = text.ToUpper();

        // Set the text of the input field to the uppercase text
        inputField.text = uppercaseText;
    }
}
