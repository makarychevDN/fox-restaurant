using Cysharp.Threading.Tasks;
using DG.Tweening;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
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

        public static async UniTask AnimateThreshold(this Image image, string propertyName, float from, float to, float duration)
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

            if (Time.timeScale == 0)
                tween.SetUpdate(true);

            // Ждём завершения
            await tween.ToUniTask();
            image.raycastTarget = false;
        }

        public static async UniTask FadeIn(this Image image)
        {
            await image.AnimateThreshold("_Fading", 0, 1, 1f);
        }

        public static async UniTask FadeOut(this Image image)
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

        public async static UniTask DotweenStep(this Transform steppingTransform, Vector3 targetPosition, float time)
            => await DotweenStep(steppingTransform, targetPosition, new Vector3(1.1f, 0.75f), time);

        public async static UniTask DotweenStep(this Transform steppingTransform, Vector3 targetPosition, Vector3 squeezeValue, float time)
        {
            steppingTransform.DOMove(targetPosition, time);
            await steppingTransform.DOScale(squeezeValue, time * 0.5f).ToUniTask();
            await steppingTransform.DOScale(Vector3.one, time * 0.5f).ToUniTask();
        }

        public async static UniTask DotweenSteps(this Transform steppingTransform, Vector3 targetPosition, Vector3 squeezeValue, float time, int stepsAmount)
        {
            var startPosition = steppingTransform.position;

            for (int i = 1; i <= stepsAmount; i++)
            {
                var intermediateTargetPosition =
                    Vector3.Lerp(startPosition, targetPosition, (float)i / stepsAmount);

                await DotweenStep(
                    steppingTransform,
                    intermediateTargetPosition,
                    squeezeValue,
                    time / stepsAmount);
            }
        }

        public static Tweener SetCameraShakingLoopAnimation(this Camera camera, float strength) =>
            camera.transform.DOShakePosition(0.3f, strength, 50).SetLoops(-1);

        public static void UpdateCameraShakingLoopAnimation(this Camera camera, ref Tweener shakingCameraLoop, float strength)
        {
            shakingCameraLoop.Kill();
            shakingCameraLoop = SetCameraShakingLoopAnimation(camera, strength);
        }

        public static Func<UniTask> WrapToTask(this Action action)
        {
            return () =>
            {
                action();
                return UniTask.CompletedTask;
            };
        }

        public static void ApplyVolume(this AudioMixer audioMixer, string parameterName, float value)
        {
            // перевод 0–1 в дБ по формуле log10(x) * 20, с защитой от 0
            float dB = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f;
            audioMixer.SetFloat(parameterName, dB);
        }

        public static int ToInt(this bool value) => value ? 1 : 0;

        public static int NegateIfFalse(bool value) => value ? 1 : -1;

        public static async UniTask ShakeCamera(this Camera camera, float strenght, int shakingValue = 50)
        {
            await camera.DOShakePosition(0.3f, strenght, shakingValue).ToUniTask();
            camera.transform.position = new Vector3(0, 0, -10);
        }
    }
}