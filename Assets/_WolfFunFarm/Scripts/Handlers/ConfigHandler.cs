using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

namespace WolfFunFarm
{
    public class ConfigHandler
    {
        private static readonly string CONFIG_PATH = "https://docs.google.com/spreadsheets/d/e/2PACX-1vSXfKdKwbZWLD014ICwsxPke5esaUYo0sd8KJ6Ax7nSpswxeDJvj6Hqc0trqP51PRTrf6wWw8kNuwTo/pub?output=csv";

        private static WorkerConfig _workerConfig;
        private static MachineConfig _machineConfig;
        private static Dictionary<string, FarmEntityConfig> _entityConfigs;

        private static IEnumerator DownloadCSV(string url, Action onDownloadSuccess = null, Action onDowloadFailed = null)
        {
            // Create a UnityWebRequest to download the file
            UnityWebRequest request = UnityWebRequest.Get(url);

            // Send the request and wait for it to complete
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Todo: Read and parse CSV data
                string[] chunks = request.downloadHandler.text.Split('\n');
                for (int i = 1; i < 5; i++)
                {
                    string id = "";
                    string name = "";
                    int seedPrice = 0;
                    int sellPrice = 0;
                    int bundleSize = 0;
                    int cycleDuration = 0;
                    int productPerCycle = 0;
                    int lifetimeProducts = 0;

                    string[] data = chunks[i].Split(',');
                    for (int j = 0; j < data.Length; j++)
                    {
                        switch (j)
                        {
                            case 0:
                                {
                                    id = data[j];
                                    name = data[j];
                                }
                                break;
                            case 1:
                                {
                                    int.TryParse(data[j], out seedPrice);
                                }
                                break;
                            case 2:
                                {
                                    int.TryParse(data[j], out sellPrice);
                                }
                                break;
                            case 3:
                                {
                                    int.TryParse(data[j], out bundleSize);
                                }
                                break;
                            case 4:
                                {
                                    int.TryParse(data[j], out cycleDuration);
                                }
                                break;
                            case 5:
                                {
                                    int.TryParse(data[j], out productPerCycle);
                                }
                                break;
                            case 6:
                                {
                                    int.TryParse(data[j], out lifetimeProducts);
                                }
                                break;
                        }
                    }

                    _entityConfigs.Add(id, new FarmEntityConfig()
                    {
                        Id = id,
                        Name = name,
                        SeedPrice = seedPrice,
                        SellPrice = sellPrice,
                        BundleSize = bundleSize,
                        CycleDuration = cycleDuration,
                        ProductPerCycle = productPerCycle,
                        LifetimeProducts = lifetimeProducts
                    });
                }

                // Worker Config
                {
                    var data = chunks[6].Split(',');
                    _workerConfig = new WorkerConfig()
                    {
                        HireCost = int.Parse(data[1]),
                        WorkDuration = int.Parse(data[2]),
                    };
                }

                // Machine Config
                {
                    var data = chunks[8].Split(',');
                    _machineConfig = new MachineConfig()
                    {
                        UpgradeCost = int.Parse(data[1]),
                        Boost = int.Parse(data[2]),
                    };
                }

                Debug.Log($"Download CSV successfully!");
                onDownloadSuccess?.Invoke();
            }
            else
            {
                Debug.LogError($"Error downloading CSV: {request.error}");
                onDowloadFailed?.Invoke();
            }
        }

        public static void InitializeConfigs(Action onDownloadSuccess = null, Action onDowloadFailed = null)
        {
            _entityConfigs = new Dictionary<string, FarmEntityConfig>();
            GameManager.Instance.StartCoroutine(DownloadCSV(CONFIG_PATH));
        }

        public static FarmEntityConfig GetEntityConfig(string id)
        {
            if (_entityConfigs.TryGetValue(id, out var config))
            {
                return config;
            }
            return null;
        }
        public static WorkerConfig GetWorkerConfig()
        {
            return _workerConfig;
        }
        public static MachineConfig GetMachineConfig()
        {
            return _machineConfig;
        }
    }
}