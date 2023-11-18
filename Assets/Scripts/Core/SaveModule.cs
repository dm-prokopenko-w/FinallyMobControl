using System;
using System.IO;
using UnityEngine;

namespace Core
{
    public class SaveModule
    {
        public T Load<T>(string uniqueKey)
        {
            if (!File.Exists(GetPath(uniqueKey)))
            {
                return default;
            }

            try
            {
                string json = File.ReadAllText(GetPath(uniqueKey));
                return JsonUtility.FromJson<T>(json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default;
            }
        }

        public void Save(string uniqueKey, object saveObj)
        {
            string json = JsonUtility.ToJson(saveObj);
            try
            {
                File.WriteAllText(GetPath(uniqueKey), json);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        public void Delete(string uniqueKey)
        {
            File.Delete(GetPath(uniqueKey));
        }

        private string GetPath(string uniqueKey) => Application.persistentDataPath + "/" + uniqueKey;
    }
}
