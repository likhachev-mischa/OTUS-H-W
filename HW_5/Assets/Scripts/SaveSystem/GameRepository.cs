using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem
{
    public sealed class GameRepository
    {
        private const string SAVE_FILE_PATH = "./SaveFile.txt";

        private Dictionary<string, string> gameState = new();

        private Aes aes;
        private byte[] aesKey;
        private byte[] aesIv;

        public bool TryGetData<T>(out T data)
        {
            string key = typeof(T).Name;
            if (gameState.TryGetValue(key, out string serializedData))
            {
                data = JsonConvert.DeserializeObject<T>(serializedData);
                return true;
            }

            data = default;
            return false;
        }

        public T GetData<T>()
        {
            string key = typeof(T).Name;
            string serializedData = gameState[key];
            var data = JsonConvert.DeserializeObject<T>(serializedData);
            return data;
        }

        public void SetData<T>(T data)
        {
            string key = typeof(T).Name;
            string serializedData = JsonConvert.SerializeObject(data, Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            Debug.Log(serializedData + "was serialized");
            gameState[key] = serializedData;
        }

        public void GetState()
        {
            var fileInfo = new FileInfo(SAVE_FILE_PATH);
            FileStream fileStream;

            if (!fileInfo.Exists)
            {
                fileStream = new FileStream(SAVE_FILE_PATH, FileMode.CreateNew);

                using (aes = Aes.Create())
                {
                    aesKey = aes.Key;
                    aesIv = aes.IV;
                }

                using (var binaryWriter = new BinaryWriter(fileStream))
                {
                    binaryWriter.Write(aesKey);
                    binaryWriter.Write(aesIv);
                }

                fileStream.Dispose();
                Debug.Log("File was not found! New was created");
                return;
            }

            fileStream = new FileStream(SAVE_FILE_PATH, FileMode.Open);

            using (var binaryReader = new BinaryReader(fileStream))
            {
                aesKey = binaryReader.ReadBytes(32);
                aesIv = binaryReader.ReadBytes(16);

                aes = Aes.Create();
                aes.Key = aesKey;
                aes.IV = aesIv;

                using (CryptoStream cryptoStream = new(fileStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader decryptReader = new(cryptoStream))
                    {
                        string json = decryptReader.ReadToEnd();
                        Debug.Log("STATE IS "+json);
                        gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    }
                }
            }

            Debug.Log("Loaded!");
            aes.Dispose();
            fileStream.Dispose();
        }

        public void SetState()
        {
            var fileInfo = new FileInfo(SAVE_FILE_PATH);
            FileStream fileStream;

            if (!fileInfo.Exists)
            {
                fileStream = new FileStream(SAVE_FILE_PATH, FileMode.CreateNew);

                using (aes = Aes.Create())
                {
                    aesKey = aes.Key;
                    aesIv = aes.IV;
                }

                using (var binaryWriter = new BinaryWriter(fileStream))
                {
                    binaryWriter.Write(aesKey);
                    binaryWriter.Write(aesIv);
                }

                fileStream.Dispose();
                Debug.Log("File was not found! New was created");
            }


            using (fileStream = new FileStream(SAVE_FILE_PATH, FileMode.Open))
            {
                using (var binaryReader = new BinaryReader(fileStream))
                {
                    aesKey = binaryReader.ReadBytes(32);
                    aesIv = binaryReader.ReadBytes(16);

                    aes = Aes.Create();
                    aes.Key = aesKey;
                    aes.IV = aesIv;
                }
            }

            fileStream = new FileStream(SAVE_FILE_PATH, FileMode.Truncate);

            using (var binaryWriter = new BinaryWriter(fileStream))
            {
                binaryWriter.Write(aesKey);
                binaryWriter.Write(aesIv);

                using (CryptoStream cryptoStream = new(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (StreamWriter encryptWriter = new(cryptoStream))
                    {
                        string json = JsonConvert.SerializeObject(gameState);
                        Debug.Log("STATE was "+json);
                        encryptWriter.Write(json);
                    }
                }
            }


            Debug.Log("Saved!");
            aes.Dispose();
            fileStream.Dispose();
        }
    }
}