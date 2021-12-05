using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLabs.DotnetHelpers.Helpers
{
    public static class EFMigrationHelper
    {
        public static string FieldFromTextToUuid(string tableName, string fieldName, bool isNullable)
        {
            string alterColumnSql = $"ALTER TABLE \"{tableName}\" ALTER COLUMN \"{fieldName}\"";
            string sql = $"{alterColumnSql} {(isNullable ? "DROP NOT NULL" : "SET NOT NULL")};"
                + $"\n {alterColumnSql} TYPE uuid USING \"{fieldName}\"::uuid;";
            Console.WriteLine($"{nameof(FieldFromTextToUuid)} sql:\n {sql}\n");
            return sql;
        }

        public static string FieldFromUuidToText(string tableName, string fieldName, bool isNullable)
        {
            string alterColumnSql = $"ALTER TABLE \"{tableName}\" ALTER COLUMN \"{fieldName}\"";
            string sql = $"{alterColumnSql} {(isNullable ? "DROP NOT NULL" : "SET NOT NULL")};"
                + $"\n {alterColumnSql} TYPE text;";
            Console.WriteLine($"{nameof(FieldFromUuidToText)} sql:\n {sql}\n");
            return sql;
        }
    }
}
