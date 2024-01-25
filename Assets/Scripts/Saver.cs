using System;
using System.IO;
using UnityEngine;

namespace TowerDefense
{
    [Serializable]
    public class Saver<T>
    {
        public static void TryLoad(string filename, ref T data)
        {
            var path = Path(filename);
            if (File.Exists(path))
            {
                var dataString = File.ReadAllText(path);
                var saver = JsonUtility.FromJson<Saver<T>>(dataString);
                data = saver.Data;
            }
        }
        public T Data;
        public static void Save(string filename, T data)
        {
            var wrapper = new Saver<T> { Data = data };
            var dataString = JsonUtility.ToJson(wrapper);
            File.WriteAllText(Path(filename), dataString);
        }

       
        private static string Path(string filename)
        {
            return $"{Application.persistentDataPath}/{filename}";
        }

        
    }
}