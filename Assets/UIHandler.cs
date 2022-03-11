using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI wordToFindText;
    string currentWord;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateWordToFind(string shuffledWord, string originalWord){
        wordToFindText.text = shuffledWord;
        currentWord = originalWord;
    }

    public void onEndInput(string input){
        if(input == currentWord){
            //user has guessed correctly
            Debug.Log("Congrats");
        }else{
            //user did not guess correctly
        }
    }
    
}
