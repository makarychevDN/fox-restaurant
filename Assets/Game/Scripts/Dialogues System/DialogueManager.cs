using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace foxRestaurant
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private DialogueDisplayer dialogueDisplayer;
        [SerializeField] private List<VoiceKeyPair> voices;
        [SerializeField] private int chunkSize = 3;
        [SerializeField] private float delayBetweenChunks = 0.1f;

        private Dictionary<string, Action<string>> commands;
        private Dictionary<string, AudioClip> voiceMap;
        private float pauseTime;
        private bool skipRequested;

        public UnityEvent<string> OnChangeEmotion;

        private void Awake()
        {
            if (chunkSize <= 0)
                chunkSize = 1;

            commands = new Dictionary<string, Action<string>>()
            {
                { "pause", Pause },
                { "delay", SetDelay },
                { "color", SetTextColor },
                { "chunk", SetChunkSize },
                { "emote", OnChangeEmotion.Invoke },
                { "voice", SetVoice },
                { "volume", SetVolume },
                { "pitch", SetPitch },
                { "pitch delta", SetPitchDelta }
            };

            voiceMap = new();
            foreach (var v in voices)
            {
                if (!string.IsNullOrEmpty(v.key) && v.voice != null)
                    voiceMap[v.key] = v.voice;
            }
        }

        public async Task DisplayDialogueLine(string text)
        {
            dialogueDisplayer.Show(text);
            skipRequested = false;

            for (int i = 0; i < text.Length;)
            {
                if (text[i] == '<')
                {
                    i += TryToExecuteCommand(text, i);

                    if (pauseTime > 0 && !skipRequested)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(pauseTime));
                        pauseTime = 0;
                    }
                }
                else
                {
                    int nextCommandIndex = text.IndexOf('<', i);
                    if (nextCommandIndex == -1) nextCommandIndex = text.Length;

                    int chunkLength = Math.Min(chunkSize, nextCommandIndex - i);
                    string chunk = text.Substring(i, chunkLength);

                    dialogueDisplayer.Print(chunk);
                    i += chunkLength;

                    if(!skipRequested)
                        await Task.Delay(TimeSpan.FromSeconds(delayBetweenChunks));
                }
            }

            await WaitForMouseClick();
            dialogueDisplayer.Hide();
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

        private async Task WaitForMouseClick()
        {
            while (!Input.GetMouseButtonDown(0))
            {
                await Task.Yield();
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                skipRequested = true;
            }
        }

        private void Pause(string pauseTime) => this.pauseTime = pauseTime.ParseFloatSafe();
        private void SetDelay(string delay) => delayBetweenChunks = delay.ParseFloatSafe();
        private void SetTextColor(string color) => dialogueDisplayer.Print($"<color={color}>", false);
        private void SetChunkSize(string size) => chunkSize = Math.Clamp(Convert.ToInt32(size), 1, 10);
        private void SetVoice(string voiceKey) => dialogueDisplayer.SetSound(voiceMap[voiceKey]);
        private void SetVolume(string voiceKey) => dialogueDisplayer.SetVolume(voiceKey);
        private void SetPitch(string voiceKey) => dialogueDisplayer.SetPitch(voiceKey);
        private void SetPitchDelta(string voiceKey) => dialogueDisplayer.SetPitchDelta(voiceKey);
    }

    [Serializable]
    public struct VoiceKeyPair
    {
        public string key;
        public AudioClip voice;
    }
}