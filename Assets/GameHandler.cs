using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameHandler : MonoBehaviour
{
    public UIHandler UIHandler;
    public List<string> wordsList = new List<string>();
    public int amountOfChar = 3;
    // Start is called before the first frame update
    void Start()
    {
        //Add all the words from a file to the list
        wordsList.AddRange(File.ReadAllLines(@"words.txt"));

        //Remove all the words that arent with the same amount of characters
       for (int i = wordsList.Count - 1; i >= 0; i--)
        {
            if (wordsList[i].Length != amountOfChar)
                wordsList.RemoveAt(i);
        }
        UIHandler.updateWordToFind(Shuffle(wordsList[0]), wordsList[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string Shuffle( string str)
    {
        char[] array = str.ToCharArray();
        System.Random rng = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        return new string(array);
    }
}
