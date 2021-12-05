using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLabs.DotnetHelpers.Helpers
{
    public static class EFMigrationHelper
    {
        public static string FieldFromTextToUuid(string tableName, string fieldName)
        {
            string sql = $"ALTER TABLE \"{tableName}\" ALTER COLUMN \"{fieldName}\" TYPE uuid USING \"{fieldName}\"::uuid;";
            Console.WriteLine($"{nameof(FieldFromTextToUuid)} sql:\n {sql}");
            return sql;
        }

        public static string FieldFromUuidToText(string tableName, string fieldName)
        {
            string sql = $"ALTER TABLE \"{tableName}\" ALTER COLUMN \"{fieldName}\" TYPE text;";
            Console.WriteLine($"{nameof(FieldFromUuidToText)} sql:\n {sql}");
            return sql;
        }
    }
}
