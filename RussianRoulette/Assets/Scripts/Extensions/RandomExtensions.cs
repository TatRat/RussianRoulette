using System.Collections.Generic;
using UnityEngine;

namespace TatRat.Extensions
{
    public static class RandomExtensions
    {
        public static Vector3 GetRandom(this Vector3 point, Vector3 randomZone) => 
            point + new Vector3(GetRandomAxis(randomZone.x), GetRandomAxis(randomZone.y),GetRandomAxis(randomZone.z));
        
        public static Vector3 GetRandom(this Vector3 point, float radius) => 
            point + new Vector3(GetRandomAxis(radius), GetRandomAxis(radius),GetRandomAxis(radius));
        private static float GetRandomAxis(float distance) =>
            Random.Range(-distance, distance);
        
        public static T GetRandom<T>(this IList<T> list) => list.Count <= 0
            ? default
            : list[Random.Range(0, list.Count)];

        public static T GetRandom<T>(this T[] array) => array.Length <= 0
            ? default
            : array[Random.Range(0, array.Length)];
    }
}