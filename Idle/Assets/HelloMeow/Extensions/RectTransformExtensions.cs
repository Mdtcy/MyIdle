using System.Collections.Generic;
using UnityEngine;

namespace HM.Extensions
{
    public static class RectTransformExtensions
    {
        public static RectTransform GetLastChild(this RectTransform trans)
        {
            if (trans.childCount > 0)
            {
                return trans.GetChild(trans.childCount - 1) as RectTransform;
            }
            return null;
        }
        public static void SetDefaultScale(this RectTransform trans)
        {
            trans.localScale = new Vector3(1, 1, 1);
        }

        public static void SetAnchorMin(this RectTransform trans, Vector2 v)
        {
            trans.anchorMin = v;
        }

        public static void SetRotationY(this RectTransform trans, float v)
        {
            var cur = trans.rotation;
            trans.rotation = new Quaternion(cur.x, v, cur.z, cur.w);
        }

        public static void SetRotationZ(this RectTransform trans, float v)
        {
            var cur = trans.rotation;
            trans.rotation = new Quaternion(cur.x, cur.y, v, cur.w);
        }

        public static void SetLocalRotationZ(this RectTransform trans, float v)
        {
            var cur = trans.localRotation;
            trans.localRotation = new Quaternion(cur.x, cur.y, v, cur.w);
        }

        public static void SetAnchorMax(this RectTransform trans, Vector2 v)
        {
            trans.anchorMax = v;
        }

        public static void SetPivot(this RectTransform trans, Vector2 v)
        {
            trans.pivot = v;
        }

        public static void SetPivotAndAnchors(this RectTransform trans, Vector2 aVec)
        {
            trans.pivot     = aVec;
            trans.anchorMin = aVec;
            trans.anchorMax = aVec;
        }

        public static Vector2 GetSizeByCanvasScale(this RectTransform trans)
        {
            var lossyScale = trans.lossyScale;
            var rect       = trans.rect;
            return new Vector2(rect.size.x * lossyScale.x, rect.size.y * lossyScale.y);
        }

        public static Vector2 GetSize(this RectTransform trans)
        {
            return trans.rect.size;
        }

        public static float GetWidth(this RectTransform trans)
        {
            return trans.rect.width;
        }

        public static float GetHeight(this RectTransform trans)
        {
            return trans.rect.height;
        }

        public static void SetPosition3(this RectTransform trans, Vector3 pos)
        {
            trans.localPosition = pos;
        }

        public static void SetPosition2(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x, newPos.y, trans.localPosition.z);
        }

