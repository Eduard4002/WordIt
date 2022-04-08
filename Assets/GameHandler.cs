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
        StartNewGame();
    }
    public void StartNewGame(){
        //Add all the words from a file to the list
        origWordsList.AddRange(File.ReadAllLines(@"words.txt"));
        origWordsList = origWordsList.OrderBy(x => x.Length).ToList();
        int currentAmountOfChar = 3;
        int currentAmountOfWords = 0;
        //Remove all the words that arent with the same amount of characters
        for (int i = 0; i < origWordsList.Count;i++)
        {
            //if(currentAmountOfWords > 50) continue;
            if(origWordsList[i].Length == currentAmountOfChar && currentAmountOfWords < 50){
                copyWordsList.Add(origWordsList[i]);
                currentAmountOfWords++;
            }
            if(origWordsList[i].Length > currentAmountOfChar){
                currentAmountOfChar++;
                currentAmountOfWords = 0;
            }
        }
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
    public string getNewWord(out string originalWord){
        string shuffledWord = ShuffleWord(copyWordsList[levelsCleared + 1]);
        originalWord = copyWordsList[levelsCleared + 1];
        return shuffledWord;
    }
}
