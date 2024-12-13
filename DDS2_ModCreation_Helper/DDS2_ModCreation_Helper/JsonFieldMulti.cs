using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;

namespace DDS2_ModCreation_Helper
{
    internal class JsonFieldMulti
    {
        internal static void MultipliField()
        {
            // Step 1: Input from the user
            Console.Write("Enter the JSON file path: ");
            string jsonFilePath = Console.ReadLine()?.Trim('"');

            Console.Write("Enter the multiplication amount (e.g., 3): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal multiplier))
            {
                Console.WriteLine("Invalid multiplier entered.");
                return;
            }

            Console.Write("Enter the field to modify (e.g., InventoryMaxStack): ");
            string fieldToModify = Console.ReadLine();

            // Step 2: Read the JSON file
            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            string jsonData = File.ReadAllText(jsonFilePath);

            // Step 3: Parse the JSON dynamically
            JsonNode? root = JsonNode.Parse(jsonData);

            if (root == null || root is not JsonArray jsonArray)
            {
                Console.WriteLine("Invalid JSON structure. Expected an array of objects.");
                return;
            }

            // Step 4: Modify the specified field in each object
            foreach (JsonNode? item in jsonArray)
            {
                if (item is JsonObject jsonObject)
                {
                    if (jsonObject.ContainsKey(fieldToModify))
                    {
                        JsonNode? fieldValue = jsonObject[fieldToModify];

                        // Check if the field is a number
                        if (fieldValue is JsonValue value && value.TryGetValue(out decimal number))
                        {
                            decimal newValue = number * multiplier;
                            jsonObject[fieldToModify] = newValue;
                            Console.WriteLine($"Modified {fieldToModify} for item {jsonObject["Name"]}: New value = {newValue}");
                        }
                    }
                }
            }

            // Step 5: Save the modified JSON back to a new file
            string outputFilePath = Path.Combine(Path.GetDirectoryName(jsonFilePath), "modified_" + Path.GetFileName(jsonFilePath));
            File.WriteAllText(outputFilePath, jsonArray.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));

            Console.WriteLine($"Modified JSON saved to: {outputFilePath}");
        }
    }
}
