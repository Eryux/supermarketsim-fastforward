// Copyright(c) 2024 - C.Nicolas <contact@bark.tf>
// github.com/eryux

using BepInEx.Configuration;
using System.Collections.Generic;
using System.Text;

namespace FastForward
{
    public class Setting
    {
        public ConfigEntry<KeyboardShortcut> Input_DecreaseGameSpeed { get; internal set; }

        public ConfigEntry<KeyboardShortcut> Input_IncreaseGameSpeed { get; internal set; }

        public ConfigEntry<KeyboardShortcut> Input_ResetGameSpeed { get; internal set; }


        public ConfigEntry<float[]> GameSpeedLoop { get; internal set; }


        public Setting(ConfigFile cfg)
        {
            TomlTypeConverter.AddConverter(typeof(float[]), new TypeConverter()
            {
                ConvertToObject = (str, type) =>
                {
                    List<float> list = new List<float>();
                    foreach (string x in str.Replace("[", "").Replace("]", "").Trim().Split(',')) {
                        list.Add(float.Parse(x, System.Globalization.CultureInfo.InvariantCulture));    
                    } return list.ToArray();
                },

                ConvertToString = (value, type) =>
                {
                    if (value != null && value.GetType() == typeof(float[]))
                    {
                        float[] arr = (float[])value;
                        StringBuilder str = new StringBuilder();
                        List<string> items = new List<string>();
                        for (int i = 0; i < arr.Length; ++i) { items.Add(arr[i].ToString("0.##", System.Globalization.CultureInfo.InvariantCulture)); }
                        str.Append("[ ");
                        str.Append(string.Join(",", items));
                        str.Append(" ]");
                        return str.ToString();
                    } return "[ ]";
                }
            });

            GameSpeedLoop = cfg.Bind("FastForward", "game_speed", new float[] { 0.5f, 1f, 1.5f, 2f, 3f, 5f, 10f  }, "Available speeds");

            Input_DecreaseGameSpeed = cfg.Bind("FastForward.Shortcut", "decrease_speed", KeyboardShortcut.Deserialize("F2"), "Decrease Game speed");
            Input_IncreaseGameSpeed = cfg.Bind("FastForward.Shortcut", "increase_speed", KeyboardShortcut.Deserialize("F4"), "Increase Game speed");
            Input_ResetGameSpeed = cfg.Bind("FastForward.Shortcut", "reset_speed", KeyboardShortcut.Deserialize("F3"), "Reset Game speed");
        }

    }
}
