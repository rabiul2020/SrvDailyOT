using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SrvDailyOT
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        string connectionString = "Data Source=192.168.0.181;Initial Catalog=EmailCube;User ID=sa;Password=dbsrv170@sdk;";
        public Service1()
        {
            InitializeComponent();
            timer = new Timer();
        }

        protected override void OnStart(string[] args)
        {          
            timer.Interval = 86400000;
            timer.Elapsed += Timer_Elapsed;          
            timer.Start();
        }

        protected override void OnStop()
        {
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {           
            if (DateTime.Now.Hour == 22)
            {
            Console.WriteLine("Timer Elapsed event triggered.");
            ExecuteStoredProcedure();
            }
        }

        private void ExecuteStoredProcedure()
        {
            // Implement code to execute your stored procedure
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("usp_DailyOTScheduleReportingWise", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // Add any parameters if needed
                    // command.Parameters.AddWithValue("@ParameterName", parameterValue);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
