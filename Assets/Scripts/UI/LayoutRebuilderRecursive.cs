using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LayoutRebuilderRecursive : MonoBehaviour
    {
        public void Start()
        {
            RefreshLayoutGroupsImmediateAndRecursive(gameObject);
        }

        private static void RefreshLayoutGroupsImmediateAndRecursive(GameObject root)
        {
            foreach (var layoutGroup in root.GetComponentsInChildren<LayoutGroup>())
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.transform as RectTransform);
            }
        }
    }
}