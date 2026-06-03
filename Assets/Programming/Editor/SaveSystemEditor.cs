using RollABall.Domain.Models;
using RollABall.Infrastructure.Save;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RollABall.Editor
{
    public class SaveSystemEditor : EditorWindow
    {
        private PlayerProgress _currentProgress;
        private string _statusMessage = "";

        [MenuItem("RollABall/Save System Manager")]
        public static void ShowWindow()
        {
            GetWindow<SaveSystemEditor>("Save System Manager");
        }

        private void OnGUI()
        {
            GUILayout.Label("SAVE SYSTEM MANAGER", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // ─── Estado del archivo ───────────────────────────────────
            GUILayout.Label("Estado del archivo", EditorStyles.boldLabel);

            bool hasSave = SaveSystem.HasSaveFile();
            EditorGUILayout.LabelField("Archivo de guardado:", hasSave ? "✅ Existe" : "❌ No existe");

            EditorGUILayout.Space();

            // ─── Acciones ─────────────────────────────────────────────
            GUILayout.Label("Acciones", EditorStyles.boldLabel);

            if (GUILayout.Button("Cargar datos actuales"))
            {
                _currentProgress = SaveSystem.Load();
                _statusMessage   = "Datos cargados correctamente";
            }

            if (GUILayout.Button("Borrar save (resetear juego)"))
            {
                if (EditorUtility.DisplayDialog(
                    "Borrar Save",
                    "¿Estás seguro? Se perderá todo el progreso.",
                    "Borrar",
                    "Cancelar"))
                {
                    SaveSystem.Delete();
                    _currentProgress = null;
                    _statusMessage   = "Save borrado correctamente";
                }
            }

            EditorGUILayout.Space();

            // ─── Ver y modificar datos ────────────────────────────────
            if (_currentProgress != null)
            {
                GUILayout.Label("Datos actuales", EditorStyles.boldLabel);

                _currentProgress.HasPlayedBefore = EditorGUILayout.Toggle(
                    "Ha jugado antes:", _currentProgress.HasPlayedBefore);

                _currentProgress.IntroductionCompleted = EditorGUILayout.Toggle(
                    "Introduction completada:", _currentProgress.IntroductionCompleted);

                _currentProgress.LastCompletedLevel = EditorGUILayout.IntField(
                    "Último nivel completado:", _currentProgress.LastCompletedLevel);

                _currentProgress.NextLevelToPlay = EditorGUILayout.IntField(
                    "Siguiente nivel a jugar:", _currentProgress.NextLevelToPlay);

                _currentProgress.TotalScore = EditorGUILayout.IntField(
                    "Score total:", _currentProgress.TotalScore);

                _currentProgress.MusicVolume = EditorGUILayout.IntSlider(
                    "Volumen música:", _currentProgress.MusicVolume, 0, 5);

                _currentProgress.SfxVolume = EditorGUILayout.IntSlider(
                    "Volumen SFX:", _currentProgress.SfxVolume, 0, 5);

                // ─── Niveles desbloqueados ────────────────────────────
                EditorGUILayout.Space();
                GUILayout.Label("Niveles desbloqueados", EditorStyles.boldLabel);

                if (_currentProgress.UnlockedLevels == null)
                    _currentProgress.UnlockedLevels = new List<int>();

                string[] levelNames = { "Introduction (0)", "Level 1 (1)", "Level 2 (2)", "Level 3 (3)" };

                foreach (var name in levelNames)
                {
                    int index     = System.Array.IndexOf(levelNames, name);
                    bool unlocked = _currentProgress.UnlockedLevels.Contains(index);
                    EditorGUILayout.LabelField($"  {name}", unlocked ? "✅ Desbloqueado" : "🔒 Bloqueado");
                }

                EditorGUILayout.Space();

                if (GUILayout.Button("Guardar cambios"))
                {
                    SaveSystem.Save(_currentProgress);
                    _statusMessage = "Cambios guardados correctamente";
                }

                EditorGUILayout.Space();

                // ─── Atajos rápidos ───────────────────────────────────
                GUILayout.Label("Atajos rápidos", EditorStyles.boldLabel);

                if (GUILayout.Button("Simular primera vez (resetear todo)"))
                {
                    SetProgress(
                        hasPlayed: false,
                        introCompleted: false,
                        lastLevel: -1,
                        nextLevel: 0,
                        unlockedLevels: new List<int>()
                    );
                    _statusMessage = "Reseteado → primera vez";
                }

                if (GUILayout.Button("Simular Introduction completada"))
                {
                    SetProgress(
                        hasPlayed: true,
                        introCompleted: true,
                        lastLevel: 0,
                        nextLevel: 1,
                        unlockedLevels: new List<int> { 0, 1 }
                    );
                    _statusMessage = "Introduction completada → Level 1 desbloqueado";
                }

                if (GUILayout.Button("Simular Level 1 completado"))
                {
                    SetProgress(
                        hasPlayed: true,
                        introCompleted: true,
                        lastLevel: 1,
                        nextLevel: 2,
                        unlockedLevels: new List<int> { 0, 1, 2 }
                    );
                    _statusMessage = "Level 1 completado → Level 2 desbloqueado";
                }

                if (GUILayout.Button("Simular Level 2 completado"))
                {
                    SetProgress(
                        hasPlayed: true,
                        introCompleted: true,
                        lastLevel: 2,
                        nextLevel: 3,
                        unlockedLevels: new List<int> { 0, 1, 2, 3 }
                    );
                    _statusMessage = "Level 2 completado → Level 3 desbloqueado";
                }

                if (GUILayout.Button("Simular Level 3 completado (todos desbloqueados)"))
                {
                    SetProgress(
                        hasPlayed: true,
                        introCompleted: true,
                        lastLevel: 3,
                        nextLevel: 3,
                        unlockedLevels: new List<int> { 0, 1, 2, 3 }
                    );
                    _statusMessage = "Level 3 completado → todos los niveles desbloqueados";
                }

                if (GUILayout.Button("Desbloquear todos los niveles"))
                {
                    SetProgress(
                        hasPlayed: true,
                        introCompleted: true,
                        lastLevel: 3,
                        nextLevel: 3,
                        unlockedLevels: new List<int> { 0, 1, 2, 3 }
                    );
                    _statusMessage = "Todos los niveles desbloqueados";
                }

                // ─── Navegación rápida ────────────────────────────────
                EditorGUILayout.Space();
                GUILayout.Label("Navegación rápida", EditorStyles.boldLabel);

                if (GUILayout.Button("Recargar MainMenu"))
                {
                    if (Application.isPlaying)
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene("02_main_menu");
                        _statusMessage = "Recargando MainMenu...";
                    }
                    else
                    {
                        _statusMessage = "Solo funciona con el juego corriendo";
                    }
                }
            }

            // ─── Mensaje de estado ────────────────────────────────────
            if (!string.IsNullOrEmpty(_statusMessage))
            {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox(_statusMessage, MessageType.Info);
            }
        }

        // ─── Helper privado ───────────────────────────────────────────
        private void SetProgress(bool hasPlayed, bool introCompleted, int lastLevel, int nextLevel, List<int> unlockedLevels)
        {
            _currentProgress.HasPlayedBefore       = hasPlayed;
            _currentProgress.IntroductionCompleted = introCompleted;
            _currentProgress.LastCompletedLevel    = lastLevel;
            _currentProgress.NextLevelToPlay       = nextLevel;
            _currentProgress.UnlockedLevels        = unlockedLevels;
            SaveSystem.Save(_currentProgress);
        }
    }
}