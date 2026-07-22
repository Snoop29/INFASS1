namespace BOOTSTRAP.Models
{
    public class User
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Parameterless constructor (needed for model binding, e.g. Login/Register)
        public User() { }

        // Parameterized constructor
        public User(string fullName, string email, string password)
        {
            FullName = fullName;
            Email = email;
            Password = password;
        }

        // Loops through an array of users and builds a display string
        public static string DisplayAll(User[] users)
        {
            string result = "";

            for (int i = 0; i < users.Length; i++)
            {
                result += "User " + (i + 1) + ": " + users[i].FullName + " - " + users[i].Email + "\n";
            }

            return result;
        }

        // Builds a dynamic INSERT INTO query string from a table name,
        // an array of field/column names, and an array of values.
        // Strings get wrapped in single quotes; numbers are left as-is.
        public string GenerateInsertQuery(string tableName, string[] fields, object[] values)
        {
            string query = "INSERT INTO " + tableName + " (";

            for (int i = 0; i < fields.Length; i++)
            {
                query += fields[i];
                if (i < fields.Length - 1)
                {
                    query += ", ";
                }
            }

            query += ") VALUES (";

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] is string)
                {
                    query += "'" + values[i] + "'";
                }
                else
                {
                    query += values[i];
                }

                if (i < values.Length - 1)
                {
                    query += ", ";
                }
            }

            query += ")";

            return query;
        }
    }
}