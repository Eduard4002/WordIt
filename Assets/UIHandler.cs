using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI wordToFindText;
    string currentWord;

    public GameHandler gameHandler;
    public TMP_InputField mainInputField;
    public TextMeshProUGUI scoreText;
    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject mainPanel;
    public GameObject jumpToPanel;
    // Start is called before the first frame update
    void Start()
    {
       //make sure all panels are closed but main panel is open
       closePanels();
       mainPanel.SetActive(true);
    }
    void closePanels(){
        pausePanel.SetActive(false);
        jumpToPanel.SetActive(false);
    }
    
    public void updateWordToFind(string shuffledWord){
        wordToFindText.text = gameHandler.getNewWord(out currentWord);
        scoreText.text = "Level: "+gameHandler.levelsCleared;
    }

    
    public void openPausePanel(){
        pausePanel.SetActive(true);
    }
    public void closePausePanel(){
        pausePanel.SetActive(false);
    }
    public void openJumpToPanel(){
        jumpToPanel.SetActive(true);
    }
    public void closeJumpToPanel(){
        jumpToPanel.SetActive(false);
    }
    public void JumpToFunc(int amountOfChar){
        gameHandler.JumpTo((amountOfChar-3) * 50);        
    }
    public void onChangeInput(string input){
        wordToFindText.ForceMeshUpdate();
        
        if(input == currentWord){
            //user has guessed correctly
            gameHandler.levelsCleared++;
            //Request new word 
            //string word = gameHandler.getNewWord(out currentWord);
            updateWordToFind(gameHandler.getNewWord(out currentWord));
            mainInputField.text = "";
            return;
        }else if(input.Length == currentWord.Length){
            //user has guessed incorrectly
            mainInputField.text = "";
            return;
        }
        if(input == "" || input.Length > wordToFindText.text.Length) return ;
        

        char[] inputCharArray = input.ToCharArray();
        //char lastInput = inputCharArray[inputCharArray.Length-1];
      
        char[] wordToFindCharArray = wordToFindText.text.ToCharArray();
        if(wordToFindText.text.Contains(inputCharArray[inputCharArray.Length-1].ToString())){
            //wordToFindText.ForceMeshUpdate();
            var info = wordToFindText.textInfo;
            for (int i = 0; i < inputCharArray.Length; ++i)
            {
                    int[] indexesOfChars = AllIndexesOf(wordToFindText.text,inputCharArray[i].ToString());
                    foreach(int index in indexesOfChars){
                        var charInfo = info.characterInfo[index];
                        //var verts = info.meshInfo[charInfo.materialReferenceIndex].vertices;
                        var meshInfo = info.meshInfo[charInfo.materialReferenceIndex];
                        for(int j = 0; j < 4; ++j){
                            var verIndex = charInfo.vertexIndex + j;
                            //var orig = verts[charInfo.vertexIndex + j];
                            //verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x *0.01f) * 10, 0);
                            meshInfo.colors32[verIndex] = new Color32(131, 255, 128, 255);
                        }
                    }
            }
            for(int i = 0; i < info.meshInfo.Length;++i){
                var meshInfo = info.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                meshInfo.mesh.colors32 = meshInfo.colors32;
                wordToFindText.UpdateGeometry(meshInfo.mesh,i);
            }
        }
        
    }
    public int[] AllIndexesOf(string str, string searchstring)
    {
        List<int> charsPos = new List<int>();
        int minIndex = str.IndexOf(searchstring);
        while (minIndex != -1)
        {
            charsPos.Add(minIndex);
            minIndex = str.IndexOf(searchstring, minIndex + searchstring.Length);
        }
        return charsPos.ToArray();
    }
    
    //TODO: change position of the character when user writes on the field input
    /*
    public void onChangeInput(string input){
        if(input == null) return; 
        char[] charsToChangeColor = input.ToCharArray();
        int var = 0;
        int step = 23;
        for(int i = 0; i < charsToChangeColor.Length;i++){
            // Get index of character.
            int charIndex = wordToFindText.text.IndexOf(charsToChangeColor[i],var);
            // Replace text with color value for character.
            wordToFindText.text.Insert(i * step,"<color=#FFFFFF>" + charsToChangeColor[i].ToString() + "</color>" );
            //wordToFindText.text = wordToFindText.text.Replace (wordToFindText.text [charIndex].ToString (), "<color=#FFFFFF>" + wordToFindText.text[charIndex].ToString () + "</color>");
            var++;
        }
        
    }*/
    
}
