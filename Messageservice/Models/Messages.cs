using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Messageservice.Models
{
    public class Messages
    {
        //defining all the properties of the table with get and set
        public int Id { get; set; }
        public string mes_content { get; set; }      
        public DateTime delivered_dt { get; set; }
    }
    // Creating an object of Messages class 

    public class MessageModel :Messages
    {
        private SqlConnection _conn;
        private SqlDataAdapter Adapter;
        public void MessagesData(DataRow row)
        {
            Id = (int)row["Id"];
            mes_content = row["mes_content"].ToString();
            delivered_dt = Convert.ToDateTime(row["delivered_dt"]);
        }

        public List<Messages> GetAllData()
        {
            List<Messages> message = null;
            var query = "select * from Messages order by delivered_dt asc";
            message = GetDataFromDB(query);
            return message;
        }

        public List<Messages> GetDataById(int id)
        {
            List<Messages> message = null;
            var query = "select * from Messages where id="+ id;
            message = GetDataFromDB(query);          
            
            return message;
        }

        public List<Messages> CreateMessage(Messages msg)
        {
            SqlCommand cmd = null;
            List<Messages> message = null;
            try
            {
                _conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Radha\\Documents\\tempdb.mdf;Integrated Security=True;Connect Timeout=30");
                cmd = new SqlCommand("insert into Messages(mes_content,delivered_dt) values(@msg,@deliveredDt); SELECT SCOPE_IDENTITY()", _conn);
                _conn.Open();
                cmd.Parameters.AddWithValue("@msg", msg.mes_content);
                cmd.Parameters.AddWithValue("@deliveredDt", msg.delivered_dt);
                int insertedID = Convert.ToInt32(cmd.ExecuteScalar());

                message = GetDataById(insertedID);
            }
            catch(Exception ex)
            {
                message = null;
            }
            finally
            {
                _conn.Close();
                cmd = null;
            }
            

            return message;
        }

        public List<Messages> UpdateMessage(Messages msg)
        {
            SqlCommand cmd = null;
            List<Messages> message = null;

            // Coonecting local SQL Dtabase
            try
            {
                _conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Radha\\Documents\\tempdb.mdf;Integrated Security=True;Connect Timeout=30");
                cmd = new SqlCommand("update Messages set mes_content = @content,delivered_dt=@deliveredTime where id=@id", _conn);
                _conn.Open();
                cmd.Parameters.AddWithValue("@content", msg.mes_content);
                cmd.Parameters.AddWithValue("@deliveredTime", msg.delivered_dt);
                cmd.Parameters.AddWithValue("@id", msg.Id);

                cmd.ExecuteNonQuery();

                message = GetDataById(msg.Id);
            }
            catch (Exception ex)
            {
                message = null;
            }
            finally
            {
                _conn.Close();
                cmd = null;
            }

            return message;
        }

        public string DeleteMessage(int id)
        {
            SqlCommand cmd = null;
            //List<Messages> message = null;
            try
            {
                _conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Radha\\Documents\\tempdb.mdf;Integrated Security=True;Connect Timeout=30");
                cmd = new SqlCommand("delete Messages where id=@id", _conn);
                _conn.Open();

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                _conn.Close();
                cmd = null;
            }

            return "Delete successful";
        }

        public List<Messages> GetDataFromDB(string connString)
        {
            List<Messages> message = null;
            try
            {
                _conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Radha\\Documents\\tempdb.mdf;Integrated Security=True;Connect Timeout=30");
                DataTable _dt = new DataTable();

                Adapter = new SqlDataAdapter
                {
                    SelectCommand = new SqlCommand(connString, _conn)
                };
                Adapter.Fill(_dt);
                 message = new List<Messages>(_dt.Rows.Count);
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow row in _dt.Rows)
                    {
                        Messages mg = new Messages();
                        mg.Id = (int)row["Id"];
                        mg.mes_content = row["mes_content"].ToString();
                        mg.delivered_dt = Convert.ToDateTime(row["delivered_dt"]);
                        message.Add(mg);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                _conn.Close();
                Adapter = null;
            }
            return message;
        }


    }
}