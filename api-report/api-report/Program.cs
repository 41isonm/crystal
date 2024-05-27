using System;
using System.Net;
using System.Text;
using CrystalDecisions.Shared;
using System.Linq;
using CrystalDecisions.CrystalReports.Engine;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.IO;
using api_report.Repository;
using api_report.Controller;

namespace CrystalReportApi
{
  
        class Program
        {
            static void Main(string[] args)
        {
            ReportController reportController = new ReportController();
            reportController.getReportPDF();
        }
        }
    } 
    

