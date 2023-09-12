using System.Text.RegularExpressions;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using GoodlistInsert.Interfaces;
using GoodlistInsert.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoodlistInsert.Services
{
    public class ListService : IListService
    {

        public void CreateListScript(vwObjectQuery complet, IFormFile file)
        {
            var script = new List<string>();

            var listDocuments = parseCsvtoObject(complet, file);
            

            foreach (ObjectQueryInserir obj in listDocuments)
            {
                var proc = complet.isGoodlist ? "suporte.spr_InserirWhiteListPadrao" : "suporte.spr_InserirBlackListPadrao";

                 var query = $"EXEC {proc} @EntidadeID = {complet.entidadeid}, @UsuarioID = {complet.usuarioId}, @Observacao = '{obj.observacao}'";



                if (complet.isCpf)
                {
                    query += $", @CpfCNPJ = '{obj.Cpf}'";
                }
                if (complet.isEmail)
                {
                    query += $", @Email = '{obj.Email}'";
                }

                if (complet.isEndereco)
                {
                    query += $", @HashEndereco = '{obj.Endereço}'";
                }


                if (complet.isTelefone)
                {
                    query += $", @tel_ddd = '{obj.ddd}'";
                    query += $", @tel_nro = '{obj.Tel}'";
                }

                script.Add(query);

            }

            GravatTxt(script);

        }

        public void CreateUpdateListScript(vwObjectQuery complet, IFormFile file)
        {
            var listDocuments = parseCsvtoObject(complet, file);
            var script = new List<string>();


            foreach (ObjectQueryInserir obj in listDocuments)
            {
                var query = $"EXEC suporte.spr_AlterarWhiteListPadrao @EntidadeID = {complet.entidadeid}, @UsuarioID = {complet.usuarioId}, @Observacao = '{obj.observacao}', @Ativar = {obj.Ativar}";
               
                if (complet.isCpf)
                query += $", @CpfCNPJ = '{obj.Cpf}'";
                
                if (complet.isEmail)
                query += $", @Email = '{obj.Email}'";
                

                if (complet.isEndereco)
                query += $", @HashEndereco = '{obj.Endereço}'";
                


                if (complet.isTelefone)
                {
                    query += $", @tel_ddd = '{obj.ddd}'";
                    query += $", @tel_nro = '{obj.Tel}'";
                }

                script.Add(query);

            }

            GravatTxt(script);
        }


        public List<ObjectQueryInserir> parseCsvtoObject(vwObjectQuery complet, IFormFile file)
        {
            string path = $"C:\\Users\\lucas.carvalho\\Downloads\\{file.FileName}";



            var xls = new XLWorkbook(path);

            var planilha = xls.Worksheets.Where(x => x.Name == complet.Aba).FirstOrDefault();

            var totalLinhas = planilha.Rows().Count() + 1;


            var queryresult = new List<ObjectQueryInserir>();


            for (int i = 2; i < totalLinhas; i++)
            {
                ObjectQueryInserir objquery = new ObjectQueryInserir
                {
                    entidadeId = complet.entidadeid,
                    UserId = complet.usuarioId,
                    observacao = complet.Observacao == "" ? planilha.Cell("A" + i).Value.ToString() : complet.Observacao,
                    Cpf = planilha.Cell("B" + i).Value.ToString(),
                    Tel = planilha.Cell("E" + i).Value.ToString(),
                    ddd = planilha.Cell("D" + i).Value.ToString(),
                    Endereço = planilha.Cell("F" + i).Value.ToString(),
                    Email = planilha.Cell("C" + i).Value.ToString(),

                };

                queryresult.Add(objquery);
            }

            return queryresult;
        }


        public void GravatTxt(List<string> script)
        {

            foreach (string str in script)
            {
                var contagem = 1;
                string finalPath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), $"Downloads\\ScriptWhitelist-{contagem}.txt");
                using (StreamWriter writer = new StreamWriter(finalPath, true))
                {

                    writer.WriteLine(str);
                }

                contagem++;
            }
        }
    }
}
