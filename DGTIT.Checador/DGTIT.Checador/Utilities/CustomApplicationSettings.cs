using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGTIT.Checador.Utilities {
    internal class CustomApplicationSettings {

        public static string GetGeneralDirections() {
            string registryPath = @"SOFTWARE\DGTIT\Checador";
            string valueName = "generalDirection";
            string settingValue = String.Empty;

            // Try reading from 64-bit registry first
            settingValue = GetRegistryValue(RegistryHive.LocalMachine, registryPath, valueName, RegistryView.Registry64);
            if(!string.IsNullOrEmpty(settingValue))
            {
                return settingValue;
            }

            // If not found, try reading from 32-bit registry
            settingValue = GetRegistryValue(RegistryHive.LocalMachine, registryPath, valueName, RegistryView.Registry32);
            return settingValue;
        }

        public static string GetStoragePath()
        {
            string registryPath = @"SOFTWARE\DGTIT\Checador";
            string valueName = "storagePath";
            string settingValue = String.Empty;

            // Try reading from 64-bit registry first
            settingValue = GetRegistryValue(RegistryHive.LocalMachine, registryPath, valueName, RegistryView.Registry64);
            if (!string.IsNullOrEmpty(settingValue))
            {
                return settingValue;
            }

            // If not found, try reading from 32-bit registry
            settingValue = GetRegistryValue(RegistryHive.LocalMachine, registryPath, valueName, RegistryView.Registry32);
            return settingValue;
        }


        static string GetRegistryValue(RegistryHive hive, string subKey, string valueName, RegistryView view) {
            using (RegistryKey baseKey = RegistryKey.OpenBaseKey(hive, view))
            using (RegistryKey key = baseKey.OpenSubKey(subKey)) {
                if (key != null) {
                    object value = key.GetValue(valueName);
                    return value?.ToString();
                }
            }
            return null;
        }
    }
}
