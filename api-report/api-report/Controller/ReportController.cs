using System;
using System.Linq;
using System.Net;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using api_report.Repository;

namespace api_report.Controller
{
    public class ReportController
    {
        public void getReportPDF()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:5000/");
            listener.Start();
            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                response.AddHeader("Access-Control-Allow-Origin", "*");
                response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                response.AddHeader("Access-Control-Allow-Headers", "Content-Type");
                if (request.HttpMethod == "OPTIONS")
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Close();
                    continue;
                }

                if (request.HttpMethod == "GET" && request.Url.AbsolutePath == "/report")
                {
                    try
                    {
                        var queryString = request.Url.Query;
                        var parameters = queryString.TrimStart('?').Split('&')
                            .Select(x => x.Split('='))
                            .ToDictionary(x => Uri.UnescapeDataString(x[0]), x => Uri.UnescapeDataString(x[1]));

                        int codigoEmpresa = int.Parse(parameters["codigoEmpresa"]);
                        int codigoPedido = int.Parse(parameters["codigoPedido"]);
                        var reportRepository = new ReportRepository();

                        var reportData = reportRepository.GetReportData(codigoEmpresa, codigoPedido);
                        string reportPath = "C:\\cristal\\VEN000000002.rpt";

                        using (var reportDocument = new ReportDocument())
                        {
                            reportDocument.Load(reportPath);

                            foreach (ParameterFieldDefinition parameterField in reportDocument.DataDefinition.ParameterFields)
                            {
                                Console.WriteLine($"Parâmetro: {parameterField.Name}");
                                Console.WriteLine($"Tipo de dado: {parameterField.ParameterValueKind}");

                                // Verificar se o parâmetro está sendo usado no relatório
                                if (parameterField.CurrentValues.Count > 0)
                                {
                                    Console.WriteLine("Parâmetro utilizado no relatório:");
                                    foreach (var currentValue in parameterField.CurrentValues)
                                    {
                                        Console.WriteLine($"- Valor: {currentValue}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Parâmetro não utilizado no relatório.");
                                }
                            }

                            // Verificar parâmetros faltantes
                            var missingParameters = reportDocument.DataDefinition.ParameterFields
                                .Cast<ParameterFieldDefinition>()
                                .Where(pf => pf.CurrentValues.Count == 0)
                                .Select(pf => pf.Name)
                                .ToList();

                            if (missingParameters.Any())
                            {
                                Console.WriteLine("Parâmetros não preenchidos:");
                                foreach (var param in missingParameters)
                                {
                                    Console.WriteLine($"- {param}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Todos os parâmetros estão preenchidos.");
                            }

                            reportDocument.SetDataSource(reportData);

                            // Exportar para PDF
                            var exportOptions = new ExportOptions
                            {
                                ExportFormatType = ExportFormatType.PortableDocFormat,
                                ExportDestinationType = ExportDestinationType.DiskFile,
                                ExportDestinationOptions = new DiskFileDestinationOptions
                                {
                                    DiskFileName = "output.pdf"
                                }
                            };

                            try
                            {
                                using (var stream = reportDocument.ExportToStream(ExportFormatType.PortableDocFormat))
                                {
                                    using (var memoryStream = new MemoryStream())
                                    {
                                        stream.CopyTo(memoryStream);
                                        var pdfBytes = memoryStream.ToArray();

                                        var base64String = Convert.ToBase64String(pdfBytes);

                                        response.ContentLength64 = pdfBytes.Length;
                                        response.ContentType = "application/pdf";
                                        response.AddHeader("Content-Disposition", "attachment; filename=PedidoVenda.pdf");

                                        response.OutputStream.Write(pdfBytes, 0, pdfBytes.Length);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Erro ao exportar o relatório:", ex.ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao processar relatório:");
                        Console.WriteLine($"Tipo de exceção: {ex.GetType()}");
                        Console.WriteLine($"Mensagem de erro: {ex.Message}");
                        Console.WriteLine($"StackTrace: {ex.StackTrace}");
                    }
                }
            }
        }
    }
}