        public static void SetLeftBottomPosition(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width),
                                              newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
        }

        public static void SetLeftTopPosition(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width),
                                              newPos.y - ((1f - trans.pivot.y) * trans.rect.height),
                                              trans.localPosition.z);
        }

        public static void SetRightBottomPosition(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width),
                                              newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
        }

        public static void SetRightTopPosition(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width),
                                              newPos.y - ((1f - trans.pivot.y) * trans.rect.height),
                                              trans.localPosition.z);
        }

        public static void SetSize(this RectTransform trans, Vector2 newSize)
        {
            Vector2 oldSize   = trans.rect.size;
            Vector2 deltaSize = newSize - oldSize;
            trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
            trans.offsetMax = trans.offsetMax +
                              new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
        }

        public static void SetWidth(this RectTransform trans, float newSize)
        {
            SetSize(trans, new Vector2(newSize, trans.rect.size.y));
        }

        public static void SetHeight(this RectTransform trans, float newSize)
        {
            SetSize(trans, new Vector2(trans.rect.size.x, newSize));
        }

        static Vector2 sc = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        public static Vector2 ScreenCenter()
        {
            return sc;
        }

        public static float LX(this RectTransform trans)
        {
            return trans.localPosition.x;
        }

        public static float LY(this RectTransform trans)
        {
            return trans.localPosition.y;
        }

        public static float LZ(this RectTransform trans)
        {
            return trans.localPosition.z;
        }

        public static Vector2 CanvasSize(this RectTransform trans)
        {
            var    go     = trans.gameObject;
            Canvas canvas = go.GetComponentInParent<Canvas>();
            return GetSize(canvas.gameObject.RTrans());
        }

        public static void AddTo(this RectTransform trans, GameObject go, bool stays = false)
        {
            trans.gameObject.transform.SetParent(go.transform, stays);
        }

        public static void AddToRelNode(this RectTransform child,
                                        RectTransform parent,
                                        RectTransform node,
                                        Vector2 childAp,
                                        Vector2 relAp,
                                        Vector2 extraPos)
        {
            child.transform.SetParent(parent.transform, false);

            var nodePos = new Vector2(node.position.x, node.position.y);

            nodePos = new Vector2(
                                  node.GetSize().x * (relAp.x - node.pivot.x) + nodePos.x,
                                  node.GetSize().y * (relAp.y - node.pivot.y) + nodePos.y);

            nodePos = new Vector2(
                                  nodePos.x + child.GetSize().x * (child.pivot.x - childAp.x),
                                  nodePos.y + child.GetSize().y * (child.pivot.y - childAp.y)
                                 );

            nodePos = nodePos + extraPos;

            child.position = nodePos;
        }

        public static void SetPositionY(this RectTransform trans, float v)
        {
            trans.position = new Vector3(trans.position.x, v);
        }

        public static void SetRelativePositionX(this RectTransform trans, float v)
        {
            trans.position = new Vector3(trans.position.x + v, trans.position.y);
        }

        public static void SetRelativePositionY(this RectTransform trans, float v)
        {
            trans.position = new Vector3(trans.position.x, v + trans.position.y);
        }

        public static Vector2 RelativePosition(this RectTransform node, Vector2 relAp)
        {
            var nodePos = node.anchoredPosition;
            return new Vector2(
                               node.GetSizeByCanvasScale().x * (relAp.x - node.pivot.x) + nodePos.x,
                               node.GetSizeByCanvasScale().y * (relAp.y - node.pivot.y) + nodePos.y);
        }

    public static void SetLocalScaleX(this RectTransform trans, float v)
        {
            trans.localScale = new Vector3(v, trans.localScale.y, trans.localScale.z);
        }

        public static void SetLocalScaleY(this RectTransform trans, float v)
        {
            trans.localScale = new Vector3(trans.localScale.x, v, trans.localScale.z);
        }

        public static void SetAnchoredPositionX(this RectTransform trans, float v)
        {
            trans.anchoredPosition = new Vector2(v, trans.anchoredPosition.y);
        }

        public static void SetAnchoredPositionY(this RectTransform trans, float v)
        {
            trans.anchoredPosition = new Vector2(trans.anchoredPosition.x, v);
        }

        public static void RemoveAllChildren(this RectTransform trans)
        {
            List<GameObject> childList = new List<GameObject>();
            int childCount = trans.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                GameObject child = trans.transform.GetChild(i).gameObject;
                childList.Add(child);
            }
            for (int i = 0; i < childCount; i++)
            {
                UnityEngine.Object.DestroyImmediate(childList[i]);
            }
        }

        public static Vector3 AP_CC3 = new Vector3(0.5f, 0.5f);
        public static Vector3 AP_CL3 = new Vector3(0.0f, 0.5f);
        public static Vector3 AP_CR3 = new Vector3(1.0f, 0.5f);
        public static Vector3 AP_TL3 = new Vector3(0.0f, 1.0f);
        public static Vector3 AP_TC3 = new Vector3(0.5f, 1.0f);
        public static Vector3 AP_TR3 = new Vector3(1.0f, 1.0f);
        public static Vector3 AP_BL3 = new Vector3(0.0f, 0.0f);
        public static Vector3 AP_BC3 = new Vector3(0.5f, 0.0f);
        public static Vector3 AP_BR3 = new Vector3(1.0f, 0.0f);
    }
}