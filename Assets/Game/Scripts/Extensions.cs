using DG.Tweening;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

namespace foxRestaurant
{
    public static class Extensions
    {
        public static T GetRandomElement<T>(this List<T> list)
        {
            if (list == null || list.Count == 0) return default;
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static UnityEngine.Vector2 GetSpriteSizeInPixels(this Sprite sprite)
        {
            UnityEngine.Vector2 itemSpriteSize = sprite.bounds.size;
            float pixelsPerUnit = sprite.pixelsPerUnit;
            itemSpriteSize.y *= pixelsPerUnit;
            itemSpriteSize.x *= pixelsPerUnit;
            return itemSpriteSize;
        }

        public static (int x, int y) IndexOf<T>(this T[,] array, T value)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    if (EqualityComparer<T>.Default.Equals(array[i, j], value))
                        return (i, j);

            return (-1, -1);
        }

        public static string[,] ToStringsArray(this TextAsset csvFile, char delimiter = ',')
        {
            if (csvFile == null || string.IsNullOrWhiteSpace(csvFile.text))
                return new string[0, 0];

            string[] lines = csvFile.text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            int rows = lines.Length;
            int cols = 0;

            foreach (string line in lines)
            {
                int count = line.Split(delimiter).Length;
                if (count > cols) cols = count;
            }

            string[,] result = new string[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                string[] values = lines[r].Split(delimiter);
                for (int c = 0; c < values.Length; c++)
                {
                    result[r, c] = values[c].Trim();
                }
            }

            return result;
        }

        public static Color HexToColor(string hex)
        {
            if (ColorUtility.TryParseHtmlString(hex, out Color color))
            {
                return color;
            }

            Debug.LogWarning($"incorrect hex-color: {hex}. Return Color.white");
            return Color.white;
        }

        public static float ParseFloatSafe(this string raw, float fallback = 0f)
        {
            if (string.IsNullOrWhiteSpace(raw))
            {
                Debug.LogWarning("[ParseUtils] Пустая строка для парсинга float. Использую fallback.");
                return fallback;
            }

            raw = raw.Trim();

            if (float.TryParse(raw, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out float result))
                return result;

            if (float.TryParse(raw, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out result))
                return result;

            Debug.LogWarning($"[ParseUtils] Не удалось распарсить float из строки '{raw}'. Использую fallback {fallback}.");
            return fallback;
        }

        public static async Task AnimateThreshold(this Image image, string propertyName, float from, float to, float duration)
        {
            // Сначала устанавливаем стартовое значение
            image.raycastTarget = true;
            image.material.SetFloat(propertyName, from);

            // Создаём твин
            Tween tween = DOTween.To(
                () => image.material.GetFloat(propertyName),
                x => image.material.SetFloat(propertyName, x),
                to,
                duration
            );

            // Ждём завершения
            await tween.AsyncWaitForCompletion();
            image.raycastTarget = false;
        }

        public static async Task FadeIn(this Image image)
        {
            await image.AnimateThreshold("_Fading", 0, 1, 1f);
        }

        public static async Task FadeOut(this Image image)
        {
            await image.AnimateThreshold("_Fading", 1, 0, 1f);
        }

        public static void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
        }
    }
}