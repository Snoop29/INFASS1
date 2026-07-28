namespace BOOTSTRAP.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string[] Fields { get; set; }
        public object[] Values { get; set; }

        public User(string fullName, string email, string password)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            Fields = new string[] { "FullName", "Email", "Password" };
            Values = new object[] { fullName, email, password };
        }

        // dynamic INSERT INTO query
        public string GenerateInsertQuery(string tableName)
        {
            string query = "INSERT INTO " + tableName + " (";
            for (int i = 0; i < Fields.Length; i++)
            {
                query += Fields[i];
                if (i < Fields.Length - 1) query += ", ";
            }
            query += ") VALUES (";
            for (int i = 0; i < Values.Length; i++)
            {
                if (Values[i] is string)
                {
                    query += "'" + Values[i] + "'";
                }
                else
                {
                    query += Values[i];
                }
                if (i < Values.Length - 1) query += ", ";
            }
            query += ")";
            return query;
        }

        // dynamic SELECT query — fetches a user by Id
        public string GenerateSelectQuery(string tableName, int id, string[]Fields)
        {
            string query = "SELECT ";
            for (int i = 0; i < Fields.Length; i++)
            {
                query += Fields[i];
                if (i < Fields.Length - 1) query += ", ";
            }
            query += " FROM " + tableName;
            query += " WHERE Id = " + Id;
            return query;
        }

        // dynamic UPDATE query — updates all fields for the user matching this Id
        public string GenerateUpdateQuery(string tableName, int id, string[]Fields)
        {
            string query = "UPDATE " + tableName + " SET ";
            for (int i = 0; i < Fields.Length; i++)
            {
                query += Fields[i] + " = ";
                if (Values[i] is string)
                {
                    query += "'" + Values[i] + "'";
                }
                else
                {
                    query += Values[i];
                }
                if (i < Fields.Length - 1) query += ", ";
            }
            query += " WHERE Id = " + Id;
            return query;
        }

        // dynamic DELETE query — deletes the user matching this Id
        public string GenerateDeleteQuery(string tableName)
        {
            string query = "DELETE FROM " + tableName + " WHERE Id = " + Id;
            return query;
        }
    }
}