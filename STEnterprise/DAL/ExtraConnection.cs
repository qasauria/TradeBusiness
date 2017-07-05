using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace STEnterprise.DAL
{
    
        public class ExraconnectionConfig
        {
            public DataSet GetData(string query)
            {
                string conString = ConfigurationManager.ConnectionStrings["STEnterpriseDB"].ConnectionString;
                SqlCommand cmd = new SqlCommand(query);
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;

                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds, "DataTable1");
                            return ds;
                        }
                    }
                }
            }

    
    }
    }
