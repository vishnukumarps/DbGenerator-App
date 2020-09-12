using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web;

namespace TestAppLibrary
{
    public  class HelperUtility
    {
        private const string connectionString = "Server=localhost\\SQLEXPRESS;Database=ZTestDb;Trusted_Connection=True";

        public bool Status { get; set; }
        public string ErrorMessage { get; set; }


        public bool CreateDb()
        {
            return false;
        }

        public List<string> GetAllTableNames()
        {
            List<string> tableNames = new List<string>();
            var Query = " SELECT  name FROM sys.Tables";
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(Query, connection);
                DataReader = sqlCommand.ExecuteReader();
                while(DataReader.Read())
                {
                    tableNames.Add(DataReader.GetValue(0).ToString());
                }
                connection.Close();
                return tableNames;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Dictionary<string,string> GetTableSchema(string tableName)
        {


            Dictionary<string, string> tableSchemea = new Dictionary<string, string>();

            string Query = "SELECT  COLUMN_NAME,DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'"+tableName+"'";
            try
            {
                try
                {
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(Query, connection);
                    DataReader = sqlCommand.ExecuteReader();
                    while (DataReader.Read())
                    {
                        tableSchemea.Add(DataReader.GetValue(0).ToString(),DataReader.GetValue(1).ToString());
                    }
                    connection.Close();
                    return tableSchemea;

                }
                catch (Exception ex)
                {

                    ErrorMessage = ex.Message;
                    Status = true;
                }
            }
            catch ( Exception ex)
            {

                throw;
            }


            return tableSchemea;
        }
     

        public  void InsertData(string TableName,List<string> Data)
        {
            string Query = "Insert  into "+TableName+" ";

            var columns = GetTableSchema(TableName);
            var firstchar = "(";
            var lastchar = ")";
            string columnNames = "";
            string _data = "";
            foreach (var item in columns)
            {
                columnNames +=item.Key+",";

         
               

            }

            foreach(var item in Data)
            {
                _data += "'"+item+"'" + ",";
            }
            
            var ColumnNames=columnNames.Remove(columnNames.Length-1);
           
            var Datas=_data.Remove(_data.Length-1);
           

            Query =Query+firstchar+ ColumnNames +lastchar+" values " +firstchar+ Datas+lastchar;
           
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(Query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(Query);
                throw;
            }


        }
        public bool Table( Dictionary<string,string> tableSechema,string tableName)
        {

            string Query = "CREATE TABLE "+tableName+" (";

            string columnNames = "";

            foreach(var item in tableSechema)
            {
                columnNames += ""+item.Key+""+" "+item.Value+" , ";
            }

            Query += columnNames + "date VARCHAR(64))";

            
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(Query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {

                throw;
            }

            return true;
        }

       
        public SqlDataReader DataReader { get; set; }

        public bool doDbChange(string Query)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(Query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return Status = true;
        }

        public List<string> getTable(string tableName)
        {
            List<string> TableData = new List<string>();

            string Query = "Select"+" "+"*" +" " +"from  "+" "+tableName;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(Query, connection);
                DataReader = sqlCommand.ExecuteReader();
                var count = DataReader.FieldCount;
                int i = 0;
                while (DataReader.Read())
                {
                    
                   for(int j=0;j<count;j++)
                    {

                        Console.Write(DataReader.GetValue(j).ToString());
                        TableData.Add(DataReader.GetValue(j).ToString());

                    }




                }
                return TableData;

            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
                Status = true;
            }
            return TableData;
        }

        public bool SendEmail(string toAddress, string subject, string message, string filePath)
        {
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("vishnukumarps2017@gmail.com");
                mail.To.Add(toAddress);
                mail.Subject = subject;
                mail.Body = message;

                if (filePath != null)
                {
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(filePath);
                    mail.Attachments.Add(attachment);
                }

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("vishnukumarps2017@gmail.com", "thriller1!");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return (Status = true);
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
                return (Status = false);
            }

        }

        //public bool SendSms(string toPhoneNumber, string msg)
        //{
        //    using (var web = new System.Net.WebClient())
        //    {
        //        try
        //        {
        //            string userName = "hostel123";
        //            string userPassword = "1234567";
        //            //string msgRecepient = "9656284838";
        //            //string msgText = "This is a demo msg please ignore!";

        //            string url2 = "http://198.24.149.4/API/pushsms.aspx?loginID=" + userName + "&password=" + userPassword + "&mobile=" + msgRecepient + "&text=" + msgText + "&senderid=CHPSMS&route_id=2&Unicode=0";
        //            string result = web.DownloadString(url2);

        //            if (result.Contains("OK"))
        //            {
        //                return Status = true;

        //            }
        //            else
        //            {
        //                return Status = false;
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //            ErrorMessage = ex.Message;

        //        }
        //        return Status = true;
        //    }



        //}
    }
}