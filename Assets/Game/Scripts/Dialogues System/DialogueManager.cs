using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private DialogueDisplayer dialogueDisplayer;
        [SerializeField] private int lettersPerOnceCount;
        [SerializeField] private float delayBetweenChunks = 0.1f;
        [SerializeField] private string test;
        private Dictionary<string, Action<string>> commands;

        private void Awake()
        {
            commands = new Dictionary<string, Action<string>>()
            {
                { "x2", X2 }
            };

            DisplayDialogueLine(test);
        }

        public async Task DisplayDialogueLine(string text)
        {
            for(int i = 0; i < text.Length;)
            {
                int nextCommandIndex = text.IndexOf('<', i);
                if (nextCommandIndex == -1) nextCommandIndex = text.Length;

                int chunkLength = Math.Min(lettersPerOnceCount, nextCommandIndex - i);

                if (chunkLength > 0)
                {
                    string chunk = text.Substring(i, chunkLength);
                    dialogueDisplayer.Print(chunk);
                    i += chunkLength;
                }
                else
                {
                    i += TryToExecuteCommand(text, i);
                }
                await Task.Delay(TimeSpan.FromSeconds(delayBetweenChunks));
            }
        }

        private void X2(string x)
        {
            print(Convert.ToInt32(x) * 2);
        }

        private int TryToExecuteCommand(string text, int startIndex)
        {
            if (text[startIndex] != '<')
                return 1;

            int endIndex = text.IndexOf('>', startIndex);
            int splitterIndex = text.IndexOf(':', startIndex, endIndex - startIndex);

            string commandKey;
            string parameter = "";

            if (splitterIndex != -1)
            {
                commandKey = text.Substring(startIndex + 1, splitterIndex - startIndex - 1);
                parameter = text.Substring(splitterIndex + 1, endIndex - splitterIndex - 1);
            }
            else
            {
                commandKey = text.Substring(startIndex + 1, endIndex - startIndex - 1);
            }

            commands[commandKey](parameter);

            return endIndex - startIndex + 1;
        }
    }
}