using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Web;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using System.Linq;
using MySql.Data.MySqlClient;

namespace CrystalReportApi
{
    public class ReportModel
    {
        public int CodigoPedido { get; set; }
        public int NumeroPedidoFormatado { get; set; }
        public DateTime DataPedido { get; set; }
        public int CodigoCliente { get; set; }
        public string Cliente { get; set; }
        public string LogradouroCliente { get; set; }
        public string NumeroCliente { get; set; }
        public string ComplementoCliente { get; set; }
        public string CepCliente { get; set; }
        public string CnpjCliente { get; set; }
        public string InscricaoEstadualCliente { get; set; }
        public string EmailCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public string BairroCliente { get; set; }
        public string UfCliente { get; set; }
        public string CondicaoPagamento { get; set; }
        public decimal DescontoGeral { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorFinal { get; set; }
        public string CodigoItem { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorUnitarioDesconto { get; set; }
        public decimal ValorTotalProduto { get; set; }
        public string NomeVendedor { get; set; }
        public string Observacao { get; set; }
        public int CodigoDestinacao { get; set; }
        public string LogradouroEmpresa { get; set; }
        public string NumeroEmpresa { get; set; }
        public string BairroEmpresa { get; set; }
        public string MunicipioEmpresa { get; set; }
        public string UfEmpresa { get; set; }
        public string CepEmpresa { get; set; }
        public string CnpjEmpresa { get; set; }
        public string InscricaoEstadualEmpresa { get; set; }
        public string TelefoneEmpresa { get; set; }
        public string EmailEmpresa { get; set; }
    }


    public class ReportRepository
    {
        private readonly string _connectionString;



        public ReportRepository()
        {
            _connectionString = "Server=185.211.7.22;Database=u787840538_dragonpharmaPD;Uid=u787840538_dragonpharmaPD;Pwd=P@ssw0rd013459;SslMode=None;Charset=utf8mb4;Allow User Variables=True;";
        }


        public List<ReportModel> GetReportData(int codigoEmpresa, int codigoPedido)
        {
            var reportData = new List<ReportModel>();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.InfoMessage += (sender, e) => {
                        Console.WriteLine($"MySQL Info: {e.ToString()}");
                    };

                    connection.StateChange += (sender, e) => {
                        Console.WriteLine($"MySQL Connection State: {e.CurrentState}");
                    };
                    Console.WriteLine("Abrindo conexão com o banco de dados...");

                    connection.Open(); // Linha 87 onde ocorre o erro

                    using (var command = connection.CreateCommand())
                    {


                        command.CommandText = "sp_report_ven000000002_mysql";
                        command.CommandType = CommandType.StoredProcedure;

                        var paramCodigoEmpresa = new MySqlParameter("p_codigo_empresa", MySqlDbType.Int32);
                        paramCodigoEmpresa.Value = codigoEmpresa;
                        command.Parameters.Add(paramCodigoEmpresa);

                        var paramCodigoPedido = new MySqlParameter("p_codigo_pedido", MySqlDbType.Int32);
                        paramCodigoPedido.Value = codigoPedido;
                        command.Parameters.Add(paramCodigoPedido);
                        Console.WriteLine("Executando comando SQL...");

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var reportModel = new ReportModel
                                {
                                    CodigoPedido = reader.GetInt32(reader.GetOrdinal("codigo_pedido")),
                                    NumeroPedidoFormatado = reader.GetInt32(reader.GetOrdinal("numero_pedido_formatado")),
                                    DataPedido = reader.GetDateTime(reader.GetOrdinal("data_pedido")),
                                    CodigoCliente = reader.GetInt32(reader.GetOrdinal("codigo_cliente")),
                                    Cliente = reader.GetString(reader.GetOrdinal("cliente")),
                                    LogradouroCliente = reader.GetString(reader.GetOrdinal("logradouro_cliente")),
                                    NumeroCliente = reader.IsDBNull(reader.GetOrdinal("numero_cliente")) ? null : reader.GetString(reader.GetOrdinal("numero_cliente")),
                                    ComplementoCliente = reader.GetString(reader.GetOrdinal("complemento_cliente")),
                                    CepCliente = reader.GetString(reader.GetOrdinal("cep_cliente")),
                                    CnpjCliente = reader.GetString(reader.GetOrdinal("cnpj_cliente")),
                                    InscricaoEstadualCliente = reader.GetString(reader.GetOrdinal("inscricao_estadual_cliente")),
                                    EmailCliente = reader.GetString(reader.GetOrdinal("email_cliente")),
                                    TelefoneCliente = reader.GetString(reader.GetOrdinal("telefone_cliente")),
                                    BairroCliente = reader.GetString(reader.GetOrdinal("bairro_cliente")),
                                    UfCliente = reader.GetString(reader.GetOrdinal("uf_cliente")),
                                    CondicaoPagamento = reader.GetString(reader.GetOrdinal("condicao_pagamento")),
                                    DescontoGeral = reader.GetDecimal(reader.GetOrdinal("desconto_geral")),
                                    ValorTotal = reader.GetDecimal(reader.GetOrdinal("valor_total")),
                                    ValorFinal = reader.GetDecimal(reader.GetOrdinal("valor_final")),
                                    CodigoItem = reader.GetString(reader.GetOrdinal("codigo_item")),
                                    Quantidade = reader.GetInt32(reader.GetOrdinal("quantidade")),
                                    ValorUnitario = reader.GetDecimal(reader.GetOrdinal("valor_unitario")),
                                    ValorUnitarioDesconto = reader.GetDecimal(reader.GetOrdinal("valor_unitario_desconto")),
                                    ValorTotalProduto = reader.GetDecimal(reader.GetOrdinal("valor_total_produto")),
                                    NomeVendedor = reader.GetString(reader.GetOrdinal("nome")),
                                    Observacao = reader.GetString(reader.GetOrdinal("observacao")),
                                    CodigoDestinacao = reader.GetInt32(reader.GetOrdinal("codigo_destinacao")),
                                    LogradouroEmpresa = reader.GetString(reader.GetOrdinal("logradouro_empresa")),
                                    NumeroEmpresa = reader.GetString(reader.GetOrdinal("numero_empresa")),
                                    BairroEmpresa = reader.GetString(reader.GetOrdinal("bairro_empresa")),
                                    MunicipioEmpresa = reader.GetString(reader.GetOrdinal("municipio_empresa")),
                                    UfEmpresa = reader.GetString(reader.GetOrdinal("uf_empresa")),
                                    CepEmpresa = reader.GetString(reader.GetOrdinal("cep_empresa")),
                                    CnpjEmpresa = reader.GetString(reader.GetOrdinal("cnpj_empresa")),
                                    InscricaoEstadualEmpresa = reader.GetString(reader.GetOrdinal("inscricao_estadual_empresa")),
                                    TelefoneEmpresa = reader.GetString(reader.GetOrdinal("telefone_empresa")),
                                    EmailEmpresa = reader.GetString(reader.GetOrdinal("email_empresa"))
                                };

                                reportData.Add(reportModel);
                            }
                        }
                    }
                }

