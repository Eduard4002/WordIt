using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
public class GameHandler : MonoBehaviour
{
    public UIHandler UIHandler;
    public List<string> origWordsList = new List<string>();
    public List<string> copyWordsList = new List<string>();

    public int levelsCleared = 0;
    
        
    // Start is called before the first frame update
    void Start()
    {
        if(SaveSystem.LoadData() == null){
            levelsCleared = 0;
            SavePlayer();
            Debug.Log("Testing");
        }else{
            loadData();
        }
        
        StartNewGame();
        
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            UIHandler.openPausePanel();
        }
    
    }
    public void StartNewGame(){
        copyWordsList = SaveSystem.LoadWordsList().words.ToList();
        UIHandler.updateWordToFind(copyWordsList[levelsCleared]);
    }

    public void JumpTo(int level){
        levelsCleared = level;
        UIHandler.updateWordToFind(copyWordsList[levelsCleared]);
        //UIHandler.updateScore(levelsCleared);
    }
    public string ShuffleWord(string str)
    {
        System.Random rng = new System.Random();
        char[] array = str.OrderBy(item => rng.Next()).ToArray();
        return new string(array);
    }
    public void SavePlayer(){
        SaveSystem.SaveData(this);
    }
    public void SaveWordsList(){
        SaveSystem.SaveWordsList(this);
    }
    public void loadData(){
        PlayerData data = SaveSystem.LoadData();
        levelsCleared = data.levelsCleared;
        UIHandler.levelText.text = "Level: "+levelsCleared;
    }
    public string getNewWord(out string originalWord){
        //make sure that the word gets actually shuffled
        string shuffledWord = ShuffleWord(copyWordsList[levelsCleared]);
        while(shuffledWord == copyWordsList[levelsCleared]){
            shuffledWord = ShuffleWord(copyWordsList[levelsCleared]);
        }
        originalWord = copyWordsList[levelsCleared];
        
        return shuffledWord;
    }
}
