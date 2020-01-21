using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace GameTracker_Core
{
    public static class Serializer
    {
        private static readonly JsonSerializerSettings settings;
        
        static Serializer(){
            settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                Formatting = Formatting.Indented
            };
        }
        
    

        public static bool Save<T>(string filePath, T objToSerialize)
        {
            try
            {
                var jsonString = SerializeJson(objToSerialize);
                if (string.IsNullOrEmpty(jsonString)) return false;

                File.WriteAllText(filePath, jsonString);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static T Load<T>(string filePath) where T : class, new()
        {
            try
            {
                var jsonString = File.ReadAllText(filePath);
                return DeserializeJson<T>(jsonString);
            }
            catch (IOException)
            {
                return new T();
            }
        }

        public static string SerializeJson<T>(T objToSerialize)
        {
            try
            {
                return JsonConvert.SerializeObject(objToSerialize,settings);
            }
            catch
            {
                return null;
            }
            
        }

        public static T DeserializeJson<T>(string json) where T: class
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json,settings);
            }
            catch (InvalidCastException)
            {
                return null;
            }

        }
    }
}
