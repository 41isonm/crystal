using api_report.Views;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;


namespace api_report.Repository
{
    public class ReportRepository
    {
        private readonly string _connectionString;

        public ReportRepository()
        {
            _connectionString = "Server=185.211.7.22;Database=u787840538_dragonpharmaPD;Uid=u787840538_dragonpharmaPD;Pwd=P@ssw0rd013459;";
        }

        public List<ReportView> GetReportData(int codigoEmpresa, int codigoPedido)
        {
            var reportData = new List<ReportView>();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    
                    connection.Open(); 

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "sp_report_ven000000002_mysql";
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        var paramCodigoEmpresa = new MySqlParameter("p_codigo_empresa", MySqlDbType.Int16);
                        paramCodigoEmpresa.Value = codigoEmpresa;
                        command.Parameters.Add(paramCodigoEmpresa);

                        var paramCodigoPedido = new MySqlParameter("p_codigo_pedido", MySqlDbType.Int64);
                        paramCodigoPedido.Value = codigoPedido;
                        command.Parameters.Add(paramCodigoPedido);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var reportMap = new ReportView
                                {
                                    Codigo_Pedido =  reader.GetInt32(reader.GetOrdinal("codigo_pedido")),
                                    Numero_Pedido_Formatado = reader.IsDBNull(reader.GetOrdinal("numero_pedido_formatado")) ? 0 : reader.GetInt32(reader.GetOrdinal("numero_pedido_formatado")),
                                    Data_Pedido =  reader.GetDateTime(reader.GetOrdinal("data_pedido")),
                                    Codigo_Cliente =  reader.GetInt32(reader.GetOrdinal("codigo_cliente")),
                                    Cliente =  reader.GetString(reader.GetOrdinal("cliente")),
                                    Logradouro_Cliente =  reader.GetString(reader.GetOrdinal("logradouro_cliente")),
                                    Numero_Cliente =  reader.GetString(reader.GetOrdinal("numero_cliente")),
                                    Complemento_Cliente =  reader.GetString(reader.GetOrdinal("complemento_cliente")),
                                    Cep_Cliente =  reader.GetString(reader.GetOrdinal("cep_cliente")),
                                    Cnpj_Cliente =  reader.GetString(reader.GetOrdinal("cnpj_cliente")),
                                    Inscricao_Estadual_Cliente = reader.GetString(reader.GetOrdinal("inscricao_estadual_cliente")),
                                    Email_Cliente =  reader.GetString(reader.GetOrdinal("email_cliente")),
                                    Telefone_Cliente =  reader.GetString(reader.GetOrdinal("telefone_cliente")),
                                    Bairro_Cliente =  reader.GetString(reader.GetOrdinal("bairro_cliente")),
                                    Uf_Cliente =  reader.GetString(reader.GetOrdinal("uf_cliente")),
                                    Condicao_Pagamento =  reader.GetString(reader.GetOrdinal("condicao_pagamento")),
                                    Desconto_Geral =  reader.GetDecimal(reader.GetOrdinal("desconto_geral")),
                                    Valor_Total =  reader.GetDecimal(reader.GetOrdinal("valor_total")),
                                    Valor_Final =  reader.GetDecimal(reader.GetOrdinal("valor_final")),
                                    Codigo_Item =  reader.GetString(reader.GetOrdinal("codigo_item")),
                                    Quantidade =  reader.GetInt32(reader.GetOrdinal("quantidade")),
                                    Valor_Unitario =  reader.GetDecimal(reader.GetOrdinal("valor_unitario")),
                                    Valor_Unitario_Desconto =  reader.GetDecimal(reader.GetOrdinal("valor_unitario_desconto")),
                                    Valor_Total_Produto =  reader.GetDecimal(reader.GetOrdinal("valor_total_produto")),
                                    Nome =  reader.GetString(reader.GetOrdinal("nome")),
                                    Observacao =  reader.GetString(reader.GetOrdinal("observacao")),
                                    Codigo_Destinacao =  reader.GetInt32(reader.GetOrdinal("codigo_destinacao")),
                                    Logradouro_Empresa =  reader.GetString(reader.GetOrdinal("logradouro_empresa")),
                                    Numero_Empresa =  reader.GetString(reader.GetOrdinal("numero_empresa")),
                                    Bairro_Empresa = reader.IsDBNull(reader.GetOrdinal("bairro_empresa")) ? string.Empty : reader.GetString(reader.GetOrdinal("bairro_empresa")),
                                    Municipio_Empresa = reader.IsDBNull(reader.GetOrdinal("municipio_empresa")) ? "teste" : reader.GetString(reader.GetOrdinal("municipio_empresa")),
                                    Municipio_Cliente = reader.IsDBNull(reader.GetOrdinal("municipio_cliente")) ? string.Empty : reader.GetString(reader.GetOrdinal("municipio_cliente")),
                                    Uf_Empresa = reader.IsDBNull(reader.GetOrdinal("uf_empresa")) ? string.Empty : reader.GetString(reader.GetOrdinal("uf_empresa")),
                                    Cep_Empresa = reader.IsDBNull(reader.GetOrdinal("cep_empresa")) ? string.Empty : reader.GetString(reader.GetOrdinal("cep_empresa")),
                                    Cnpj_Empresa = reader.IsDBNull(reader.GetOrdinal("cnpj_empresa")) ? string.Empty : reader.GetString(reader.GetOrdinal("cnpj_empresa")),
                                    Inscricao_Estadual_Empresa = reader.IsDBNull(reader.GetOrdinal("inscricao_estadual_empresa")) ? string.Empty : reader.GetString(reader.GetOrdinal("inscricao_estadual_empresa")),
                                    Telefone_Empresa = reader.IsDBNull(reader.GetOrdinal("telefone_empresa")) ? string.Empty : reader.GetString(reader.GetOrdinal("telefone_empresa")),
                                    Email_Empresa = reader.IsDBNull(reader.GetOrdinal("email_empresa")) ? "teste" : reader.GetString(reader.GetOrdinal("email_empresa")),
                                };

                                reportData.Add(reportMap);
                            }
                        }
                    }
                }

                return reportData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro lancado: ", ex.ToString());
                return null;
            }
        }
    }
}