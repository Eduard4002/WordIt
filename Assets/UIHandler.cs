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
    public TextMeshProUGUI levelText;
    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject mainPanel;
    public GameObject jumpToPanel;
    Dictionary<int, bool> charsCleared = new Dictionary<int, bool>();
    
    // Start is called before the first frame update
    void Start()
    {
       //make sure all panels are closed but main panel is open
       closePausePanel();
       closeJumpToPanel();
       openMainPanel();
    }
    
    public void updateWordToFind(string shuffledWord){
        wordToFindText.text = gameHandler.getNewWord(out currentWord);
        levelText.text = "Level: "+gameHandler.levelsCleared;
    }
    #region PANELS
    public void openPausePanel() => pausePanel.SetActive(true);
    public void closePausePanel() => pausePanel.SetActive(false);
    public void openJumpToPanel() => jumpToPanel.SetActive(true);
    public void closeJumpToPanel() => jumpToPanel.SetActive(false);
    public void openMainPanel() => mainPanel.SetActive(true);
    public void closeMainPanel() => mainPanel.SetActive(false);
    #endregion
    public void JumpToFunc(int amountOfChar){
        gameHandler.JumpTo((amountOfChar-3) * 50);
        charsCleared.Clear();
    }
    public void ResetLevel(){
        gameHandler.JumpTo(0);
        charsCleared.Clear();
        gameHandler.SavePlayer();
    }
    public void QuitApp(){
        gameHandler.SavePlayer();
        Application.Quit();
    }
    public void onChangeInput(string input){
        wordToFindText.ForceMeshUpdate();
        charsCleared.Clear();
        if(input == currentWord){
            //user has guessed correctly
            gameHandler.levelsCleared++;
            gameHandler.SavePlayer();
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
        bool[] alreadyDid = new bool[wordToFindText.text.Length];
        //resetting the values
        for(int i = 0; i < wordToFindText.text.Length;++i){
            charsCleared.Add(i,false);
            alreadyDid[i] = false;
        }
        for(int i = 0; i < wordToFindText.text.Length;i++){
             for(int j = 0; j < input.Length;j++){
                if(wordToFindText.text[i] == input[j] && (alreadyDid[j] == false)){
                    charsCleared[i] = true;
                    alreadyDid[j] = true;
                    break;
                }
             }
        }
        var info = wordToFindText.textInfo;
        foreach(KeyValuePair<int, bool> kvp in charsCleared){
            if(kvp.Value == true){
                var charInfo = info.characterInfo[kvp.Key];
                //var verts = info.meshInfo[charInfo.materialReferenceIndex].vertices;
                var meshInfo = info.meshInfo[charInfo.materialReferenceIndex];
                for(int j = 0; j < 4; ++j){
                    var verIndex = charInfo.vertexIndex + j;
                    //var orig = verts[charInfo.vertexIndex + j];
                    //verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x *0.1f) * 10, 0);
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
