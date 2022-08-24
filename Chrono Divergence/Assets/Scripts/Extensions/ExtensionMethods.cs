using System.Collections.Generic;
using UnityEngine;

namespace ChronoDivergence
{
    static class ExtensionMethods
    {
        /// <summary>
        /// Rounds Vector3.
        /// </summary>
        /// <param name="vector3"></param>
        /// <param name="decimalPlaces"></param>
        /// <returns></returns>
        public static Vector3 Round(this Vector3 vector3, int decimalPlaces = 2)
        {
            float multiplier = 1;
            for (int i = 0; i < decimalPlaces; i++)
            {
                multiplier *= 10f;
            }

            return new Vector3(
                Mathf.Round(vector3.x * multiplier) / multiplier,
                Mathf.Round(vector3.y * multiplier) / multiplier,
                Mathf.Round(vector3.z * multiplier) / multiplier);
        }

        public static Vector2 Round(this Vector2 vector, int decimalPlaces = 2)
        {
            float multiplier = 1;
            for (int i = 0; i < decimalPlaces; i++)
            {
                multiplier *= 10f;
            }

            return new Vector2(
                Mathf.Round(vector.x * multiplier) / multiplier,
                Mathf.Round(vector.y * multiplier) / multiplier);
        }

        public static List<T> GetInterfaces<T>(this GameObject objectToSearch) where T : class
        {
            MonoBehaviour[] list = objectToSearch.GetComponents<MonoBehaviour>();
            List<T> resultList = new List<T>();
            foreach (MonoBehaviour mb in list)
            {
                if (mb is T)
                {
                    //found one
                    resultList.Add((T) ((System.Object) mb));
                }
            }

            return resultList;
        }

        public static Vector2 Rotate(this Vector2 vectorToRotate, float angle)
        {
            //return Quaternion.AngleAxis(angle, Vector3.up) * vectorToRotate;
            
            float theta = Mathf.Deg2Rad * angle;

            float cs = Mathf.Cos(theta);
            float sn = Mathf.Sin(theta);

            float rx = vectorToRotate.x * cs - vectorToRotate.y * sn;
            float ry = vectorToRotate.x * sn + vectorToRotate.y * cs;
            
            return new Vector2(rx, ry);
        }
    }
}