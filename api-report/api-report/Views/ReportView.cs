using System;


namespace api_report.Views
{
    public class ReportView
    {
        public int Codigo_Pedido { get; set; }
        public int Numero_Pedido_Formatado { get; set; }
        public DateTime Data_Pedido { get; set; }
        public int Codigo_Cliente { get; set; }
        public string Cliente { get; set; }
        public string Logradouro_Cliente { get; set; }
        public string Numero_Cliente { get; set; }
        public string Complemento_Cliente { get; set; }
        public string Cep_Cliente { get; set; }
        public string Cnpj_Cliente { get; set; }
        public string Inscricao_Estadual_Cliente { get; set; }
        public string Email_Cliente { get; set; }
        public string Telefone_Cliente { get; set; }
        public string Bairro_Cliente { get; set; }
        public string Uf_Cliente { get; set; }
        public string Condicao_Pagamento { get; set; }
        public decimal Desconto_Geral { get; set; }
        public decimal Valor_Total { get; set; }
        public decimal Valor_Final { get; set; }
        public string Codigo_Item { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor_Unitario { get; set; }
        public decimal Valor_Unitario_Desconto { get; set; }
        public decimal Valor_Total_Produto { get; set; }
        public string Nome { get; set; }
        public string Observacao { get; set; }
        public int Codigo_Destinacao { get; set; }
        public string Logradouro_Empresa { get; set; }
        public string Numero_Empresa { get; set; }
        public string Bairro_Empresa { get; set; }
        public string Municipio_Empresa { get; set; }

        public string Municipio_Cliente { get; set; }
        public string Uf_Empresa { get; set; }
        public string Cep_Empresa { get; set; }
        public string Cnpj_Empresa { get; set; }
        public string Inscricao_Estadual_Empresa { get; set; }
        public string Telefone_Empresa { get; set; }
        public string Email_Empresa { get; set; }
    }

}
