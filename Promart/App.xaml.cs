using Microsoft.EntityFrameworkCore;
using Promart.Database.Context;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Promart
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {   
        public static AppDbContext AppDbContext { get; set; }

        public App() : base()
        {
            var factory = new AppDbContextFactory();
            AppDbContext = factory.CreateDbContext(null);
        }
    }
}
