using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Sql;

namespace Rockweed
{

    public partial class PasswordManager : Form
    {
        public PasswordManager()
        {
            InitializeComponent();

            DbSavingBtn.Enabled = false; // Initially disable the save button
            UserInput.Enabled = false; // Initially disable the user input
            PasswordInput.Enabled = false; // Initially disable the password input
            InitializeMyComponents();
            InitializeDatabases();
        }
        public void InitializeMyComponents()
        {
            this.Text = "Password Manager";
            DbChangeBtn.Click += (object sender, EventArgs e) =>
            {
                if(DbSavingBtn.Enabled == false && PasswordInput.Enabled == false && UserInput.Enabled == false)
                {
                    DbSavingBtn.Enabled = true;
                    PasswordInput.Enabled = true;
                    UserInput.Enabled = true;
                }
                
            };

            DbSavingBtn.Click += (object sender, EventArgs e) =>
            {
                Properties.Settings.Default.DatabaseAccount = UserInput.Text;
                Properties.Settings.Default.DatabasePassword = PasswordInput.Text;
                Properties.Settings.Default.Save();
                UserInput.Enabled = false;
                PasswordInput.Enabled = false;
                DbSavingBtn.Enabled = false;
            };
        }

        public void InitializeDatabases()
        {
            // Initialize the database connection here
            string connectionString = $"Server=localhost;User={Properties.Settings.Default.DatabaseAccount};" +
                $"Password={Properties.Settings.Default.DatabasePassword};DATABASE=";

            // Use the connection string to connect to your database
        }

    }
}
