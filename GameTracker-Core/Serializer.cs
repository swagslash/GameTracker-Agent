using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace GameTracker_Core
{
    public static class Serializer
    {
        public static void Save(string filePath, object objToSerialize)
        {
            try
            {
                using (var stream = File.Open(filePath, FileMode.Create))
                {
                    var bin = new BinaryFormatter();
                    bin.Serialize(stream, objToSerialize);
                }
            }
            catch (IOException)
            {
                Console.WriteLine("path error");
            }
        }

        public static T Load<T>(string filePath) where T : class, new()
        {
            T rez = new T();

            try
            {
                using (var stream = File.Open(filePath, FileMode.Open))
                {
                    var bin = new BinaryFormatter();
                    rez = bin.Deserialize(stream) as T;
                }
            }
            catch (IOException)
            {
                return default(T);
            }

            return rez;
        }
    }
}
