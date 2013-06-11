using System;
using System.Windows.Forms;
using System.Text;
using System.Data;
using ConfigurationLibrary;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace ServerConfigurator
{
    //целый класс для получения мд5 хэша
    static class MD5HashGenerate
    {
        public static string GetMD5(string input)
        {
            MD5 md5 = MD5.Create();
            // пример сперт с мсдна - костыль на костыле ИМХО. Что нельзя было сделать нормальный МД5 хотябы?
            byte[] tmp = md5.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            // Это тормозной велосипед для конвертирования значений байт в hex строку. Для отображения в консоли, например. © kolorotur 
            for (int i = 0; i < tmp.Length; i++)
            {
                sBuilder.Append(tmp[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }

    /// <summary>
    ///  класс контейнер данных о пользователе
    /// </summary>
    class User
    {
        public string id;
        public string Name;
        public string Invite;
        public string Friends;
        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// класс для работы с базой данных
    /// </summary>
    class DBConnect
    {
        MySqlConnection Connection;
        MySqlDataAdapter _da;
        MySqlCommandBuilder _cb;
        DataSet _ds;

        /// <summary>
        /// выполняет Insert/Update запрос
        /// </summary>
        /// <param name="query">запрос</param>
        public void ExecuteQuery(string query)
        {
            Connection = new MySqlConnection("Database=" + Program.cfg.DB + ";Data Source=" + Program.cfg.DBhost + ";User Id=" + Program.cfg.DBuser + ";Password=" + Program.cfg.DBpassword + ";");
            try
            {
                if (query == null) throw new ArgumentNullException();
                MySqlCommand Command = new MySqlCommand(query, Connection);
                Connection.Open(); //Устанавливаем соединение с базой данных.
                Command.ExecuteNonQuery();
                Connection.Close(); //Обязательно закрываем соединение!
            }
            catch (Exception ex)
            {
                if (Connection.State == System.Data.ConnectionState.Open) Connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// получает таблицу пользователей
        /// </summary>
        /// <returns></returns>
        public DataTable LoadUsersTable()
        {
            Connection = new MySqlConnection("Database=" + Program.cfg.DB + ";Data Source=" + Program.cfg.DBhost + ";User Id=" + Program.cfg.DBuser + ";Password=" + Program.cfg.DBpassword + ";");
            try
            {
                string query = "Select UserID,UserName,Invite,Friends from Users";
                //string query = "Select * from Users";
                _ds = new DataSet();
                Connection.Open();
                _da = new MySqlDataAdapter(query, Connection);
                _cb = new MySqlCommandBuilder(_da);
                _da.Fill(_ds);
                return _ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { Connection.Close(); }
            return null;
        }

        /// <summary>
        /// добавляет пользователя в систему
        /// </summary>
        /// <param name="Username">Имя пользователя</param>
        /// <param name="password">пароль</param>
        /// <param name="secret">секретный код</param>
        /// <returns>результат операции</returns>
        public string AddUser(string Username, string password, string secret)
        {
            try
            {
                string query = "Insert into Users (UserName, Password, Invite) values('" + Username + "','" + password + "','" + secret + "')";
                MySqlCommand Command = new MySqlCommand(query, Connection);
                Connection.Open(); 
                Command.ExecuteNonQuery();

                query = "CREATE TABLE `" + Username + @"` (
                `Latitude`  decimal(10,7) NULL DEFAULT NULL ,
                `Longitude`  decimal(10,7) NULL DEFAULT NULL ,
                `Speed`  decimal(10,7) NULL DEFAULT NULL ,
                `Time`  datetime NULL DEFAULT NULL 
                )
                ENGINE=InnoDB
                DEFAULT CHARACTER SET=utf8 COLLATE=utf8_bin
                ROW_FORMAT=FIXED
                ;go;";

                Command = new MySqlCommand(query, Connection);
                Command.ExecuteNonQuery();
                Connection.Close(); 
            }
            catch (Exception ex) { return ex.Message; }
            return "Пользователь создан";
        }

        /// <summary>
        /// изменяет пароль пользователя
        /// </summary>
        /// <param name="UserID">идентификатор пользователя</param>
        /// <param name="password">новый пароль</param>
        public void ChangePassword(string UserID, string password)
        {
            string query = "update Users set Password='" + password + "' where UserID = '" + UserID + "'";
            this.ExecuteQuery(query);
        }

        public string CreateInvite(string UserName)
        {
            string invite;
            do
            {
                invite = MD5HashGenerate.GetMD5(UserName + DateTime.Now.ToString());
            }
            while (this.GetUserIDbySecret(invite) != null);
            return invite;
        }

        /// <summary>
        /// предоставляет пользователю возможность просматривать маршруты других пользователей
        /// </summary>
        /// <param name="uname">пользователь, которому предоставляется доступ</param>
        /// <param name="friendsList">пользователи к которым предоставляется доступ</param>
        public void AddFriends(string uname, CheckedListBox friendsList)
        {
            string friends = "";
            foreach (User u in friendsList.Items)
            {
                friends += u.Name + ";";
            }
            string query = "update Users set Friends='" + friends + "' where Username = '" + uname + "'";
            this.ExecuteQuery(query);
        }

        /// <summary>
        /// получает данные пользователя из бд
        /// </summary>
        /// <param name="name">имя пользователя</param>
        /// <returns>данные о пользователе</returns>
        public User GetUser(string name)
        {
            if (string.IsNullOrEmpty(name)) { return null; }
            User u = new User();
            try
            {
                string query = "select UserId, UserName, Invite, Friends from Users where UserName='" + name + "'";
                Connection = new MySqlConnection("Database=" + Program.cfg.DB + ";Data Source=" + Program.cfg.DBhost + ";User Id=" + Program.cfg.DBuser + ";Password=" + Program.cfg.DBpassword + ";");
                Connection.Open();
                MySqlCommand Command = new MySqlCommand(query, Connection);
                MySqlDataReader rd = Command.ExecuteReader();
                rd.Read();
                u.id = rd.GetString(0);
                u.Name = rd.GetString(1);
                u.Invite = rd.GetValue(2).ToString();
                u.Friends = rd.GetValue(3).ToString();
                return u;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { Connection.Close(); }
            return null;
        }

        /// <summary>
        /// возвращает имя пользователя по секретному коду
        /// </summary>
        /// <param name="secret">секретный код</param>
        /// <returns>имя пользователя</returns>
        public string GetUserIDbySecret(string secret)
        {
            try
            {
                string result = null;
                string query = "Select UserName from Users where Invite='" + secret + "'";
                Connection = new MySqlConnection("Database=" + Program.cfg.DB + ";Data Source=" + Program.cfg.DBhost + ";User Id=" + Program.cfg.DBuser + ";Password=" + Program.cfg.DBpassword + ";");
                Connection.Open();
                MySqlCommand Command = new MySqlCommand(query, Connection);
                MySqlDataReader rd = Command.ExecuteReader();
                if (rd.Read())
                     result= rd.GetValue(0).ToString();
                if (string.IsNullOrEmpty(result)) return null;
                else return result;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { Connection.Close(); }
            return null;
        }

        /// <summary>
        /// применяет изменения
        /// </summary>
        public void Apply()
        {
            this._da.Update(this._ds);
        }
    }
}