                return reportData;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro do MySQL:", ex.ToString());
                return null;
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("Erro de conversão de tipo:", ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro geral:", ex.ToString());
                return null;
            }
        }


        class Program
        {
            static void Main(string[] args)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
                string _connectionString = "Server=185.211.7.22;Database=u787840538_dragonpharmaPD;Uid=u787840538_dragonpharmaPD;Pwd=P@ssw0rd013459;SslMode=None;";

                HttpListener listener = new HttpListener();
                listener.Prefixes.Add("http://localhost:5000/");
                listener.Start();
                Console.WriteLine("Servidor iniciado em http://localhost:5000/");


                while (true)
                {
                    HttpListenerContext context = listener.GetContext();
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response;

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

                            //pasando uma string vazia eu consigo visualizar o pdf
                            var reportData = reportRepository.GetReportData(codigoEmpresa, codigoPedido);
                            string reportPath = "C:\\git\\Cristal\\VEN000000002.rpt";

                            using (MemoryStream pdfStream = new MemoryStream())
                            {
                                using (ReportDocument reportDocument = new ReportDocument())
                                {
                                    // Carrega o relatório a partir do arquivo .rpt
                                    reportDocument.Load(reportPath);

                                    // Define os dados do relatório
                                    reportDocument.SetDataSource(reportData);

                                    // Exporta o relatório para o fluxo de memória como PDF
                                    reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

                                    // Define o conteúdo da resposta HTTP como o PDF
                                    response.ContentType = "application/pdf";
                                    response.ContentLength64 = pdfStream.Length;

                                    // Copia o stream do PDF para o stream de resposta HTTP
                                    pdfStream.WriteTo(response.OutputStream);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            Console.WriteLine("Erro ao processar relatório:");
                            Console.WriteLine($"Tipo de exceção: {ex.GetType()}");
                            Console.WriteLine($"Mensagem de erro: {ex.Message}");
                            Console.WriteLine($"StackTrace: {ex.StackTrace}");
                            response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            byte[] buffer = Encoding.UTF8.GetBytes("Erro ao gerar o relatório.");
                            response.ContentLength64 = buffer.Length;
                            response.OutputStream.Write(buffer, 0, buffer.Length);
                            response.OutputStream.Close();
                        }
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Close();
                    }
                }
            }
        }
    }
}
