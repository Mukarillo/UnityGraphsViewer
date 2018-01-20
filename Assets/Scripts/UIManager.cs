using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public Graph graph;
    public InputField inputField;


    private void Awake(){
        instance = this;
    }

    public void SetFunction(){
        graph.SetStringFunction(inputField.text);
    }

    public void ToggleAnimation(bool value){
        graph.animate = value;
    }

    public void SetMessageInInputField(string message){
        inputField.placeholder.GetComponent<Text>().text = message;
    }
}
