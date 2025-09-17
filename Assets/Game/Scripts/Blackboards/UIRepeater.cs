using System.Collections.Generic;
using UnityEngine;

namespace foxRestaurant
{
    /// <summary>
    /// Универсальный компонент для повторяющихся UI элементов (панелей).
    /// </summary>
    public abstract class UIRepeater<TPanel, TData> : MonoBehaviour
        where TPanel : Component
    {
        [SerializeField] private Transform parentForPanels;
        [SerializeField] private TPanel panelPrefab;

        private readonly List<TPanel> panels = new List<TPanel>();

        /// <summary>
        /// Обновить список UI элементов по данным.
        /// </summary>
        public void UpdatePanels(IReadOnlyList<TData> dataList)
        {
            int panelsCount = Mathf.Max(dataList.Count, panels.Count);

            for (int i = 0; i < panelsCount; i++)
            {
                if (i < dataList.Count)
                {
                    var panel = GetPanel(i);
                    Bind(panel, dataList[i]);
                    panel.gameObject.SetActive(true);
                }
                else
                {
                    panels[i].gameObject.SetActive(false);
                }
            }
        }

        private TPanel GetPanel(int index)
        {
            return panels.Count > index ? panels[index] : SpawnPanel();
        }

        private TPanel SpawnPanel()
        {
            var panel = Instantiate(panelPrefab, parentForPanels);
            panel.transform.localScale = Vector3.one;
            panels.Add(panel);
            return panel;
        }

        /// <summary>
        /// Как привязывать данные к панели.
        /// </summary>
        protected abstract void Bind(TPanel panel, TData data);
    }
}