using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using System;

namespace APIMaisEventos.Utils
{
    // Singleton -> Static
    public static class Upload
    {
        // Upload de Imagens
        public static string UploadFile(IFormFile arquivo, string[] extensoesPermitidas, string diretorio)
        {
            try
            {
                // Determinamos onde será salvo o arquivo
                var pasta = Path.Combine("StaticFiles", diretorio);
                var caminho = Path.Combine(Directory.GetCurrentDirectory(), pasta);

                // Verificamos se existe um arquivo para ser salvo
                if (arquivo.Length > 0)
                {
                    // Pegamos o nome do arquivo
                    string nomeArquivo = ContentDispositionHeaderValue.Parse(arquivo.ContentDisposition).FileName.Trim('"');

                    // Validamos a extensão permitida
                    if(ValidarExtensao(extensoesPermitidas, nomeArquivo))
                    {
                        var extensao = RetornarExtensao(nomeArquivo);
                        var novoNome = $"{Guid.NewGuid()}.{extensao}";
                        var caminhoCompleto = Path.Combine(caminho, novoNome);

                        // Salvamos efetivamente o arquivo na aplicação
                        using(var stream = new FileStream(caminhoCompleto, FileMode.Create))
                        {
                            arquivo.CopyTo(stream);
                        }

                        return novoNome;
                    }

                }
                
                return "";
            }
            catch (System.Exception e)
            {

                return e.Message;
            }            
        }

        // Remover arquivo

        // Validar extensão de arquivo
        public static bool ValidarExtensao(string[] extensoesPermitidas, string nomeArquivo)
        {
            string extensao = RetornarExtensao(nomeArquivo);

            foreach(string ext in extensoesPermitidas)
            {
                if(ext == extensao)
                {
                    return true;
                }
            }

            return false;
        }

        // Retornar a extensão
        public static string RetornarExtensao(string nomeArquivo)
        {
            // [0]  [1]  [2]
            // arq.uivo.jpeg = 3
            // length(3) - 1 = 2
            string[] dados = nomeArquivo.Split('.');
            return dados[dados.Length - 1];
        }
    }
}
