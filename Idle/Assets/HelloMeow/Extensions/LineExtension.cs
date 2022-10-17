/**
 * @author [BoLuo]
 * @email [tktetb@163.com]
 * @create date 2020-07-10 14:09:53
 * @desc [一些与线有关的方法]
 */

#pragma warning disable 0649

using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HM.Extensions
{
   /// <summary>
   /// 一些与线有关的方法
   /// </summary>
   public class LineExtension
   {
       /// <summary>
       /// 获取两条直线之间的交点
       /// </summary>
       /// <param name="p1"></param>
       /// <param name="p2"></param>
       /// <param name="p3"></param>
       /// <param name="p4"></param>
       /// <returns></returns>
       public static Vector2 Inter(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
       {
           float s1 = fArea(p1, p2, p3), s2 = fArea(p1, p2, p4);

           return new Vector2((p4.x * s1 + p3.x * s2) / (s1 + s2), (p4.y * s1 + p3.y * s2) / (s1 + s2));
       }

       /// <summary>
       /// 判断两线段是否相交 （根据快速排斥、跨立实验） 参考链接:1615882553/article/details/80372202
       /// </summary>
       /// <param name="p1">线段1起点</param>
       /// <param name="p2">线段1终点</param>
       /// <param name="p3">线段2起点</param>
       /// <param name="p4">线段2终点</param>
       /// <returns>是否相交</returns>
       public static bool CanInter(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
       {
           //判断两个形成的矩形不相交
           if (Math.Max(p1.x, p2.x) < Math.Min(p3.x, p4.x)) return false;
           if (Math.Max(p1.y, p2.y) < Math.Min(p3.y, p4.y)) return false;
           if (Math.Max(p3.x, p4.x) < Math.Min(p1.x, p2.x)) return false;
           if (Math.Max(p3.y, p4.y) < Math.Min(p1.y, p2.y)) return false;
           //现在已经满足快速排斥实验，那么后面就是跨立实验内容(叉积判断两个线段是否相交)
           if (Area(p1, p3, p2) * Area(p1, p2, p4) < 0) return false;
           if (Area(p3, p1, p4) * Area(p3, p4, p2) < 0) return false;

           return true;

       }

       /// <summary>
       /// 获取两点间的一个随机点
       /// </summary>
       /// <param name="startPoint">起始点</param>
       /// <param name="endPoint">终点</param>
       /// <returns></returns>
       public static Vector2 GetRandomPointFromLine(Vector2 startPoint, Vector2 endPoint)
       {
           return (endPoint - startPoint) * Random.Range(0, 1f) + startPoint;
       }

       #region  求交点的辅助方法

       /// <summary>
       /// 二维向量叉乘
       /// </summary>
       /// <param name="p1">向量1的起点</param>
       /// <param name="p2">向量1的终点</param>
       /// <param name="p3">向量2的起点</param>
       /// <param name="p4">向量2的终点</param>
       /// <returns></returns>
       private static float Cross(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
       {
           return (p2.x - p1.x) * (p4.y - p3.y) - (p2.y - p1.y) * (p4.x - p3.x);
       }

       /// <summary>
       /// 以同一点为起始点的两二维向量向量叉乘
       /// </summary>
       /// <param name="p1"></param>
       /// <param name="p2"></param>
       /// <param name="p3"></param>
       /// <returns></returns>
       private static float Area(Vector2 p1, Vector2 p2, Vector2 p3)
       {
           return Cross(p1, p2, p1, p3);
       }

       /// <summary>
       /// 几何意义: 以（p1,p2）(p1,p3)为两边的平行四边形的面积
       /// </summary>
       /// <param name="p1"></param>
       /// <param name="p2"></param>
       /// <param name="p3"></param>
       /// <returns></returns>
       private static float fArea(Vector2 p1, Vector2 p2, Vector2 p3)
       {
           return Mathf.Abs(Area(p1, p2, p3));
       }


       #endregion
   }
}

#pragma warning restore 0649
