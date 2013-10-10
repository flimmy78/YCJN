using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NewOriental.Core.Helpers
{
    /// <summary>
    /// 序列化帮助类。
    /// </summary>
    public static class SerializationHelper
    {
        public static byte[] Serialize(object graph)
        {
            byte[] serializedData = null;
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, graph);
                serializedData = stream.ToArray();
            }
            return serializedData;
        }

        public static object Deserialize(byte[] serializedData)
        {
            object graph = null;
            if (serializedData != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    for (int i = 0; i < serializedData.Length; i++)
                    {
                        stream.WriteByte(serializedData[i]);
                    }
                    stream.Position = 0;
                    BinaryFormatter formatter = new BinaryFormatter();
                    graph = formatter.Deserialize(stream);
                }
            }
            return graph;
        }
    }
}
