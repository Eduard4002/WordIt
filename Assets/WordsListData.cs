
using UnityEngine;
[System.Serializable]
public class WordsListData 
{
    public string[] words;
    public WordsListData(GameHandler words)
    {
        this.words = words.copyWordsList.ToArray();
    }
}
