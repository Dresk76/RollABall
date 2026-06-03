using RollABall.Domain.Models;
using System.IO;
using UnityEngine;

namespace RollABall.Infrastructure.Save
{
    public static class SaveSystem
    {
        private static readonly string SavePath =
            Path.Combine(Application.persistentDataPath, "player_progress.json");

        public static void Save(PlayerProgress progress)
        {
            string json = JsonUtility.ToJson(progress, prettyPrint: true);
            File.WriteAllText(SavePath, json);
            Debug.Log($"Progreso guardado en: {SavePath}");
        }

        public static PlayerProgress Load()
        {
            if (!File.Exists(SavePath))
            {
                Debug.Log("No existe archivo de guardado, creando progreso nuevo");
                return new PlayerProgress();
            }

            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<PlayerProgress>(json);
        }

        public static bool HasSaveFile()
        {
            return File.Exists(SavePath);
        }

        public static void Delete()
        {
            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
                Debug.Log("Archivo de guardado eliminado");
            }
        }
    }
}