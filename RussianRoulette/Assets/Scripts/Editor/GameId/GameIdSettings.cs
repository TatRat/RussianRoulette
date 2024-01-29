using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using DotnetRandom = System.Random;
using UDebug = UnityEngine.Debug;

namespace TatRat.Editor.GameId
{
    [SavedScriptableSingletonData(
        savePath: "ProjectSettings/GameIdSettings.asset",
        settingsPath: "Project/Game Id Settings")]
    public sealed class GameIdSettings : SavedScriptableSingleton<GameIdSettings>,
        ISerializationCallbackReceiver
    {
        [SerializeField]
        [HideInInspector]
        private List<GameIdEntry> _gameIds = new();

        private readonly Dictionary<int, GameIdEntry> _gameIdMap = new();

        public string[] IdNames =>
            _gameIdMap.Values.Select(x => x.Name).ToArray();

        public (int, string)[] IdTuples
            => _gameIds.Select(entry => (entry.Value, entry.Name)).ToArray();

        public string GetIdName(int value)
            => _gameIdMap.TryGetValue(value, out var result) ? result.Name : string.Empty;

        public int GetIdByIndex(int index)
            => index >= 0 && index < _gameIds.Count ? _gameIds[index].Value : 0;

        public int GetIndexById(int value)
            => _gameIds.FindIndex(id => id.Value == value);

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            _gameIdMap.Clear();
            foreach (var gameId in _gameIds)
            {
                _gameIdMap[gameId.Value] = gameId;
            }
        }

        public int CreateNewId(string inputName = null)
        {
            var random = new DotnetRandom();
            int newId;
            do
            {
                newId = random.Next();
            } while (_gameIdMap.ContainsKey(newId));

            var newName = !string.IsNullOrWhiteSpace(inputName)
                ? inputName
                : $"New Game Id #{_gameIds.Count}";

            var newEntry = new GameIdEntry(newId, newName);
            _gameIds.Add(newEntry);
            _gameIdMap[newId] = newEntry;
            Save();

            return newId;
        }

        public int[] CreateNewIds(IList<string> names)
        {
            var random = new DotnetRandom();
            var count = names.Count;
            var result = new int[count];

            for (var i = 0; i < count; ++i)
            {
                int newId;
                do
                {
                    newId = random.Next();
                } while (_gameIdMap.ContainsKey(newId));
                var newEntry = new GameIdEntry(newId, names[i]);
                _gameIds.Add(newEntry);
                _gameIdMap[newId] = newEntry;
                result[i] = newId;
            }

            Save();
            
            return result;
        }

        public void RemoveElements(int[] elementIndex)
        {
            foreach (var index in elementIndex)
            {
                var removedId = _gameIds[index].Value;
                _gameIds.RemoveAt(index);
                _gameIdMap.Remove(removedId);
            }

            Save();
        }

        [SettingsProvider]
        private static SettingsProvider GetProvider()
            => GetSingletonSettingsProvider();

        [ContextMenu(nameof(CheckForDuplicates))]
        private void CheckForDuplicates()
        {
            var duplicateMap = new Dictionary<int, List<int>>();

            for (var i = 0; i < _gameIds.Count; ++i)
            {
                var gameIdEntry = _gameIds[i];
                var id = gameIdEntry.Value;

                if (!duplicateMap.TryGetValue(id, out var duplicateList))
                {
                    duplicateMap[id] = duplicateList = new List<int>();
                }

                duplicateList.Add(i);
            }

            var doubles = duplicateMap.Where(pair => pair.Value.Count > 1)
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            if (!doubles.Any())
            {
                Debug.Log($"Game Id Settings doesn't have any duplicates");
            }
            else
            {
                foreach ((var id, var doubleIndices) in doubles)
                {
                    var messageBuilder = new StringBuilder();
                    messageBuilder.AppendLine($"Duplicates for {id}:");
                    foreach (var index in doubleIndices)
                    {
                        messageBuilder.AppendLine($"#{index} = {_gameIds[index].Name}");
                    }

                    Debug.LogWarning(messageBuilder);
                }
            }
        }
    }
}