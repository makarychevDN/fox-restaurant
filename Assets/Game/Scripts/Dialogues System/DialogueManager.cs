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
        private float pauseTime;

        private void Awake()
        {
            commands = new Dictionary<string, Action<string>>()
            {
                { "pause", Pause },
                { "delay", SetDelay },
                { "color", SetTextColor }
            };

            DisplayDialogueLine(test);
        }

        public async Task DisplayDialogueLine(string text)
        {
            dialogueDisplayer.Show();

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

                await MakeDelayBetweenChunks();
            }

            //dialogueDisplayer.Hide();
        }

        private async Task MakeDelayBetweenChunks()
        {
            await Task.Delay(TimeSpan.FromSeconds(pauseTime == 0 ? delayBetweenChunks : pauseTime));
            pauseTime = 0;
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

        private void Pause(string pauseTime) => this.pauseTime = pauseTime.ParseFloatSafe();
        private void SetDelay(string delay) => delayBetweenChunks = delay.ParseFloatSafe();
        private void SetTextColor(string color) => dialogueDisplayer.Print($"<color={color}>");
    }
}