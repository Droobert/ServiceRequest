using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using Newtonsoft.Json;
using Android.Content.Res;

namespace ServiceRequest
{
    public class IOHelper
    {
        public void WriteToJsonFile<T>(Context con, T objectToWrite) where T : new()
        {
            StreamWriter writer = null;
            try
            {
                AssetManager assets = con.Assets;

                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(assets.Open("Json.txt"));
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// Reads an object instance from an Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the Json file.</returns>
        public T ReadFromJsonFile<T>(Context con) where T : new()
        {
            StreamReader reader = null;
            try
            {
                AssetManager assets = con.Assets;
                string content;
                using (reader = new StreamReader(assets.Open("Json.txt")))
                {
                    content = reader.ReadToEnd();
                }

                //reader = new StreamReader(filePath);
                //var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(content);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
